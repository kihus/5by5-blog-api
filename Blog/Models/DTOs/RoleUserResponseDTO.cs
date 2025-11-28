namespace Blog.Models.DTOs
{
	public class RoleUserResponseDTO
	{
		public int Id { get; init; }
		public string Name { get; init; } = string.Empty;
		public string Slug { get; init; } = string.Empty;
		public List<RoleUserDTO> Users { get; set; } = [];
	}
}
