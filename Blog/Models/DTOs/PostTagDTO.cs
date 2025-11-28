namespace Blog.Models.DTOs;

public class PostTagDTO
{
	public int Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public string Slug { get; init; } = string.Empty;
}
