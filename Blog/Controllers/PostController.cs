using Blog.Models.DTOs;
using Blog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly PostService _service;

		public PostController(PostService service)
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
		public async Task<ActionResult<List<PostResponseDTO>>> GetAll()
		{
			try
			{
				var posts = await _service.GetAllPosts();

				if (posts.Count is 0)
					return NotFound("Register not found!");

				return Ok(posts);

			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet]
		[Route("post-tag")]
		public async Task<ActionResult<List<PostTagResponseDTO>>> GetAllPostTags()
		{
			try
			{
				var postTags = await _service.GetAllPostTags();

				if (postTags.Count is 0)
					return NotFound("Register not found!");

				return Ok(postTags);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet]
		[Route("{slug}/tag")]
		public async Task<ActionResult<PostTagResponseDTO>> GetPostTagBySlug(string slug)
		{
			try
			{
				var postTags = await _service.GetPostTagBySlug(slug);

				if (postTags is null)
					return NotFound("Register not found!");

				return Ok(postTags);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
