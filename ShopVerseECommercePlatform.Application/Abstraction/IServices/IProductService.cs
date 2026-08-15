using ShopVerseECommercePlatform.Application.RRModels.Product;
using ShopVerseECommercePlatform.Application.Utils.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IServices
{
    public interface IProductService
    {
        Task<Result<ProductResponseWithDetails>> CreateProduct(ProductRequest model);
        Task<Result<ProductResponseWithDetails>> AddProduct(ProductRequest model);
        Task<Result<IEnumerable<ProductResponseWithDetails>>> GetProductsByCategoryId(Guid categoryId);
        Task<Result<IEnumerable<ProductResponse>>> GetProductsByCatId(Guid categoryId);
        Task<Result<IEnumerable<ProductResponseWithJsonResult>>> GetProductsByCatIdWithJsonResult(Guid catId);
        Task<Result<ProductResponse>> GetProductById(Guid id);
        Task<Result<ProductResponse>> UpdateProduct(Guid id, UpdateProductRequest model);
        Task<Result<string>> DeleteProduct(Guid id);

    }
}
