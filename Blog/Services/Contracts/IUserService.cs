using Blog.Models.DTOs;

namespace Blog.Services.Contracts;

public interface IUserService
{
	public Task CreateUserAsync(UserRequestDTO userRequest);
}
