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

	}
}
