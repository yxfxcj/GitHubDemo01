using Ivan.DianPing.Models.PO;
using Ivan.DianPing.V2.Constants;
using Ivan.DianPing.V2.Services.Impl;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ivan.DianPing.V2.Filters
{
    /// <summary>
    /// 刷新用户登录状态TTL
    /// </summary>
    public class RefreshTokenFilter : IActionFilter
    {
        private RedisService _redisService;

        public RefreshTokenFilter(RedisService redisService)
        {
            _redisService = redisService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("authorization", out var token))
            {
                var tokenKey = string.Format(CommonConstants.LOGIN_TOKEN_PREFIX, token);
                var user = await _redisService.HashGetObjectAsync<User>(tokenKey);

                if (user is null) return;

                // 刷新用户token有效期
                await _redisService.SetKeyExAsync(tokenKey, TimeSpan.FromMinutes(CommonConstants.LOGIN_TOKEN_TTL));
            }
        }
    }
}
