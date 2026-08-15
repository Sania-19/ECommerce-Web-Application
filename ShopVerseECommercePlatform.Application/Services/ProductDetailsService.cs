using Microsoft.AspNetCore.Http;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using ShopVerseECommercePlatform.Application.RRModels.Product;
using ShopVerseECommercePlatform.Application.RRModels.ProductDetails;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using ShopVerseECommercePlatform.Application.Abstraction.IStorageService;
using ShopVerseECommercePlatform.Application.Abstraction.IUnitOfWork;
using ShopVerseECommercePlatform.Application.RRModels.Files;
using ShopVerseECommercePlatform.Application.Utils.Result;
using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Application.Services
{
    public class ProductDetailsService(IStorageService storageService, IProductDetailsRepository productDetailsRepository,
                                       IUnitOfWork unitOfWork, IAppFilesRepository appFilesRepository) : IProductDetailsService
    {
        #region CREATE
        public async Task<Result<ProductDetailsResponse>> CreateProductDetails(ProductDetailsRequest model)
        {
            (string filePath, string fileName) = await storageService.SaveFileAsync(model.File);

            var productDetails = new ProductDetails
            {
                Discount = model.Discount,
                FileName = fileName,
                FilePath = filePath,
                Quantity = model.Quantity,
                Price = model.Price,
                ProductId = model.ProductId,
            };

            await productDetailsRepository.AddAsync(productDetails);
            var returnVal = await unitOfWork.SaveChangesAsync();

            var productResponse = new ProductDetailsResponse
            {
                Id = productDetails.Id,
                ProductId = productDetails.ProductId,
                Discount = productDetails.Discount,
                Quantity = productDetails.Quantity,
                FileName = fileName,
                FilePath = filePath,
                CreatedOn = productDetails.CreatedOn,
                Price = productDetails.Price,

            };
            if (returnVal > 0)
                return Result<ProductDetailsResponse>.Success(productResponse, "Product Details Added Successfully!", StatusCodes.Status200OK);

            return Result<ProductDetailsResponse>.Failure("Something went wrong", StatusCodes.Status500InternalServerError);
        }
        #endregion

        #region READ
        public async Task<Result<IEnumerable<ProductDetailsResponse>>> GetDetailsByProductId(Guid productId)
        {
            var products = await productDetailsRepository.FindByAsync(x => x.ProductId == productId);
            if (products is null)
                return Result<IEnumerable<ProductDetailsResponse>>.Failure("No Product Details Found", StatusCodes.Status404NotFound);

            var productResponse = products.Select(x => new ProductDetailsResponse
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Discount = x.Discount,
                FileName = x.FileName,
                FilePath = x.FilePath,
                Price = x.Price,
                Quantity = x.Quantity,
            }).ToList();

            return Result<IEnumerable<ProductDetailsResponse>>.Success(productResponse);
        }

        public async Task<Result<ProductFullDetailsWithFilesResponse>> GetProductDetailsWithFilesByProductId(Guid pdid)
        {
            var productDetails = await productDetailsRepository.GetProductDetailsByPdId(pdid);
            if (productDetails is null)
            {
                return Result<ProductFullDetailsWithFilesResponse>.Failure("Product details not found");
            }
            return Result<ProductFullDetailsWithFilesResponse>.Success(productDetails);
        }

        public async Task<Result<ProductDetailsResponse>> GetDetailsById(Guid id)
        {
            var detail = await productDetailsRepository.GetByIdAsync(id);

            if (detail is null)
                return Result<ProductDetailsResponse>.Failure("No Product Detail Found", StatusCodes.Status404NotFound);
            return Result<ProductDetailsResponse>.Success(new ProductDetailsResponse
            {
                Id = detail.Id,
                Discount = detail.Discount,
                FileName = detail.FileName,
                FilePath = detail.FilePath,
                Price = detail.Price,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
            });
        }
        #endregion

        #region UPDATE
        public async Task<Result<ProductDetailsResponse>> UpdateProductDetails(Guid id, ProductDetailsUpdateRequest model)
        {
            var productDetails = await productDetailsRepository.GetByIdAsync(id);
            if (productDetails == null)
            {
                return Result<ProductDetailsResponse>.Failure("Product details not found");
            }


            (string filePath, string fileName) = await storageService.UpdateFileAsync(model.File, productDetails.FileName);
            var updtaedDetails = new ProductDetails
            {
                Id = id,
                Price = model.Price,
                Discount = model.Discount,
                Quantity = model.Quantity,
                FilePath = filePath,
                FileName = fileName,
                ProductId = productDetails.ProductId,
            };

            var productResponse = new ProductDetailsResponse
            {
                Id = updtaedDetails.Id,
                ProductId = updtaedDetails.ProductId,
                Discount = updtaedDetails.Discount,
                Quantity = updtaedDetails.Quantity,
                FileName = fileName,
                FilePath = filePath,
                Price = updtaedDetails.Price,

            };
            await productDetailsRepository.UpdateAsync(updtaedDetails);
            var returnVal = await unitOfWork.SaveChangesAsync();

            if (returnVal > 0)
                return Result<ProductDetailsResponse>.Success(productResponse, "Product Details Updated Successfully", StatusCodes.Status200OK);

            return Result<ProductDetailsResponse>.Failure("Something went wrong", StatusCodes.Status500InternalServerError);

        }
        #endregion

        #region DELETE
        public async Task<Result<ProductDetailsResponse>> DeleteDetailsById(Guid id)
        {
            var productDetails = await productDetailsRepository.GetByIdAsync(id);
            if (productDetails == null)
            {
                return Result<ProductDetailsResponse>.Failure("Product details not found");
            }
            await productDetailsRepository.DeleteAsync(id);
            var returnValue = await unitOfWork.SaveChangesAsync();
            if (returnValue <= 0)
            {
                return Result<ProductDetailsResponse>.Failure("Failed to delete product details");
            }

            var product = new ProductDetailsResponse
            {
                Id = productDetails.Id,
                Price = productDetails.Price,
                Discount = productDetails.Discount,
                FilePath = productDetails.FilePath,
                FileName = productDetails.FileName,
                ProductId = productDetails.ProductId,
                Quantity = productDetails.Quantity,
                CreatedOn = productDetails.CreatedOn
            };

            return Result<ProductDetailsResponse>.Success(product);
        }
        #endregion

        #region UPLOAD FILES
        public async Task<Result<IEnumerable<FileResponse>>> UploadProductDetailsImages(UpdateProductImageRequest model)
        {
            var productDetails = await productDetailsRepository.GetByIdAsync(model.ProductDetailId);
            if (productDetails is null)
            {
                return Result<IEnumerable<FileResponse>>.Failure("Product details not found");
            }

            if (model.Files.Count <= 0)
            {
                return Result<IEnumerable<FileResponse>>.Failure("No Image Found");
            }
            var (filesResponse, totalUpload) = await storageService.SaveFilesAsync(model.Files);

            if (totalUpload <= 0)
            {
                return Result<IEnumerable<FileResponse>>.Failure("Failed to update Product Image");
            }
            List<AppFiles> appFiles = new();
            foreach (var file in filesResponse)
            {
                AppFiles appFile = new();
                appFile.FileName = file.FileName;
                appFile.FilePath = file.FilePath;
                appFile.AppModule = AppModule.Product;
                appFile.EntityId = model.ProductDetailId;
                appFile.IsVideo = false;
                appFiles.Add(appFile);
            }
            await appFilesRepository.AddRangeAsync(appFiles);

            var returnVal = await unitOfWork.SaveChangesAsync();

            if (returnVal <= 0)
            {
                return Result<IEnumerable<FileResponse>>.Failure("Failed to Update Product Image!");
            }

            return Result<IEnumerable<FileResponse>>.Success(filesResponse);
        }
        #endregion
    }
}
