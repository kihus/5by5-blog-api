using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories.Contracts;
using Dapper;

namespace Blog.Repositories;

public class UserRepository : IUserRepository
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

	public async Task<string?> GetAuthorById(int id)
	{
		var sql = @"SELECT Name FROM [User] WHERE Id = @Id";

		using (var con = _connection.GetConnection())
		{
			return await con.QueryFirstOrDefaultAsync<string>(sql);
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

	public async Task<List<UserRoleResponseDTO>> GetAllUserRoles()
	{
		IEnumerable<UserRoleResponseDTO> userRoles = new List<UserRoleResponseDTO>();
		var sql = @"SELECT u.[Id], u.[Name], u.[Email], u.[Bio], u.[Image], u.[Slug], r.[Id], r.[Name], r.[Slug] 
					FROM [User] u 
					JOIN [UserRole] ur 
					ON u.Id = ur.UserId 
					JOIN [Role] r 
					ON r.Id = ur.RoleId";

		using (var con = _connection.GetConnection())
		{
			userRoles = await con.QueryAsync<UserRoleResponseDTO, UserRoleDTO, UserRoleResponseDTO>(
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

	public async Task<IEnumerable<UserRoleResponseDTO?>> GetUserRoleBySlug(string slug)
	{
		var sql = @"SELECT u.[Id], u.[Name], u.[Email], u.[Bio], u.[Image], u.[Slug], r.[Id], r.[Name], r.[Slug] 
					FROM [User] u 
					JOIN [UserRole] ur 
					ON u.Id = ur.UserId 
					JOIN [Role] r 
					ON r.Id = ur.RoleId
					WHERE u.Slug = @Slug";

		IEnumerable<UserRoleResponseDTO> userRole = new List<UserRoleResponseDTO>();

		using (var con = _connection.GetConnection())
		{
			userRole = await con.QueryAsync<UserRoleResponseDTO, UserRoleDTO, UserRoleResponseDTO>(
				sql,
				(user, role) =>
				{
					user.Roles.Add(role);
					return user;
				},
				new { slug },
				splitOn: "Id"

			);
		}

		return userRole.ToList();
	}
}
