namespace AuthProject.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }   
    public string Role { get; set; }
    public DateTime Created_at { get; set; }
}