using Ivan.DianPing.Models.PO;
using Ivan.DianPing.V2.Constants;
using Ivan.DianPing.V2.Services.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ivan.DianPing.Filters
{
    public class AuthFilter(RedisService redisService) : IAuthorizationFilter
    {
        private readonly RedisService _redisService = redisService;

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("authorization", out var token)) 
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var tokenKey = string.Format(CommonConstants.LOGIN_TOKEN_PREFIX, token);
            var user = await _redisService.HashGetObjectAsync<User>(tokenKey);

            if (user is null) 
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
