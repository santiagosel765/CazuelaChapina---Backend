namespace CazuelaChapina.API.Models;

public class Device
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public int? UserId { get; set; }
    public User? User { get; set; }
}
