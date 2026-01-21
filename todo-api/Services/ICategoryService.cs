using todo_api.Models.Dtos;

namespace todo_api.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(int userId);
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto);
        Task<CategoryDTO> UpdateCategoryAsync(long id, CategoryDTO categoryDto);
        Task<bool> DeleteCategoryAsync(long id);
    }
}
