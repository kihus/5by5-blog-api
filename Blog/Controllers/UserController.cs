	using Blog.Models.DTOs;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserService _service;

		public UserController(UserService service)
		{
			_service = service;
		}

		[HttpGet]
		[Route("HeartBeat")]
		public ActionResult GetAll()
		{
			return Ok("Still Alive");
		}

		[HttpGet]
		[Route("GetAll")]
		public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsers()
		{
			try
			{
				var users = await _service.GetAllUsersAsync();

				if (users.Count is 0)
					return NotFound("Register not found!");

				return users;
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet]
		[Route("Get/{slug}")]
		public async Task<ActionResult<UserResponseDTO>> GetUserBySlug(string slug)
		{
			try
			{
				var user = await _service.GetUserBySlug(slug);

				if (user is null)
					return NotFound("Register not found!");

				return user;
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		[Route("Create")]
		public async Task<ActionResult> CreateUser(UserRequestDTO user)
		{
			try
			{
				await _service.CreateUserAsync(user);
				return Created();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut]
		[Route("Update/{slug}")]
		public async Task<ActionResult> UpdateUser(string slug, UserRequestDTO user)
		{
			try
			{
				if (await _service.GetUserBySlug(slug) is null)
					return NotFound("Register not found!");

				await _service.UpdateUser(slug, user);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete]
		[Route("Delete/{slug}")]
		public async Task<ActionResult> DeleteUser(string slug, UserLoginDTO user)
		{
			try
			{
				if (await _service.GetUserBySlug(slug) is null)
					return NotFound("Register not found!");

				await _service.DeleteUser(slug, user);
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
