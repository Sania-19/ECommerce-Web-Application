using ShopVerseECommercePlatform.Application.RRModels.Address;
using ShopVerseECommercePlatform.Application.Utils.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IServices
{
    public interface IAddressService
    {
        Task<Result<AddressResponse>> AddAddress(AddressRequest model);
        Task<Result<AddressResponse>> UpdateAddress(UpdateAddressRequest model);
        Task<Result<AddressResponse>> GetAddressById(Guid id);
        Task<Result<IEnumerable< AddressResponse>>> GetAddressOfLoggedInUser();
        Task<Result<IEnumerable<AddressResponse>>> GetAddressByUserId();
        Task<Result<AddressResponse>> DeleteById(Guid id);
        Task<Result<int>> DeleteAddresses(IEnumerable<Guid> ids);
        Task<Result<int>> DeleteAllAddresses();
    }
}
