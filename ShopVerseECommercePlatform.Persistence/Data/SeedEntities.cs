using Microsoft.EntityFrameworkCore;
using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Persistence.Data
{
	public static class SeedEntities
	{
		public static void SeedData(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(

				new User
				{
					Id = Guid.CreateVersion7(),
					Email = "sania@gmail.com",
					PhoneNo = "9797893466",
					UserRole = UserRole.SuperAdmin,
					UserStatus = UserStatus.Active,
					Password = "$2a$11$YOtZkxWhHmwRR4XiiwA1PO8WGZyTnzJXue6ZFesAsJiB8a3bzbXTi",
					Salt = "$2a$11$YOtZkxWhHmwRR4XiiwA1PO",
					ConfirmationCode = "",
				}
			);
		}
	}
}
