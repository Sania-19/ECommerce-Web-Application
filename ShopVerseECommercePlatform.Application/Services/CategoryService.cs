using Microsoft.AspNetCore.Http;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.RRModels.Category;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using ShopVerseECommercePlatform.Application.Abstraction.IStorageService;
using ShopVerseECommercePlatform.Application.Abstraction.IUnitOfWork;
using ShopVerseECommercePlatform.Application.Utils.Result;


namespace ShopVerseECommercePlatform.Application.Services
{
    public class CategoryService(ICategoryRepository categoryRepository, IStorageService storageService, IUnitOfWork unitOfWork,
                                 IProductRepository productRepository) : ICategoryService
    {
        public async Task<Result<CategoryResponse>> CreateCategory(CategoryRequest model)
        {
            if (model.File is null)
                return Result<CategoryResponse>.Failure("Please Select File", StatusCodes.Status404NotFound);

            (string filePath, string fileName) = await storageService.SaveFileAsync(model.File);

            var category = new Category
            {
                Name = model.Name,
                Description = model.Description,
                IsActive = true,
                FilePath = filePath,
                FileName = fileName,
            };
            await categoryRepository.AddAsync(category);
            int returnVal = await unitOfWork.SaveChangesAsync();

            if (returnVal > 0)
            {
                return Result<CategoryResponse>.Success(new CategoryResponse
                {
                    Id = category.Id,
                    Name = category.Name,
                    FilePath = filePath,
                    FileName = category.FileName,
                    Description = category.Description,
                    IsActive = category.IsActive,
                    CreatedOn = category.CreatedOn

                }, "Category Added Successfully!", StatusCodes.Status200OK);
            }
            return Result<CategoryResponse>.Failure("Something went wrong", StatusCodes.Status500InternalServerError);
        }

        public async Task<Result<IEnumerable<CategoryResponse>>> FindCategoryByName(string catName)
        {
            var categories = await categoryRepository.FindByAsync(x => x.Name.StartsWith(catName));

            if (categories is null)
                return Result<IEnumerable<CategoryResponse>>.Failure("No Categories Found", StatusCodes.Status404NotFound);

            var categoryResponse = categories.Select(x => new CategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,
                FileName = x.FileName,
                FilePath = x.FilePath,
            }).ToList();
            return Result<IEnumerable<CategoryResponse>>.Success(categoryResponse);

        }

        public async Task<Result<IEnumerable<CategoryResponse>>> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();
            if (categories is null)
                return Result<IEnumerable<CategoryResponse>>.Failure("No Categories Found", StatusCodes.Status404NotFound);

            var categoryResponse = categories.Select(x => new CategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,
                FileName = x.FileName,
                FilePath = x.FilePath,
                CreatedOn = x.CreatedOn
            });
            return Result<IEnumerable<CategoryResponse>>.Success(categoryResponse);
        }

        public async Task<Result<IEnumerable<CategoryResponse>>> GetActiveCategories()
        {
            var categories = await categoryRepository.FindByAsync(x => x.IsActive == true);

            if (categories is null)
                return Result<IEnumerable<CategoryResponse>>.Failure("No Active Categories Found", StatusCodes.Status404NotFound);

            var categoryResponse = categories.Select(x => new CategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,
                FileName = x.FileName,
                FilePath = x.FilePath,
            }).ToList();
            return Result<IEnumerable<CategoryResponse>>.Success(categoryResponse);
        }

        public async Task<Result<CategoryResponse>> GetCategoryById(Guid id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category is null)
                return Result<CategoryResponse>.Failure("No Category is associated with this Id", StatusCodes.Status404NotFound);

            CategoryResponse categoryResponse = new()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                FilePath = category.FilePath,
                FileName = category.FileName,
                IsActive = category.IsActive,
                CreatedOn = category.CreatedOn
            };
            return Result<CategoryResponse>.Success(value: categoryResponse, "", StatusCodes.Status200OK);

        }

        public async Task<Result<CategoryResponse>> UpdateCategory(UpdateCategoryRequest model, Guid id)
        {
            var categoryResult = await GetCategoryById(id);

            var categoryResponse = categoryResult.Value;

            (string filePath, string fileName) = await storageService.UpdateFileAsync(model.File, categoryResponse.FileName);

            var category = new Category
            {
                Id = id,
                Name = model.Name ?? categoryResponse.Name,
                Description = model.Description ?? categoryResponse.Description,
                IsActive = true,
                FilePath = filePath,
                FileName = fileName,
                CreatedOn = categoryResponse.CreatedOn
            };

            await categoryRepository.UpdateAsync(category);
            int returnVal = await unitOfWork.SaveChangesAsync();

            if (returnVal > 0)
            {
                return Result<CategoryResponse>.Success(new CategoryResponse
                {
                    Id = category.Id,
                    Name = category.Name,
                    FilePath = filePath,
                    FileName = fileName,
                    Description = category.Description,
                    IsActive = category.IsActive

                }, "Category Updated Successfully!", StatusCodes.Status200OK);
            }
            return Result<CategoryResponse>.Failure("Something went wrong", StatusCodes.Status500InternalServerError);
        }

        public async Task<Result<CategoryResponse>> DeleteCategory(Guid id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null)
                return Result<CategoryResponse>.Failure("No Category Found!", StatusCodes.Status404NotFound);
            using var transaction = unitOfWork.BeginTransaction();

            category.IsActive = false;
            await categoryRepository.UpdateAsync(category);
            CategoryResponse categoryResponse = new()
            {
                Id = category.Id,
                Description = category.Description,
                CreatedOn = category.CreatedOn,
                FileName = category.FileName,
                FilePath = category.FilePath,
                IsActive = category.IsActive
            };
            var products = await productRepository.FindByAsync(x => x.CategoryId == id);
            var updateProducts = new List<Product>();
            foreach (var product in products)
            {
                product.IsActive = false;
                product.DeactivatedByCategoryDelete = true;
                updateProducts.Add(product);
            }

            await productRepository.UpdateRangeAsync(updateProducts);
            var returnVal = await unitOfWork.SaveChangesAsync();
            if (returnVal > 0)
            {
                transaction.Commit();
                return Result<CategoryResponse>.Success(categoryResponse);
            }
            else
            {
                transaction.Rollback();
            }
            return Result<CategoryResponse>.Failure("SOmething went wrong, Please try again later!", StatusCodes.Status500InternalServerError);
        }

        public async Task<Result<CategoryResponse>> RestoreCategory(Guid id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null)
                return Result<CategoryResponse>.Failure("No Category Found!", StatusCodes.Status404NotFound);

            using var transaction = unitOfWork.BeginTransaction();

            category.IsActive = true;
            await categoryRepository.UpdateAsync(category);

            CategoryResponse categoryResponse = new()
            {
                Id = category.Id,
                Description = category.Description,
                CreatedOn = category.CreatedOn,
                FileName = category.FileName,
                FilePath = category.FilePath,
                IsActive = category.IsActive
            };

            var products = await productRepository.FindByAsync(x => x.CategoryId == id);
            var updateProducts = new List<Product>();
            foreach (var product in products)
            {
                product.IsActive = true;
                product.DeactivatedByCategoryDelete = false;
                updateProducts.Add(product);
            }
            await productRepository.UpdateRangeAsync(updateProducts);

            var returnVal = await unitOfWork.SaveChangesAsync();
            if (returnVal > 0)
            {
                transaction.Commit();
                return Result<CategoryResponse>.Success(categoryResponse);
            }
            else
            {
                transaction.Rollback();
            }
            return Result<CategoryResponse>.Failure("Something went wrong, Please try again later!", StatusCodes.Status500InternalServerError);
        }
    }
}
