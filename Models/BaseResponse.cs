using Ivan.DianPing.Constants;

namespace Ivan.DianPing.Models
{
    public class BaseResponse
    {
        public ErrorCodeEnum Code { get; set; }

        public string Message { get; set; }
    }
}
