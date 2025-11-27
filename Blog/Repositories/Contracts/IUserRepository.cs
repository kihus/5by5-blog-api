using Blog.Models;

namespace Blog.Repositories.Contracts;

public interface IUserRepository
{
	public Task CreateUser(User user);
}
