using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopVerseECommercePlatform.Api.CustomExtensions;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using ShopVerseECommercePlatform.Application.RRModels.Category;

namespace ShopVerseECommercePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
       
        [HttpPost]
        public async Task<IResult> CreateCategory([FromForm] CategoryRequest model)
        {
            return this.ApiResponse(await categoryService.CreateCategory(model));
        }

        [AllowAnonymous]
        [HttpGet("categoryName/{name}")]
        public async Task<IResult> FindCategoryByName(string name)
        {
            return this.ApiResponse(await categoryService.FindCategoryByName(name));
        }

        [HttpGet]
        public async Task<IResult> GetAllCategories()
        {
            return this.ApiResponse(await categoryService.GetAllCategories());
        }

        [HttpGet("{id:guid}")]
        public async Task<IResult> GetCategoryById(Guid id)
        {
            return this.ApiResponse(await categoryService.GetCategoryById(id));
        }

        [AllowAnonymous]
        [HttpGet("active")]
        public async Task<IResult> GetActiveCategories()
        {
            return this.ApiResponse(await categoryService.GetActiveCategories());
        }

        [HttpPut("{id:guid}")]
        public async Task<IResult> UpdateCategory([FromForm] UpdateCategoryRequest model, Guid id)
        {
            return this.ApiResponse(await categoryService.UpdateCategory(model, id));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IResult> DeleteCategory(Guid id) => this.ApiResponse(await categoryService.DeleteCategory(id));

        [HttpPatch("restore/{id:guid}")]
        public async Task<IResult> RestoreCategory(Guid id) => this.ApiResponse(await categoryService.RestoreCategory(id));

    }
}
