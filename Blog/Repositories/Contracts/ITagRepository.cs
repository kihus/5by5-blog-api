using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Repositories.Contracts;

public interface ITagRepository
{
	public Task<List<TagResponseDTO>> GetAllTags();
	public Task<TagResponseDTO?> GetTagsBySlug(string slug);
	public Task CreateTag(Tag tag);
	public Task UpdateTag(Tag tag);
	public Task DeleteTag(string slug);
}
