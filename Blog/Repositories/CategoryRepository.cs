using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories.Contracts;
using Dapper;

namespace Blog.Repositories;

public class CategoryRepository : ICategoryRepository
{
	private readonly ConnectionDB _connection;

	public CategoryRepository(ConnectionDB connection)
	{
		_connection = connection;
	}

	public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
	{
		var sql = "SELECT Name, Slug FROM Category";

		using (var con = _connection.GetConnection())
		{
			return (await con.QueryAsync<CategoryResponseDTO>(sql)).ToList();
		}

	}

	public async Task<Category?> GetBySlugAsync(string slug)
	{
		var sql = "SELECT Name, Slug FROM Category WHERE Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			return await con.QueryFirstOrDefaultAsync<Category>(sql, new { slug });
		}
	}

	public async Task CreateCategoryAsync(Category category)
	{
		var sql = "INSERT INTO Category (Name, Slug) VALUES (@Name, @Slug)";

		await using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { category.Name, category.Slug });
		}
	}

	public async Task DeleteCategoryAsync(string slug)
	{
		var sql = "DELETE FROM Category WHERE Slug = @Slug";

		await using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { slug });
		}
	}
}
