namespace Blog.Models.DTOs;

public class PostTagResponseDTO
{
	public int Id { get; set; }
	public string CategoryName { get; set; } = string.Empty;
	public string AuthorName { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string Summary { get; set; } = string.Empty;
	public string Body { get; set; } = string.Empty;
	public string Slug { get; set; } = string.Empty;
	public DateTime CreateDate { get; set; }
	public DateTime LastUpdateDate { get; set; }
	public List<PostTagDTO> Tags { get; set; } = [];
}
