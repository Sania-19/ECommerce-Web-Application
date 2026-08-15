using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Persistence.Repository
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(ShopVerseDbContext context) : base(context)
        {

        }
    }
}
