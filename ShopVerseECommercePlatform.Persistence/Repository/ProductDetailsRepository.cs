using Microsoft.EntityFrameworkCore;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.RRModels.ProductDetails;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Persistence.Data;
using ShopVerseECommercePlatform.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Persistence.Repository
{
    public class ProductDetailsRepository(ShopVerseDbContext context) : BaseRepository<ProductDetails>(context), IProductDetailsRepository
    {
        public async Task<ProductFullDetailsWithFilesResponse> GetProductDetailsByPdId(Guid pdid)
        {
            var products = await context.Database.SqlQuery<ProductFullDetailsWithFilesResponse>($@"SELECT P.Id, P.Title, P.Brand, P.[Description], P.Units, P.CategoryId, P.CreatedOn,PD.Id AS ProductDetailId, Price, Discount,FilePath,[FileName],PD.Quantity,
																		  (
																		   SELECT F.FilePath,F.[FileName]  FROM ProductDetails PD1
																		   RIGHT JOIN AppFiles F
																		   ON PD1.Id=F.EntityId
		                                                                   FOR JSON PATH 
																		  ) AS AppFilesJson
																		   FROM Products P
																		   INNER JOIN ProductDetails PD
																		   ON P.Id=PD.ProductId
                                                                           WHERE PD.Id =  {pdid}").FirstOrDefaultAsync();
			return products;
        }
    }
}
