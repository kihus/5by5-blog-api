namespace Blog.Models;

public class Tag(
	string name,
	string slug
	)
{
	public int Id { get; private set; }
	public string Name { get; private set; } = name;
	public string Slug { get; private set; } = slug;
}
