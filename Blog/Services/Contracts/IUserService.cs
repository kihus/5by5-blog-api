using Blog.Models.DTOs;

namespace Blog.Services.Contracts;

public interface IUserService
{
	public Task<List<UserResponseDTO>> GetAllUsersAsync();
	public Task CreateUserAsync(UserRequestDTO userRequest);
	public Task<UserResponseDTO?> GetUserBySlug(string slug);
	public Task<List<UserRoleResponseDTO>> GetAllUserRoles();
	public Task<UserRoleResponseDTO?> GetUserRoleBySlug(string slug);
	public Task UpdateUser(string slug, UserRequestDTO userRequest);
	public Task DeleteUser(string slug, UserLoginDTO userLogin);
	public Task<string?> GetAuthorById(int id);
}
