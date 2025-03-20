using System.Text.RegularExpressions;

namespace Ivan.DianPing.Utils
{
    public static class RegexUtils
    {
        // 中国大陆手机号正则表达式（2023年最新号段）
        private static readonly Regex ChinesePhoneRegex = new Regex(
            @"^(?:\+?86)?1[3-9]\d{9}$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant
        );

        /// <summary>
        /// 验证中国大陆手机号格式
        /// </summary>
        /// <param name="phoneNumber">待验证的手机号</param>
        /// <returns>是否有效</returns>
        public static bool IsValidChinesePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return false;

            // 移除所有空格和特殊字符
            var cleanNumber = Regex.Replace(phoneNumber, @"[\s\-()]", "");

            return ChinesePhoneRegex.IsMatch(cleanNumber);
        }

        /// <summary>
        /// 验证是否为6位纯数字验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsValidVerifyCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return false;

            return Regex.IsMatch(code, @"^\d{6}$");
        }
    }
}
