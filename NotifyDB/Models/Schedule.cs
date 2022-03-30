using System;

namespace NotifyDB.Models
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
