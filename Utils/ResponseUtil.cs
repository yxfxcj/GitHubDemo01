using Ivan.DianPing.Constants;
using Ivan.DianPing.Models;
using Ivan.DianPing.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ivan.DianPing.Utils
{
    public static class ResponseUtil
    {
        public static BaseResponse CreateErrorResponse(string errMsg, ErrorCodeEnum errorCode = ErrorCodeEnum.BadRequest)
        {
            return new BaseResponse { Message = errMsg, Code = errorCode };
        }

        public static LoginResponse CreateLoginResponse(string errMsg = null, ErrorCodeEnum errorCode = ErrorCodeEnum.BadRequest, UserDto user = null)
        {
            return new LoginResponse { Message = errMsg, Code = errorCode, UserDto = user };
        }
    }
}
