﻿namespace Kampus.Models
{
    public class AddStudentDto
    {
        public required string Name { get; set; }
        public required string? LastName { get; set; }
        public required DateTime Bithday { get; set; }
    }
}
