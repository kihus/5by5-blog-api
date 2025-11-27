using Blog.Data;
using Blog.Models;
using Dapper;

namespace Blog.Repositories;

public class UserRepository
{
	private readonly ConnectionDB _connection;

	public UserRepository(ConnectionDB connection)
	{
		_connection = connection;
	}

	public async Task CreateUserAsync(User user)
	{
		var sql = "INSERT INTO User (Name, Email, PasswordHash) VALUES (@Name, @Email, @Password)";

		using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { user.Name, user.Email, user.PasswordHash });
		}
	}
}
