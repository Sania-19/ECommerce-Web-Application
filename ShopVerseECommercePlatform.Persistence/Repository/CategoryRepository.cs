
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Persistence.Data;
using ShopVerseECommercePlatform.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Persistence.Repository
{
    public class CategoryRepository(ShopVerseDbContext context):BaseRepository<Category>(context), ICategoryRepository
    {
    }
}
