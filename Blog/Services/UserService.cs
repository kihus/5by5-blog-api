using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;

namespace Blog.Services;

public class UserService
{
	private readonly UserRepository _repository;

	public UserService(UserRepository repository)
	{
		_repository = repository;
	}

	public async Task CreateUserAsync(UserRequestDTO userRequest)
	{
		var user = new User(
			userRequest.Name,
			userRequest.Email,
			userRequest.Password
			);

		await _repository.CreateUserAsync(user);
	}
}
