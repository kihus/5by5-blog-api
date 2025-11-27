using Blog.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.Contracts;

public interface ITagController
{
	public Task<ActionResult<List<TagResponseDTO>>> GetAllTags();
	public Task<ActionResult<TagResponseDTO>> GetTagsBySlug(string slug);
	public Task<ActionResult> CreateTag(TagRequestDTO tag);
	public Task<ActionResult<TagResponseDTO>> UpdateTag(string slug, TagRequestDTO tag);
	public Task<ActionResult> DeleteTag(string slug);
}
