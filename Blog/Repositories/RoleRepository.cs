using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories.Contracts;
using Dapper;

namespace Blog.Repositories;

public class RoleRepository : IRoleRepository
{
	private readonly ConnectionDB _connection;

	public RoleRepository(ConnectionDB connection)
	{
		_connection = connection;
	}

	public async Task<List<RoleResponseDTO>> GetAllAsync()
	{
		var sql = "SELECT Name, Slug FROM Role";

		using (var con = _connection.GetConnection())
		{
			return (await con.QueryAsync<RoleResponseDTO>(sql)).ToList();
		}
	}

	public async Task<RoleResponseDTO?> GetBySlugAsync(string slug)
	{
		var sql = "SELECT Name, Slug FROM Role WHERE Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			return await con.QueryFirstOrDefaultAsync<RoleResponseDTO>(sql, new { slug });
		}
	}

	public async Task<List<RoleUserResponseDTO>> GetAllRolesUsers()
	{
		IEnumerable<RoleUserResponseDTO> roleUser = new List<RoleUserResponseDTO>();
		var sql = @"SELECT  r.[Id], r.[Name], r.[Slug], u.[Id], u.[Name], u.[Email], u.[Bio], u.[Image], u.[Slug]
					FROM [Role] r 
					JOIN [UserRole] ur 
					ON r.Id = ur.UserId 
					JOIN [User] u 
					ON u.Id = ur.RoleId";

		using (var con = _connection.GetConnection())
		{
			roleUser = await con.QueryAsync<RoleUserResponseDTO, RoleUserDTO, RoleUserResponseDTO>(
				sql,
				(role, user) => { role.Users.Add(user); return role; },
				splitOn: "Id"
			);
		}
		return roleUser.ToList();
	}

	public async Task<List<RoleUserResponseDTO?>> GetRoleUserBySlug(string slug)
	{
		IEnumerable<RoleUserResponseDTO> roleUser = new List<RoleUserResponseDTO>();
		var sql = @"SELECT  r.[Id], r.[Name], r.[Slug], u.[Id], u.[Name], u.[Email], u.[Bio], u.[Image], u.[Slug]
					FROM [Role] r 
					JOIN [UserRole] ur 
					ON r.Id = ur.UserId 
					JOIN [User] u 
					ON u.Id = ur.RoleId
					WHERE r.Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			roleUser = await con.QueryAsync<RoleUserResponseDTO, RoleUserDTO, RoleUserResponseDTO>(
				sql,
				(role, user) => { role.Users.Add(user); return role; },
				new {slug},
				splitOn: "Id"
			);
		}
		return roleUser.ToList();
	}

	public async Task CreateRoleAsync(Role role)
	{
		var sql = "INSERT INTO Role (Name, Slug) VALUES (@Name, @Slug)";

		using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { role.Name, role.Slug });
		}
	}

	public async Task UpdateRoleAsync(Role role)
	{
		var sql = "UPDATE Role SET Name = @Name WHERE Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { role.Name, role.Slug });
		}
	}

	public async Task DeleteRoleBySlugAsync(string slug)
	{
		var sql = "DELETE FROM Role WHERE Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { slug });
		}
	}
}
