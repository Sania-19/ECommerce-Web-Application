using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using ShopVerseECommercePlatform.Application.Abstraction.IStorageService;
using ShopVerseECommercePlatform.Application.Abstraction.IUnitOfWork;
using ShopVerseECommercePlatform.Application.Utils.Result;
using ShopVerseECommercePlatform.Domain.Entities;
using Microsoft.AspNetCore.Http;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.RRModels.Product;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;


namespace ShopVerseECommercePlatform.Application.Services
{
    public class ProductService(IProductRepository productRepository, IProductDetailsRepository productDetailsRepository,
                                IStorageService storageService, IUnitOfWork unitOfWork) : IProductService
    {
        #region CREATE

        // USING SQL QUERY
        public async Task<Result<ProductResponseWithDetails>> AddProduct(ProductRequest model)
        {
            if (model.File is null)
                return Result<ProductResponseWithDetails>.Failure("Product File is required", StatusCodes.Status400BadRequest);

            (string filePath, string fileName) = await storageService.SaveFileAsync(model.File);
            ProductWithDetails productWithDetails = new()
            {
                CreatedOn = DateTimeOffset.Now,
                FileName = fileName,
                FilePath = filePath,
                ProductDetailId = Guid.CreateVersion7(),
                ProductId = Guid.CreateVersion7(),
                ProductRequest = model,
            };
            var returnVal = await productRepository.InsertProductWithDetails(productWithDetails);
            if (returnVal > 0)
            {
                var productResponse = new ProductResponseWithDetails()
                {
                    Id = productWithDetails.ProductId,
                    Brand = model.Brand,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    Discount = model.Discount,
                    FileName = fileName,
                    FilePath = filePath,
                    Price = model.Price,
                    ProductDetailId = productWithDetails.ProductDetailId,
                    Title = model.Title,
                    Units = model.Units,
                };

                return Result<ProductResponseWithDetails>.Success(productResponse);
            }
            return Result<ProductResponseWithDetails>.Failure("Something went wrong", StatusCodes.Status500InternalServerError);

        }

        // USING TRANSACTION
        public async Task<Result<ProductResponseWithDetails>> CreateProduct(ProductRequest model)
        {
            var product = new Product()
            {
                Title = model.Title,
                Brand = model.Brand,
                Description = model.Description,
                Units = model.Units,
                CategoryId = model.CategoryId,
            };

            (string filePath, string fileName) = await storageService.SaveFileAsync(model.File);

            var productDetails = new ProductDetails()
            {
                Price = model.Price,
                Discount = model.Discount,
                ProductId = product.Id,
                FileName = fileName,
                FilePath = filePath

            };

            using var transaction = unitOfWork.BeginTransaction();
            await productRepository.AddAsync(product);

            await productDetailsRepository.AddAsync(productDetails);
            var returnVal = await unitOfWork.SaveChangesAsync();

            if (returnVal > 0)
            {
                transaction.Commit();
                var productResponse = new ProductResponseWithDetails()
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Brand = product.Brand,
                    Units = product.Units,
                    CategoryId = product.CategoryId,
                    Discount = productDetails.Discount,
                    Price = productDetails.Price,
                    ProductDetailId = productDetails.Id,
                    FileName = productDetails.FileName,
                    FilePath = productDetails.FilePath
                };
                return Result<ProductResponseWithDetails>.Success(productResponse);
            }
            else
                transaction.Rollback();

            return Result<ProductResponseWithDetails>.Failure("Something went wrong", StatusCodes.Status500InternalServerError);
        }
        #endregion

        #region READ
        public async Task<Result<IEnumerable<ProductResponseWithDetails>>> GetProductsByCategoryId(Guid categoryId)
        {
            var products = (await productRepository
                           .FindByAsync(x => x.CategoryId == categoryId))
                           .ToList();
            if (products is null)
                return Result<IEnumerable<ProductResponseWithDetails>>.Failure("No Products Found", StatusCodes.Status404NotFound);

            List<ProductResponseWithDetails> list = new();
            foreach (var product in products)
            {
                ProductResponseWithDetails productResponse = new();
                var productDetails = await productDetailsRepository.FirstOrDefaultAsync(x => x.ProductId == product.Id);
                productResponse.Id = product.Id;
                productResponse.Title = product.Title;
                productResponse.Description = product.Description;
                productResponse.Brand = product.Brand;
                productResponse.Units = product.Units;
                productResponse.CategoryId = product.CategoryId;
                productResponse.Discount = productDetails.Discount;
                productResponse.Price = productDetails.Price;
                productResponse.ProductDetailId = productDetails.Id;
                productResponse.FileName = productDetails.FileName;
                productResponse.FilePath = productDetails.FilePath;
                list.Add(productResponse);
            }
            return Result<IEnumerable<ProductResponseWithDetails>>.Success(list);
        }

        //USING SQL QUERY (JOINS)
        public async Task<Result<IEnumerable<ProductResponse>>> GetProductsByCatId(Guid categoryId)
        {
            var products = await productRepository.GetProductsByCatId(categoryId);

            if (products is null)
                return Result<IEnumerable<ProductResponse>>.Failure("No products found!!", StatusCodes.Status404NotFound);
            return Result<IEnumerable<ProductResponse>>.Success(products);
        }

        public async Task<Result<ProductResponse>> GetProductById(Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return Result<ProductResponse>.Failure("No Corresponding Product found", StatusCodes.Status404NotFound);
            }
            var productResponse = new ProductResponse()
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Brand = product.Brand,
                Units = product.Units,
                CategoryId = product.CategoryId
            };
            return Result<ProductResponse>.Success(productResponse);
        }

        public async Task<Result<IEnumerable<ProductResponseWithJsonResult>>> GetProductsByCatIdWithJsonResult(Guid catId)
        {
            var products = await productRepository.GetProductsByCatIdWithJsonResult(catId);

            if (products is null)
            {
                return Result<IEnumerable<ProductResponseWithJsonResult>>.Failure("No Products found", StatusCodes.Status404NotFound);
            }
            return Result<IEnumerable<ProductResponseWithJsonResult>>.Success(products);
        }
        #endregion

        #region UPDATE
        public async Task<Result<ProductResponse>> UpdateProduct(Guid id, UpdateProductRequest model)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return Result<ProductResponse>.Failure("No Corresponding Product found", StatusCodes.Status404NotFound);
            }

            var updatedProduct = new Product
            {
                Id = product.Id,
                Brand = model.Brand ?? product.Brand,
                Title = model.Title ?? product.Title,
                Description = model.Description ?? product.Description,
                CategoryId = product.CategoryId,
                Units = product.Units
            };

            await productRepository.UpdateAsync(updatedProduct);
            var returnVal = await unitOfWork.SaveChangesAsync();
            if (returnVal > 0)
            {
                var productResponse = new ProductResponse()
                {
                    Id = product.Id,
                    Title = product.Title!,
                    Description = product.Description!,
                    Brand = product.Brand!,
                    Units = product.Units,
                    CategoryId = product.CategoryId
                };
                return Result<ProductResponse>.Success(productResponse);
            }
            return Result<ProductResponse>.Failure("Something went wrong", StatusCodes.Status500InternalServerError);
        }
        #endregion

        #region DELETE
        public async Task<Result<string>> DeleteProduct(Guid id)
        {
            var returnVal = await productRepository.DeleteProduct(id);
            if (returnVal < 0)
                return Result<string>.Failure("Product Does Not exist", StatusCodes.Status404NotFound);
            else if (returnVal == 0)
                return Result<string>.Failure("You need to delete all assciated Product Details first!", StatusCodes.Status400BadRequest);
            else if (returnVal > 0)
                return Result<string>.Success("Product Deleted Successfully!");

            return Result<string>.Failure("Somthing Went Wrong", StatusCodes.Status500InternalServerError);
        }
        #endregion
    }
}
