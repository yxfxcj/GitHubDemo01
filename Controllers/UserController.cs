using Ivan.DianPing.Constants;
using Ivan.DianPing.Filters;
using Ivan.DianPing.Models;
using Ivan.DianPing.Services;
using Ivan.DianPing.Utils;
using Ivan.DianPing.V2.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Ivan.DianPing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("code")]
        public async Task<IActionResult> SendVerifyCode([FromBody] SendVerifyCodeRequest request)
        {
            // 1.验证手机号 不符合格式 返回错误信息
            if (!RegexUtils.IsValidChinesePhoneNumber(request.Phone))
            {
                return BadRequest(ResponseUtil.CreateErrorResponse(ErrorMessageConstants.ERROR_MSG_PHONE_INVALID));
            }

            await _userService.SendVerifyCode(request);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // 1.验证手机号 不符合格式 返回BadRequest携带错误信息
            if (!RegexUtils.IsValidChinesePhoneNumber(request.Phone))
            {
                return BadRequest(ErrorMessageConstants.ERROR_MSG_PHONE_INVALID);
            }

            // 2.验证手机验证码格式
            if (!RegexUtils.IsValidVerifyCode(request.Code))
            {
                return BadRequest(ErrorMessageConstants.ERROR_MSG_VERIFY_CODE);
            }

            var result = await _userService.Login(request);


            return result;
        }

        [HttpGet("me")]
        [TypeFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetUser()
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();

            var tokenKey = string.Format(CommonConstants.LOGIN_TOKEN_PREFIX, token);
            var user = await _userService.GetUserByToken(tokenKey);

            return Ok(user);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUserInfo(string id)
        //{
            
        //}
    }
}
