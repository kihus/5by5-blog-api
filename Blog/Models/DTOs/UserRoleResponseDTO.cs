namespace Blog.Models.DTOs;

public class UserRoleResponseDTO
{
	public int Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public string Email { get; init; } = string.Empty;
	public string Bio { get; init; } = string.Empty;
	public string Image { get; init; } = string.Empty;
	public string Slug { get; init; } = string.Empty;
	public List<UserRoleDTO> Roles { get; set; } = [];
}
