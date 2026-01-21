using Microsoft.EntityFrameworkCore;
using todo_api.Context;
using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Services
{
    public class CategoryService(TodoContext context, IConfiguration configuration) : ICategoryService
    {
        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO createdCategory)
        {
            Category category = new Category()
            {
                Id = createdCategory.Id,
                UserId = createdCategory.UserId,
                Name = createdCategory.Name,
                Color = createdCategory.Color,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };



            context.Categories.Add(category);
            try
            {
                await context.SaveChangesAsync();

            } catch (DbUpdateException ex)
            {
     
                throw new Exception("An error occurred while saving the category to the database.", ex);
            }

            CategoryDTO userDTO = new CategoryDTO()
            {
                Id = category.Id,
                UserId = category.UserId,
                Name = category.Name,
                Color = category.Color,

                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt

            };

            return userDTO;
        }

        public async Task<bool> DeleteCategoryAsync(long id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found.");
            }

            context.Categories.Remove(category);

            try
            {
                await context.SaveChangesAsync();
            } catch (DbUpdateException ex) { 
                    
                throw new Exception("An error occurred while deleting the category from the database.", ex);
            }

            return true;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(int userId)
        {

            var result = await context.Categories
            .Where(category => category.UserId == userId || category.UserId == null)
            .OrderBy(category => category.Id)
            .Select(category => new CategoryDTO
            {
                Id = category.Id,
                UserId = category.UserId,
                Name = category.Name,
                Color = category.Color,

                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt


            }).ToListAsync();

            if(result == null)
            {
                throw new Exception("No categories found for the specified user.");
            }

            return result;
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(long id, CategoryDTO item)
        {
            var category = await context.Categories.FindAsync(id);

            if (category == null)
            {
                throw new Exception("Category not found.");
            }


            category.Id = item.Id;
            category.UserId = item.UserId;
            category.Name = item.Name;
            category.Color = item.Color;

            category.UpdatedAt = DateTime.UtcNow;

            try
            {

                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               throw new Exception("An error occurred while updating the category in the database.");
            }


            var result = new CategoryDTO
            {
                Id = category.Id,
                UserId = category.UserId,
                Name = category.Name,
                Color = category.Color,

                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };

            return result;
        }
    }
}
