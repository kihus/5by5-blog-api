using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories.Contracts;
using Dapper;

namespace Blog.Repositories;

public class PostRepository : IPostRepository
{
	private readonly ConnectionDB _connection;

	public PostRepository(ConnectionDB connection)
	{
		_connection = connection;
	}

	public async Task<List<PostResponseDTO>> GetAllPosts()
	{
		var sql = @"SELECT p.Title, p.Summary, p.Body, p.Slug, p.CreateDate AS CreatedAt, p.LastUpdateDate AS UpdatedAt, 
						   u.[Name] AS AuthorName, 
						   c.[Name] AS CategoryName 
					FROM Post p 
					JOIN [User] u ON p.AuthorId = u.Id JOIN 
					[Category] c ON p.CategoryId = c.Id";

		using (var con = _connection.GetConnection())
		{
			return (await con.QueryAsync<PostResponseDTO>(sql)).ToList();
		}
	}

	public async Task<PostResponseDTO?> GetPostBySlug(string slug)
	{
		var sql = @"SELECT p.Title, p.Summary, p.Body, p.Slug, p.CreateDate AS CreatedAt, p.LastUpdateDate AS UpdatedAt, 
						   u.[Name] AS AuthorName, 
						   c.[Name] AS CategoryName
					FROM Post p 
					JOIN [User] u 
					ON p.AuthorId = u.Id JOIN 
					[Category] c 
					ON p.CategoryId = c.Id
					WHERE p.Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			return await con.QueryFirstOrDefaultAsync<PostResponseDTO>(sql, new { slug });
		}
	}

	public async Task<List<PostTagResponseDTO>> GetAllPostsTags()
	{
		IEnumerable<PostTagResponseDTO> postTags = new List<PostTagResponseDTO>();
		var sql = @"SELECT p.[Id], p.Title, p.Summary, p.Body, p.Slug, p.CreateDate, p.LastUpdateDate, u.[Name] AS AuthorName, c.[Name] AS CategoryName, t.[Id], t.[Name], t.Slug 
					FROM [Post] p
					JOIN [User] u 
					ON p.AuthorId = u.Id JOIN 
					[Category] c 
					ON p.CategoryId = c.Id
					JOIN [PostTag] pt
					ON p.Id = pt.PostId
					JOIN [Tag] t
					ON t.Id = pt.TagId";

		using (var con = _connection.GetConnection())
		{
			postTags = await con.QueryAsync<PostTagResponseDTO, PostTagDTO, PostTagResponseDTO>(
				sql,
				(post, tag) => { post.Tags.Add(tag); return post; },
				splitOn: "Id"
				);
		}
		return postTags.ToList();
	}

	public async Task<List<PostTagResponseDTO>> GetPostTagBySlug(string slug)
	{
		IEnumerable<PostTagResponseDTO> postTag = new List<PostTagResponseDTO>();
		var sql = @"SELECT p.[Id], p.Title, p.Summary, p.Body, p.Slug, p.CreateDate, p.LastUpdateDate, u.[Name] AS AuthorName, c.[Name] AS CategoryName, t.[Id], t.[Name], t.Slug 
					FROM [Post] p
					JOIN [User] u 
					ON p.AuthorId = u.Id JOIN 
					[Category] c 
					ON p.CategoryId = c.Id
					JOIN [PostTag] pt
					ON p.Id = pt.PostId
					JOIN [Tag] t
					ON t.Id = pt.TagId
					WHERE p.Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			postTag = await con.QueryAsync<PostTagResponseDTO, PostTagDTO, PostTagResponseDTO>(
				sql,
				(post, tag) => { post.Tags.Add(tag); return post; },
				new { slug },
				splitOn: "Id"
				);
		}
		return postTag.ToList();
	}

	public async Task CreatePost(Post post)
	{
		var sql = @"INSERT INTO Post (CategoryId, AuthorId, Title, Summary, Body, Slug, CreateDate, UpdateDate) 
					VALUES (@CategoryId, @AuthorId, @Title, @Sumary, @Body, GETDATE(), GETDATE())";

		using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { post.CategoryId, post.AuthorId, post.Title, post.Summary, post.Body, post.Slug });
		}
	}

	public async Task UpdatePost(Post post)
	{
		var sql = @"UPDATE Post SET Title = @Title, Summary = @Summary, Body = @Body, UpdateDate = GETDATE() WHERE Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { post.Title, post.Summary, post.Body, post.Slug });
		}
	}

	public async Task DeletePost(string slug)
	{
		var sql = @"DELETE FROM Post WHERE Slug = @Slug";

		using (var con = _connection.GetConnection())
		{
			await con.ExecuteAsync(sql, new { slug });
		}
	}

}
