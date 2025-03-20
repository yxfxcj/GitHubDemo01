using Ivan.DianPing.Models.DTO;

namespace Ivan.DianPing.V2.Utils
{
    public class UserHolder
    {
        private static readonly ThreadLocal<UserDto> t1 = new ThreadLocal<UserDto>();

        public static void SaveUser(UserDto user)
        {

            
        }
    }
}
