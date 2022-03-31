namespace NotifyAPI
{
    public class NotificationSettings
    {
        public NotificationSetting[] Settings { get; set; }
    }

    public class NotificationSetting
    {
        public string Market { get; set; }
        public int[] CompanyTypes { get; set; }
        public int[] NotificationDays { get; set; }
        
    }
}
