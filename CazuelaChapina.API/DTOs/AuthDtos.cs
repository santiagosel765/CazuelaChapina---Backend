namespace CazuelaChapina.API.DTOs;

public record class LoginRequest
{
    public LoginRequest() { }

    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public record class RegisterRequest
{
    public RegisterRequest() { }

    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}

public record class LoginResponse
{
    public LoginResponse(string token)
    {
        Token = token;
    }

    public string Token { get; init; } = string.Empty;
}
