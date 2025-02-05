using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateCategoryAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                Description = request.Description,
                Title =  request.Title,
                UserId = request.UserId!
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();  
            return new Response<Category?>(category,201);
        }
        catch (Exception ex) 
        {
            return new Response<Category?>(null,500,$"Erro ao criar categoria. {ex.Message}");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try 
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada.");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(null, 204);
        }
        catch (Exception ex) 
        {
            return new Response<Category?>(null, 500, $"Erro ao excluir categoria. {ex.Message}");
        }
    }

    public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context
                            .Categories
                            .AsNoTracking()
                            .Where(x => x.UserId == request.UserId);

            var countCategories = await query.CountAsync();

            var categories = await query
                                    .Skip((request.PagedNumber - 1) * request.PageSize)
                                    .Take(request.PageSize)
                                    .ToListAsync();

            return new PagedResponse<List<Category>?>(categories, countCategories,request.PagedNumber,request.PageSize);

        }
        catch (Exception ex)
        {
            return new PagedResponse<List<Category>?>(null,500,$"Erro ao retornar categorias.{ex.Message}");
        }
    }

    public async Task<Response<Category?>> GetAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context
                                .Categories
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return category is null ?
                   new Response<Category?>(null, 404, "Categoria não encontrada."):
                   new Response<Category?>(category, 200);

        }
        catch (Exception ex)
        {
            return new Response<Category?>(null, 500,$"Erro ao retornar a categoria. {ex.Message}");
        } 
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada.");

            category.Description = request.Description;
            category.Title = request.Title;

            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(category, 200);
        }
        catch (Exception ex)
        {
            return new Response<Category?>(null, 500, $"Erro ao atualizar categoria. {ex.Message}");
        }
    }
}
