namespace Blog.Models.DTOs;

public class UserRequestDTO
{
	public string Name { get; init; } = string.Empty;
	public string Email { get; init; } = string.Empty;
	public string Password { get; init; } = string.Empty;
	public string Image { get; init; } = string.Empty;
	public string Bio { get; init; } = string.Empty;
}
