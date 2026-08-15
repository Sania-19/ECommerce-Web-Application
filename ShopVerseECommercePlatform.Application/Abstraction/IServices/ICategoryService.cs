
using ShopVerseECommercePlatform.Application.RRModels.Category;
using ShopVerseECommercePlatform.Application.Utils.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IServices
{
    public interface ICategoryService
    {
        Task<Result<CategoryResponse>> CreateCategory(CategoryRequest model);
        Task<Result<CategoryResponse>> UpdateCategory(UpdateCategoryRequest model,Guid id);
        Task<Result<IEnumerable<CategoryResponse>>> GetAllCategories();
        Task<Result<CategoryResponse>> GetCategoryById(Guid id);
        Task<Result<IEnumerable<CategoryResponse>>> FindCategoryByName(string catName);
        Task<Result<IEnumerable<CategoryResponse>>> GetActiveCategories();
        Task<Result<CategoryResponse>> DeleteCategory(Guid id);
        Task<Result<CategoryResponse>> RestoreCategory(Guid id);

    }
}
