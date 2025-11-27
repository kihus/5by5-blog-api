namespace Blog.Models;

public class User(
	string name,
	string email,
	string password
	)
{
	public int Id { get; private set; }
	public string Name { get; private set; } = name;
	public string Email { get; private set; } = email;
	public string PasswordHash { get; private set; } = password;
	public string Bio { get; private set; }
	public string Image { get; private set; }
	public string Slug { get; private set; }
}
