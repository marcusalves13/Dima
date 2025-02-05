﻿using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;
public interface ICategoryHandler
{
    Task<Response<Category?>> CreateCategoryAsync(CreateCategoryRequest request);
    Task<Response<Category?>> GetAsync(GetCategoryByIdRequest request);
    Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request);
    Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
}
