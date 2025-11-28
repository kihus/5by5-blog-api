namespace Blog.Models.DTOs;

public class PostResponseDTO
{
	public string Category { get; set; }
	public string Author { get; set; }
	public string Title { get; set; }
	public string Summary { get; set; }
	public string Body { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
