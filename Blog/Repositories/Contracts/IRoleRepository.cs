using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Repositories.Contracts;

public interface IRoleRepository
{
	public Task<List<RoleResponseDTO>> GetAllAsync();
	public Task<RoleResponseDTO?> GetBySlugAsync(string slug);
	public Task CreateRoleAsync(Role role);
	public Task UpdateRoleAsync(Role role);
	public Task DeleteRoleBySlugAsync(string slug);
}
