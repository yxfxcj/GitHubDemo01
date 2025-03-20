using Ivan.DianPing.Models;
using Ivan.DianPing.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Ivan.DianPing.Services
{
    public interface IUserService
    {
        Task SendVerifyCode(SendVerifyCodeRequest request);

        Task<ObjectResult> Login(LoginRequest request);

        Task<UserDto> GetUserByToken(string token);
    }
}
