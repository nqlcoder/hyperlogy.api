using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperlogy.Common.Entities
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string StudentCode { get; set; }

        public string StudentName { get; set; }

        public string Address { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
    }
}
