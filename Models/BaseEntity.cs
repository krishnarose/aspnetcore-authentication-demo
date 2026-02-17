namespace AuthProject.Models
{
    public class BaseEntity
    {
        public long id { get; set; }
        public DateTime created_at { get; set; }
        public long create_by { get; set; }
        public string create_ip { get; set; }
        public DateTime? updated_at { get; set; }
        public string? update_ip { get; set; }
        public long? update_by { get; set; }

        public DateTime? deleted_at { get; set; }
        public string? deleted_ip { get; set; }
        public long? deleted_by { get; set; }

    }

}