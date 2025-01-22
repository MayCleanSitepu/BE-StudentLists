using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Kampus.Models.Entities
{
    public class Students
    {

        [Key]
        public string StudentId { get; set; }

        public required string Name { get; set; }
        public required string? LastName { get; set; }
        public required DateTime Bithday { get; set; }
    }
}
