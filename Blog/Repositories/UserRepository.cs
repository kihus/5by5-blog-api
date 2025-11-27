using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Dapper;

namespace Blog.Repositories;

public class UserRepository
{
	private readonly ConnectionDB _connection;

	public UserRepository(ConnectionDB connection)
	{
		_connection = connection;
	}

	public async Task<List<UserResponseDTO>> GetAllUsersAsync()
	{
		var sql = "SELECT [Name], Email, Bio, [Image], Slug FROM [User]";

		using (var con = _connection.GetConnection())
		{
			return (await con.QueryAsync<UserResponseDTO>(sql)).ToList();
		}
	}

	public async Task<UserResponseDTO?> GetUserBySlugAsync(string slug)
	{
		var sql = "SELECT [Name], Email, Bio, [Image], Slug FROM [User] WHERE Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			return await con.QueryFirstOrDefaultAsync<UserResponseDTO>(sql, new { slug });
		}
	}

	public async Task CreateUserAsync(User user)
	{
		var sql = "INSERT INTO [User] ([Name], Email, PasswordHash, Image, Bio, Slug) VALUES (@Name, @Email, @PasswordHash, @Image, @Bio, @Slug)";

		using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { user.Name, user.Email, user.PasswordHash, user.Image, user.Bio, user.Slug });
		}
	}

	public async Task UpdateUser(User user)
	{
		var sql = "UPDATE [User] SET [Name] = @Name, Email = @Email, PasswordHash = @PasswordHash, Image = @Image, Bio = @Bio " +
				  "WHERE Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { user.Name, user.Email, user.PasswordHash, user.Image, user.Bio, user.Slug });
		}
	}

	public async Task<UserLoginDTO?> GetUser(string slug)
	{
		var sql = "SELECT Email, PasswordHash FROM [User] WHERE Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			return await con.QueryFirstOrDefaultAsync<UserLoginDTO>(sql, new { slug });
		}
	}

	public async Task DeleteUser(string slug)
	{
		var sql = "DELETE FROM [User] WHERE Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { slug });
		}
	}

	public async Task<List<User>> GetAllUserRoles()
	{
		IEnumerable<User> userRoles = new List<User>();
		var sql = "SELECT * FROM [User] u JOIN [UserRole] ur ON u.Id = ur.UserId JOIN [Role] r ON r.Id = ur.RoleId";

		using (var con = _connection.GetConnection())
		{
			userRoles = await con.QueryAsync<User, Role, User>(
				sql,
				(user, role) =>
				{
					user.Roles.Add(role);
					return user;
				},
				splitOn: "Id"
			);
		}
		return userRoles.ToList();
	}
}
