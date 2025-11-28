using Blog.Models.DTOs;

namespace Blog.Services.Contracts;

public interface IPostService
{
	public Task<List<PostResponseDTO>> GetAllPosts();
	public Task<PostResponseDTO?> GetPostBySlug(string slug);
	public Task<List<PostTagResponseDTO>> GetAllPostTags();
	public Task<PostTagResponseDTO?> GetPostTagBySlug(string slug);
	public Task CreatePost(PostRequestDTO postRequest);
	public Task UpdatePost(string slug, PostRequestDTO postRequest);
	public Task DeletePost(string slug);
}
