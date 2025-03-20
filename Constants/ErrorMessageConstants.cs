namespace Ivan.DianPing.Constants
{
    public class ErrorMessageConstants
    {
        public const string ERROR_MSG_PHONE_INVALID = "手机号格式错误";

        public const string ERROR_MSG_VERIFY_CODE = "验证码错误";
    }

    public enum ErrorCodeEnum
    {
        Ok = 200,
        BadRequest = 400
    }
}
