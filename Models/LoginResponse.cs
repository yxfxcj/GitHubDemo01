using Ivan.DianPing.Models.DTO;

namespace Ivan.DianPing.Models
{
    public class LoginResponse : BaseResponse
    {
        public UserDto UserDto { get; set; }
    }
}
