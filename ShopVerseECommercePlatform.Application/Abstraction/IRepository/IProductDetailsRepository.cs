using ShopVerseECommercePlatform.Application.RRModels.ProductDetails;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IRepository
{
    public interface IProductDetailsRepository: IBaseRepository<ProductDetails>
    {
        Task<ProductFullDetailsWithFilesResponse> GetProductDetailsByPdId(Guid pdid);
    }
    
}
