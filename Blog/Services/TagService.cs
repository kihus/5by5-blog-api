using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;
using Blog.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Services;

public class TagService : ITagService
{
	private readonly TagRepository _repository;

	public TagService(TagRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<TagResponseDTO>> GetAllTags()
	{
		return await _repository.GetAllTags();
	}

	public async Task<TagResponseDTO?> GetTagsBySlug(string slug)
	{
		return await _repository.GetTagsBySlug(slug);
	}

	public async Task CreateTag(TagRequestDTO tag)
	{
		var newTag = new Tag(
			tag.Name,
			tag.Name.ToLower().Replace(" ", "-")
			);

		await _repository.CreateTag(newTag);
	}

	public async Task UpdateTag(string slug, TagRequestDTO tag)
	{
		var newTag = new Tag(
			tag.Name,
			slug
			);

		await _repository.UpdateTag(newTag);
	}
	public async Task DeleteTag(string slug)
	{
		await _repository.DeleteTag(slug);
	}
}
