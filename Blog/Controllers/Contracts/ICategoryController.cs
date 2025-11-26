using Blog.Models;
using Blog.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.Contracts;

public interface ICategoryController
{
	public ActionResult HeartBeat();
	public Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategories();
	public Task<ActionResult<Category>> GetBySlug(string slug);
	public Task<ActionResult> CreateCategory(CategoryRequestDTO category);
	public Task<ActionResult> DeleteCategory(string slug);
}
