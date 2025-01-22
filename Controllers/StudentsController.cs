using Kampus.Data;
using Kampus.Models;
using Kampus.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kampus.Controllers
{

    //localhost:xxxx/api/Students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public StudentsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //GET 
        [HttpGet]
        public IActionResult GetAllStudents()
        {

            var students = dbContext.StudentDatas.ToList();

            if (students == null || students.Count == 0)
            {
                return NotFound(new { status = 404, message = "No students found", data = students });
            }

            return Ok(new { status = 200, message = "Success", data = students });
        }

        [HttpPost]
        public IActionResult AddStudent(AddStudentDto addStudentDto)
        {
            var studentEntity = new Students()
            {
                Name = addStudentDto.Name,
                LastName = addStudentDto.LastName,
                Bithday = addStudentDto.Bithday
            };

            if (string.IsNullOrEmpty(studentEntity.StudentId))
            {
                var currentDate = DateTime.Now;
                var currentYearMonth = currentDate.ToString("yyMM"); // Format TahunBulan (e.g. 2501)
                studentEntity.StudentId = currentYearMonth + "01"; // Atau dengan logika increment
            }

            dbContext.StudentDatas.Add(studentEntity);
            dbContext.SaveChanges();

            return Ok(studentEntity);
        }


        [HttpGet("{StudentId}")]
        public IActionResult GetStudentsById(string StudentId)
        {
            var Student = dbContext.StudentDatas.Find(StudentId);

            if(Student is null)
            {
                return NotFound();
            }
            return Ok(Student);
        }
    }

}
