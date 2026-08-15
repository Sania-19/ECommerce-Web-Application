
using Microsoft.EntityFrameworkCore;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.RRModels.Product;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Persistence.Data;
using ShopVerseECommercePlatform.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Persistence.Repository
{
    public class ProductRepository(ShopVerseDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        public async Task<IEnumerable<ProductResponse>> GetProductsByCatId(Guid categoryId)
        {
            var products = await context.Database.SqlQuery<ProductResponse>($@"SELECT P.Id,Title,Brand,[Description],Units,CategoryId,Price,Discount,Pd.[FileName],FilePath,Pd.Id AS ProductDetailsId FROM Products P
                                                          INNER JOIN ProductDetails Pd
                                                          ON P.Id= Pd.ProductId
                                                          WHERE P.CategoryId ={categoryId}").ToListAsync();
            return products;
        }

        public async Task<int> InsertProductWithDetails(ProductWithDetails model)
        {
            string query = $@"INSERT INTO Products (Id,Title,Brand,[Description],Units,CategoryId,CreatedOn)
                                                      VALUES ('{model.ProductId}',
                                                             '{model.ProductRequest.Title}',
                                                             '{model.ProductRequest.Brand}',
                                                             '{model.ProductRequest.Description}',
                                                              {(int)model.ProductRequest.Units},
                                                             '{model.ProductRequest.CategoryId}',
                                                             '{model.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss zzz")}');";
            query += $@"INSERT INTO ProductDetails (Id,Price,Discount,FilePath,[FileName],ProductId,CreatedOn)
                        VALUES('{model.ProductDetailId}',
                                {model.ProductRequest.Price},
                                {model.ProductRequest.Discount},
                               '{model.FilePath}',
                               '{model.FileName}',
                               '{model.ProductId}',
                               '{model.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss zzz")}')";
            return await context.Database.ExecuteSqlRawAsync($@"{query}");
        }

        public async Task<IEnumerable<ProductResponseWithJsonResult>> GetProductsByCatIdWithJsonResult(Guid categoryId)
        {
           
            var products = await context.Database.SqlQuery<ProductResponseWithJsonResult>($@"SELECT P.Id, P.Title, P.Brand, P.[Description], P.Units, P.CategoryId, P.CreatedOn,
	                                                                       (
		                                                                        SELECT PD.Id AS ProductDetailId, Price, Discount, FilePath, [FileName]  FROM ProductDetails PD
		                                                                        WHERE PD.ProductId = P.Id
		                                                                        FOR JSON PATH --, WITHOUT_ARRAY_WRAPPER
	                                                                        ) AS ProductDetailsJson

                                                                            FROM Products P
                                                                            WHERE P.CategoryId =  {categoryId}").ToListAsync();

            return products;
        }
        public async Task<int> DeleteProduct(Guid id)
        {
            var returnVal = await context.Database.SqlQuery<int>($"EXEC spDeleteProduct @id={id}")
                           .ToListAsync();
            return returnVal.Single();
        }


    }
}
