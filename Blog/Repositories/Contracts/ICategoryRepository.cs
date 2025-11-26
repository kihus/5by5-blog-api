using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Repositories.Contracts
{
	public interface ICategoryRepository
	{
		public Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
		public Task<Category?> GetBySlugAsync(string slug);
		public Task CreateCategoryAsync(Category category);
		public Task DeleteCategoryAsync(string slug);
	}
}
