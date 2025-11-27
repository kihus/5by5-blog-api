using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace Blog.Services;

public class UserService
{
	private readonly UserRepository _repository;
	private const int _workFactor = 12;
	public UserService(UserRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<UserResponseDTO>> GetAllUsersAsync()
	{
		return await _repository.GetAllUsersAsync();
	}

	public async Task<UserResponseDTO?> GetUserBySlug(string slug)
	{
		return await _repository.GetUserBySlugAsync(slug);
	}

	public async Task CreateUserAsync(UserRequestDTO userRequest)
	{
		var user = new User(
			userRequest.Name,
			userRequest.Email,
			HashPassword(userRequest.Password, _workFactor),
			userRequest.Image,
			userRequest.Bio,
			userRequest.Name.ToLower().Replace(" ", "-")
			);

		await _repository.CreateUserAsync(user);
	}

	public async Task UpdateUser(string slug, UserRequestDTO userRequest)
	{
		var user = await _repository.GetUserBySlugAsync(slug);
		var userPassword = _repository.GetUser(slug).Result.PasswordHash;

		if (!string.IsNullOrEmpty(userRequest.Password))
			userPassword = HashPassword(userRequest.Password, _workFactor);

		var newUser = new User(
			string.IsNullOrEmpty(userRequest.Name) 
									? user.Name 
									: userRequest.Name,
			string.IsNullOrEmpty(userRequest.Email) 
									? user.Email 
									: userRequest.Email,
			userPassword,
			string.IsNullOrEmpty(userRequest.Image) 
									? user.Image 
									: userRequest.Image,
			string.IsNullOrEmpty(userRequest.Bio) 
									? user.Bio 
									: userRequest.Bio,
			user.Slug
			);

		await _repository.UpdateUser(newUser);
	}

	public async Task DeleteUser(string slug, UserLoginDTO userLogin)
	{
		var user = await _repository.GetUser(slug);

		var passwordsMatch = Verify(userLogin.PasswordHash, user.PasswordHash);

		if (passwordsMatch && user.Email == userLogin.Email)
			await _repository.DeleteUser(slug);

		return;
	}
}
