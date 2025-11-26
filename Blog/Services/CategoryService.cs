using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;

namespace Blog.Services;

public class CategoryService
{
	private CategoryRepository _categoryRepository;

	public CategoryService(CategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}
	public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
	{
		var categories = await _categoryRepository.GetAllCategoriesAsync();

		return [.. categories];
	}

	public async Task<Category?> GetBySlugAsync(string slug)
	{
		return await _categoryRepository.GetBySlugAsync(slug);
	}
		
	public async Task CreateSlugAsync(CategoryRequestDTO category)
	{
		var newCategory = new Category(
			category.Name, 
			category.Name.ToLower().Replace(" ", "-")
			);

		await _categoryRepository.CreateCategoryAsync(newCategory);
	}

	public async Task DeleteSlugAsync(string slug)
	{
		await _categoryRepository.DeleteCategoryAsync(slug);
	}
}