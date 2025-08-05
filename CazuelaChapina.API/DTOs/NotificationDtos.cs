namespace CazuelaChapina.API.DTOs;

public record class RegisterDeviceDto
{
    public RegisterDeviceDto() { }

    public string Token { get; init; } = string.Empty;
}
