using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Services.Contracts;

public interface ICategoryService
{
	public Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
	public Task<CategoryResponseDTO?> GetBySlugAsync(string slug);
	public Task CreateSlugAsync(CategoryRequestDTO category);
	public Task UpdateCategoryAsync(string slug, CategoryRequestDTO category);
	public Task DeleteSlugAsync(string slug);
}
