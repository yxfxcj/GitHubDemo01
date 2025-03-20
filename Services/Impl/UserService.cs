using AutoMapper;
using Ivan.DianPing.Constants;
using Ivan.DianPing.Models;
using Ivan.DianPing.Models.DTO;
using Ivan.DianPing.Models.PO;
using Ivan.DianPing.Repository;
using Ivan.DianPing.Utils;
using Ivan.DianPing.V2.Constants;
using Ivan.DianPing.V2.Services.Impl;
using Microsoft.AspNetCore.Mvc;

namespace Ivan.DianPing.Services.Impl
{
    public class UserService : IUserService
    {
        private RedisService _redisService;
        private UserRepository _userRepository;
        private IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper, RedisService redisService) 
        {
            _redisService = redisService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ObjectResult> Login(LoginRequest request)
        {
            // 1.从redis获取手机验证码
            var cacheCodeKey = string.Format(CommonConstants.LOGIN_CODE_PREFIX, request.Phone);
            var cacheCode = await _redisService.GetStringAsync(cacheCodeKey);

            // 1.1 如果从redis的获取的验证码为空 或者和用户提交的验证码不符合则返回错误信息
            if (string.IsNullOrEmpty(cacheCode) || cacheCode != request.Code)
            {
                return new BadRequestObjectResult(ErrorMessageConstants.ERROR_MSG_VERIFY_CODE);
            }

            // 2.通过手机号查询用户是否存在
            var user = await _userRepository.GetByPhoneAsync(request.Phone);

            // 2.1 不存在 创建用户
            if (user == null)
            {
                var userId = await _userRepository.CreateUserAsync(request.Phone);
                user = await _userRepository.GetByIdAsync(userId);
            }

            // 3.将用户信息存入redis
            var token = Guid.NewGuid().ToString("N");
            var tokenKey = string.Format(CommonConstants.LOGIN_TOKEN_PREFIX, token);
            await _redisService.HashSetObjectAsync(tokenKey, user, TimeSpan.FromMinutes(CommonConstants.LOGIN_TOKEN_TTL));

            return new OkObjectResult(token);
        }

        public async Task SendVerifyCode(SendVerifyCodeRequest request)
        {
            // 1.生成验证码
            var code = CommonUtil.GenerateRandomCode();

            // 2.保存验证码到redis
            await _redisService.SetStringAsync(string.Format(CommonConstants.LOGIN_CODE_PREFIX, request.Phone), code, TimeSpan.FromMinutes(CommonConstants.LOGIN_CODE_TTL));
            Console.WriteLine($"code:{code}");

            // 3.发送验证码 （模拟发送验证码操作）
            await Task.Delay(200);
        }

        public async Task<UserDto> GetUserByToken(string token)
        {
            var user = await _redisService.HashGetObjectAsync<User>(token);

            if (user is null) return default;

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
