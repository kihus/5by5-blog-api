using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Repositories.Contracts;

public interface IUserRepository
{
	public Task<List<UserResponseDTO>> GetAllUsersAsync();
	public Task<UserResponseDTO?> GetUserBySlugAsync(string slug);
	public Task CreateUserAsync(User user);
	public Task UpdateUser(User user);
	public Task<UserLoginDTO?> GetUser(string slug);
	public Task DeleteUser(string slug);
	public Task<List<UserRoleResponseDTO>> GetAllUserRoles();
	public Task<IEnumerable<UserRoleResponseDTO?>> GetUserRoleBySlug(string slug);
}
