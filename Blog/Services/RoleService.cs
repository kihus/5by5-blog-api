using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;
using Blog.Services.Contracts;

namespace Blog.Services;

public class RoleService : IRoleService
{
	private readonly RoleRepository _repository;

	public RoleService(RoleRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<RoleResponseDTO>> GetAllAsync()
	{
		return await _repository.GetAllAsync();
	}

	public async Task<RoleResponseDTO?> GetRoleBySlugAsync(string slug)
	{
		return await _repository.GetBySlugAsync(slug);
	}

	public async Task CreateRoleAsync(RoleRequestDTO role)
	{
		var newRole = new Role(
			role.Name,
			role.Name.ToLower().Replace(" ", "-")
			);

		await _repository.CreateRoleAsync(newRole);
	}

	public async Task UpdateRoleAsync(string slug, RoleRequestDTO role)
	{
		var newRole = new Role(
			role.Name,
			slug
			);

		await _repository.UpdateRoleAsync(newRole);
	}

	public async Task DeleteRoleAsync(string slug)
	{
		await _repository.DeleteRoleBySlugAsync(slug);
	}
}
