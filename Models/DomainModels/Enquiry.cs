namespace AuthProject.Models.DomainModels
{
    public class Enquiry:BaseEntity
    {
        public string first_name  { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string discription { get; set; }
    }
}
