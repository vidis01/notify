using NotifyDB.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace NotifyAPI.DtoModel
{
    public class CompanyDto
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(10)]
        [RegularExpression(@"^[0123456789]{10}$", ErrorMessage = "Only numeric characters are allowed.")]
        public string Number { get; set; }

        public CompanyType Type { get; set; }
        public Market Market { get; set; }
        public DateTime CallDate { get; set; }
    }
}
