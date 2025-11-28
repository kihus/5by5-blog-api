namespace Blog.Models;

public class Post(
	int categoryId, 
	int authorId, 
	string title, 
	string summary, 
	string body, 
	string slug
	)
{
	public int Id { get; private set; }
	public int CategoryId { get; private set; } = categoryId;
	public int AuthorId { get; private set; } = authorId;
	public string Title { get; private set; } = title;
	public string Summary { get; private set; } = summary;
	public string Body { get; private set; } = body;
	public string Slug { get; private set; } = slug;
	public DateTime CreateDate { get; private set; }
	public DateTime UpdateDate { get; private set; }
	public List<Tag> Tags { get; private set; } = [];
}
