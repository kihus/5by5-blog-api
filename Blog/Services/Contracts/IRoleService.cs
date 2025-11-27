using Blog.Models.DTOs;

namespace Blog.Services.Contracts;

public interface IRoleService
{
	public Task<List<RoleResponseDTO>> GetAllAsync();
	public Task<RoleResponseDTO?> GetRoleBySlugAsync(string slug);
	public Task CreateRoleAsync(RoleRequestDTO role);
	public Task UpdateRoleAsync(string slug, RoleRequestDTO role);
	public Task DeleteRoleAsync(string slug);
}
