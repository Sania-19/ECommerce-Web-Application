using Microsoft.AspNetCore.Http;
using ShopVerseECommercePlatform.Application.Abstraction.IContextService;
using ShopVerseECommercePlatform.Infrastructure.JWTProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Infrastructure.ContextService
{
    public class ContextService(IHttpContextAccessor context) : IContextService
    {
        public string GetClientURI()
        {
            var referer = context.HttpContext.Request.Headers["Referer"];
            return referer;
            //context.HttpContext.Request.Headers.TryGetValue("Referer", out var referer);
            //return referer;
        }

        public Guid GetId()
        {
            var userId = context.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == UserClaims.Id).Value;
            if (Guid.TryParse(userId, out Guid uid))
            {
                return uid;
            }
            return Guid.Empty;
        }
        public string GetCurrentURI()
        {
            return context.HttpContext.Request.Path.ToString();
        }

        public string GetEmail()
        {
            return context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == UserClaims.Email)!.Value;
        }

        public string GetPhoneNo()
        {
            return context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == UserClaims.PhoneNo)!.Value;
        }


        public string GetUserRole()
        {
            return context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == UserClaims.UserRole)!.Value;
        }
    }
}
