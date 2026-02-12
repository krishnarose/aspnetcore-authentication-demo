namespace AuthProject.Models.DomainModels
{
    public class User : BaseEntity
    {

        public required string name { get; set; }
        public required string role { get; set; }
        public required string password { get; set; }
        public required string mobile { get; set; }
        public required string username { get; set; }


    }
}
