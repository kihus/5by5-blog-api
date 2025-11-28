using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;
using Blog.Services.Contracts;

namespace Blog.Services;

public class PostService : IPostService
{
	private readonly PostRepository _repository;

	public PostService(PostRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<PostResponseDTO>> GetAllPosts()
	{
		return await _repository.GetAllPosts();
	}

	public async Task<PostResponseDTO?> GetPostBySlug(string slug)
	{
		return await _repository.GetPostBySlug(slug);
	}

	public async Task<List<PostTagResponseDTO>> GetAllPostTags()
	{
		var postTags = await _repository.GetAllPostsTags();

		var result = postTags.GroupBy(p => p.Id).Select(g =>
		{
			var groupedPost = g.First();
			groupedPost.Tags = (g.Select(t => t.Tags.Single())).ToList();
			return groupedPost;
		});

		return result.ToList();
	}

	public async Task<PostTagResponseDTO?> GetPostTagBySlug(string slug)
	{
		var postTag = await _repository.GetPostTagBySlug(slug);

		var result = postTag.GroupBy(p => p.Id).Select(g =>
		{
			var groupedPost = g.First();
			groupedPost.Tags = (g.Select(t => t.Tags.Single())).ToList();
			return groupedPost;
		});

		return result.FirstOrDefault(r => r.Slug == slug);
	}

	public async Task CreatePost(PostRequestDTO postRequest)
	{
		var post = new Post(
			postRequest.CategoryId,
			postRequest.AuthorId,
			postRequest.Title,
			postRequest.Summary,
			postRequest.Body,
			postRequest.Title.ToLower().Replace(" ", "-")
			);

		await _repository.CreatePost(post);
	}

	public async Task UpdatePost(string slug, PostRequestDTO postRequest)
	{
		var post = new Post(
			postRequest.CategoryId,
			postRequest.AuthorId,
			postRequest.Title,
			postRequest.Summary,
			postRequest.Body,
			slug
			);

		await _repository.UpdatePost(post);
	}

	public async Task DeletePost(string slug)
	{
		await _repository.DeletePost(slug);
	}
}
