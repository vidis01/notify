using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotifyDB.Models
{
    public class Company
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^[0123456789]{10}$")]
        public string Number { get; set; }

        public CompanyType Type { get; set; }
        public Market Market { get; set; }
        public DateTime CallDate { get; set; }
        public List<Schedule> Schedules { get; set; }
    }

    public enum CompanyType
    {
        Small,
        Medium,
        Large
    }

    public enum Market
    {
        Denmark, 
        Norway, 
        Sweden, 
        Finland
    }
}
