namespace AuthProject.Models.ServiceModel
{
    public class AuthSession
    {
        public required string Username { get; set; }
        public required string Role { get; set; }
        public required Guid SessionId { get; set; }
        public required string Name { get; set; }
    }
}