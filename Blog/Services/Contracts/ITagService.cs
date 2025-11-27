using Blog.Models.DTOs;

namespace Blog.Services.Contracts;

public interface ITagService
{
	public Task<List<TagResponseDTO>> GetAllTags();
	public Task<TagResponseDTO> GetTagsBySlug(string slug);
	public Task CreateTag(TagRequestDTO tag);
	public Task UpdateTag(string slug, TagRequestDTO tag);
	public Task DeleteTag(string slug);
}
