using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CompanyType
    {
        Small,
        Medium,
        Large
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Market
    {
        Denmark, 
        Norway, 
        Sweden, 
        Finland
    }
}
