using Blog.Controllers.Contracts;
using Blog.Models.DTOs;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase, ICategoryController
{
	private CategoryService _categoryService;

	public CategoryController(CategoryService categoryService)
	{
		_categoryService = categoryService;
	}

	[HttpGet]
	[Route("HeartBeat")]
	public ActionResult HeartBeat()
	{
		return Ok("Still alive!");
	}

	[HttpGet]
	[Route("GetAll")]
	public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategories()
	{
		var categories = await _categoryService.GetAllCategoriesAsync();

		return Ok(categories);
	}

	[HttpGet]
	[Route("Get/{slug}")]
	public async Task<ActionResult<CategoryResponseDTO>> GetBySlug(string slug)
	{
		try
		{
			var category = await _categoryService.GetBySlugAsync(slug);

			if (category is null)
				return NotFound();

			return Ok(category);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}

	}

	[HttpPost]
	[Route("Create")]
	public async Task<ActionResult> CreateCategory(CategoryRequestDTO category)
	{
		try
		{
			await _categoryService.CreateSlugAsync(category);
			return Created();
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPut]
	[Route("Update/{slug}")]
	public async Task<ActionResult<CategoryResponseDTO>> UpdateCategory(string slug, CategoryRequestDTO category)
	{
		try
		{
			if (await _categoryService.GetBySlugAsync(slug) is null)
				return NotFound("Register not found!");

			await _categoryService.UpdateCategoryAsync(slug, category);
			return Ok(await _categoryService.GetBySlugAsync(slug));
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpDelete]
	[Route("Delete/{slug}")]
	public async Task<ActionResult> DeleteCategory(string slug)
	{
		try
		{
			if (await _categoryService.GetBySlugAsync(slug) is null)
				return NotFound("Register not found!");

			await _categoryService.DeleteSlugAsync(slug);
			return NoContent();
		}
		catch(Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

}
