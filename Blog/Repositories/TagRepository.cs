using Azure;
using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories.Contracts;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Repositories
{
	public class TagRepository : ITagRepository
	{
		private readonly ConnectionDB _connection;

		public TagRepository(ConnectionDB connection)
		{
			_connection = connection;
		}

		public async Task<List<TagResponseDTO>> GetAllTags()
		{
			var sql = "SELECT Name, Slug FROM Tag";

			using (var con = _connection.GetConnection())
			{
				return (await con.QueryAsync<TagResponseDTO>(sql)).ToList();
			}
		}

		public async Task<TagResponseDTO?> GetTagsBySlug(string slug)
		{
			var sql = "SELECT Name, Slug FROM Tag WHERE Slug = @Slug";

			using (var con = _connection.GetConnection())
			{
				return await con.QueryFirstOrDefaultAsync<TagResponseDTO>(sql, new { slug });
			}
		}

		public async Task CreateTag(Tag tag)
		{
			var sql = "INSERT INTO Tag (Name, Slug) VALUES (@Name, @Slug)";

			using (var con = _connection.GetConnection())
			{
				await con.ExecuteAsync(sql, new { tag.Name, tag.Slug });
			}
		}

		public async Task UpdateTag(Tag tag)
		{
			var sql = "UPDATE Tag SET Name = @Name WHERE Slug = @Slug";

			using (var con = _connection.GetConnection())
			{
				await con.ExecuteAsync(sql, new { tag.Name, tag.Slug });
			}
		}

		public async Task DeleteTag(string slug)
		{
			var sql = "DELETE FROM Tag WHERE Slug = @Slug)";

			using (var con = _connection.GetConnection())
			{
				await con.ExecuteAsync(sql, new { slug });
			}
		}
	}
}
