namespace Blog.Models.DTOs;

public class UserLoginDTO
{
	public string Email { get; init; } = string.Empty;
	public string PasswordHash { get; init; } = string.Empty;
}
