using Ivan.DianPing.V2.Constants;
using System.Security.Cryptography;

namespace Ivan.DianPing.Utils
{
    public static class CommonUtil
    {
        /// <summary>
        /// 随机生成指定位数验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length = 6)
        {
            using var rng = RandomNumberGenerator.Create();
            var buffer = new byte[sizeof(int)];
            rng.GetBytes(buffer);

            var num = Math.Abs(BitConverter.ToInt32(buffer, 0)) % (int)Math.Pow(10, length);
            return num.ToString($"D{length}");
        }

        /// <summary>
        /// 随机生成用户昵称
        /// </summary>
        /// <returns></returns>
        public static string GenerateUserNickname()
        {
            const string safeLetters = "abcdefghijklmnopqrstuvwxyz"; 
            const string safeNumbers = "0123456789";
            const string safeAll = safeLetters + safeNumbers;

            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[10];
            rng.GetBytes(bytes);

            var chars = new char[10];
            for (var i = 0; i < 10; i++)
            {
                chars[i] = safeAll[bytes[i] % safeAll.Length];
            }

            var nickname = string.Format(CommonConstants.USER_NICKNAME_PREFIX, new string(chars));

            return nickname;
        }
    }
}
