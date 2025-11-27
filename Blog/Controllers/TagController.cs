using Blog.Controllers.Contracts;
using Blog.Models;
using Blog.Models.DTOs;
using Blog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase, ITagController
{
	private readonly TagService _service;

	public TagController(TagService service)
	{
		_service = service;
	}

	[HttpGet]
	[Route("HeartBeat")]
	public ActionResult HeartBeat()
	{
		return Ok("Still alive!");
	}

	[HttpGet]
	[Route("GetAll")]
	public async Task<ActionResult<List<TagResponseDTO>>> GetAllTags()
	{
		try
		{
			var tags = await _service.GetAllTags();

			if (tags is null)
				return NotFound("Register not found!");

			return Ok(tags);

		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
		
	}

	[HttpGet]
	[Route("Get/{slug}")]
	public async Task<ActionResult<TagResponseDTO>> GetTagsBySlug(string slug)
	{
		try
		{
			var tag = await _service.GetTagsBySlug(slug);

			if (tag is null)
				return NotFound("Register not found!");

			return Ok(tag);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPost]
	[Route("Create")]
	public async Task<ActionResult> CreateTag(TagRequestDTO tag)
	{
		try
		{
			await _service.CreateTag(tag);
			return Created();
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPut]
	[Route("Update/{slug}")]
	public async Task<ActionResult<TagResponseDTO>> UpdateTag(string slug, TagRequestDTO tag)
	{
		try
		{
			if (await _service.GetTagsBySlug(slug) is null)
				return NotFound("Register not found!");

			await _service.UpdateTag(slug, tag);
			return Ok(await _service.GetTagsBySlug(slug));
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpDelete]
	[Route("Delete/{slug}")]
	public async Task<ActionResult> DeleteTag(string slug)
	{
		try
		{
			if (await _service.GetTagsBySlug(slug) is null)
				return NotFound("Register not found!");

			await _service.DeleteTag(slug);
			return NoContent();
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

}
