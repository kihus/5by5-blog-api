namespace Blog.Models;

public class Category(
	string name, 
	string slug
	)
{
	public int Id { get; private set; }
	public string Name { get; private set; } = name;
	public string Slug { get; private set; } = slug;

	public void SetId(int id)
	{
		Id = id;
	}
}
