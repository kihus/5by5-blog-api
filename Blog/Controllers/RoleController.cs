using Blog.Models;
using Blog.Models.DTOs;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
	private readonly RoleService _service;

	public RoleController(RoleService service)
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
	public async Task<ActionResult<List<RoleResponseDTO>>> GetAll()
	{
		try
		{
			var roles = await _service.GetAllAsync();

			if (roles.Count is 0)
				return NotFound("Register not found!");

			return roles;
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpGet]
	[Route("Get/{slug}")]
	public async Task<ActionResult<RoleResponseDTO>> GetBySlug(string slug)
	{
		try
		{
			var role = await _service.GetRoleBySlugAsync(slug);

			if (role is null)
				return NotFound("Register not found!");

			return role;
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpGet]
	[Route("GetAll/Role-User")]
	public async Task<ActionResult<List<RoleUserResponseDTO>>> GetAllRolesUsers()
	{
		try
		{
			var role = await _service.GetAllRolesUsers();

			if (role is null)
				return NotFound("Register not found!");

			return role;
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpGet]
	[Route("GetAll/Role-User/{slug}")]
	public async Task<ActionResult<RoleUserResponseDTO>> GetRoleUserBySlug(string slug)
	{
		try
		{
			var role = await _service.GetRoleUserBySlug(slug);

			if (role is null)
				return NotFound("Register not found!");

			return role;
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}



	[HttpPost]
	[Route("Create")]
	public async Task<ActionResult> CreateRole(RoleRequestDTO role)
	{
		try
		{
			await _service.CreateRoleAsync(role);
			return Ok("Succesful!");
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPut]
	[Route("Update/{slug}")]
	public async Task<ActionResult> UpdateRole(string slug, RoleRequestDTO roleRequest)
	{
		try
		{
			var role = await _service.GetRoleBySlugAsync(slug);

			if (role is null)
				return NotFound("Register not found!");

			await _service.UpdateRoleAsync(slug, roleRequest);
			return Ok("Succesful!");
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpDelete]
	[Route("Delete/{slug}")]
	public async Task<ActionResult> DeleteRole(string slug)
	{
		try
		{
			var role = await _service.GetRoleBySlugAsync(slug);

			if (role is null)
				return NotFound("Register not found!");

			await _service.DeleteRoleAsync(slug);
			return NoContent();
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}

	}

}
