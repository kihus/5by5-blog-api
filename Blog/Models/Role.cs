namespace Blog.Models;

public class Role(
	string name,
	string slug
	)
{
	public int Id { get; private set; }
	public string Name { get; private set; } = name;
	public string Slug { get; private set; } = slug;
	public List<User> Users { get; private set; }

	public void SetId(int id)
	{
		Id = id;
	}
}
