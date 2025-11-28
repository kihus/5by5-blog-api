using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Repositories.Contracts;

public interface IPostRepository
{
	public Task<List<PostResponseDTO>> GetAllPosts();
	public Task<PostResponseDTO?> GetPostBySlug(string slug);
	public Task<List<PostTagResponseDTO>> GetAllPostsTags();
	public Task<List<PostTagResponseDTO>> GetPostTagBySlug(string slug);
	public Task CreatePost(Post post);
	public Task UpdatePost(Post post);
	public Task DeletePost(string slug);
}
