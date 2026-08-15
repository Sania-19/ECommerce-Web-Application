using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IContextService
{
    public interface IContextService
    {
        Guid GetId();
        string GetEmail();
        string GetPhoneNo();
        string GetUserRole();
        string GetCurrentURI();
        string GetClientURI();
    }
}
