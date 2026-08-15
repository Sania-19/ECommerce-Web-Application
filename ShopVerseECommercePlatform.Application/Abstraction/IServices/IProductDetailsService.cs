using ShopVerseECommercePlatform.Application.RRModels.ProductDetails;
using ShopVerseECommercePlatform.Application.RRModels.Files;
using ShopVerseECommercePlatform.Application.Utils.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IServices
{
    public interface IProductDetailsService
    {
        Task<Result<IEnumerable<ProductDetailsResponse>>> GetDetailsByProductId(Guid productId);
        Task<Result<ProductFullDetailsWithFilesResponse>> GetProductDetailsWithFilesByProductId(Guid pdid);
        Task<Result<ProductDetailsResponse>> CreateProductDetails(ProductDetailsRequest model);
        Task<Result<ProductDetailsResponse>> GetDetailsById(Guid id);
        Task<Result<ProductDetailsResponse>> UpdateProductDetails(Guid id,ProductDetailsUpdateRequest model);
        Task<Result<IEnumerable<FileResponse>>> UploadProductDetailsImages(UpdateProductImageRequest model);
        Task<Result<ProductDetailsResponse>> DeleteDetailsById(Guid id);
    }
}
