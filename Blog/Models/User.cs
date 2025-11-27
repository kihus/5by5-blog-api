namespace Blog.Models;

public class User(
	string name,
	string email,
	string password,
	string? bio,
	string? image,
	string slug
	)
{
	public int Id { get; private set; }
	public string Name { get; private set; } = name;
	public string Email { get; private set; } = email;
	public string PasswordHash { get; private set; } = password;
	public string? Bio { get; private set; } = bio;
	public string? Image { get; private set; } = image;
	public string Slug { get; private set; } = slug;
}
