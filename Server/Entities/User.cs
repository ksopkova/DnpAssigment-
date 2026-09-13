namespace Entities;

public class User
{
    public int UserId { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}