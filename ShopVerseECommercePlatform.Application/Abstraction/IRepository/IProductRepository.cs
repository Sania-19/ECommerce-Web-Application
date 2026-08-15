using ShopVerseECommercePlatform.Application.RRModels.Product;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IRepository
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        public Task<IEnumerable<ProductResponse>> GetProductsByCatId(Guid categoryId);
        public Task<int> InsertProductWithDetails(ProductWithDetails model);
        public Task<IEnumerable<ProductResponseWithJsonResult>> GetProductsByCatIdWithJsonResult(Guid categoryId);
        public Task<int> DeleteProduct(Guid id);

       
    }
}
