namespace Blog.Models.DTOs;

public class PostResponseDTO
{
	public string AuthorName { get; init; } = string.Empty;
	public string CategoryName { get; init; } = string.Empty;
	public string Title { get; init; } = string.Empty;
	public string Summary { get; init; } = string.Empty;
	public string Body { get; init; } = string.Empty;
	public string Slug { get; init; } = string.Empty;
	public DateTime CreatedAt { get; init; }
	public DateTime UpdatedAt { get; init; }
}
