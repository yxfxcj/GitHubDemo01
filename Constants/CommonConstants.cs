namespace Ivan.DianPing.V2.Constants
{
    public static class CommonConstants
    {
        public const string USER_NICKNAME_PREFIX = "user_{0}";

        public const string LOGIN_CODE_PREFIX = "login:code:{0}";

        public const string LOGIN_TOKEN_PREFIX = "login:token:{0}";

        public const int LOGIN_CODE_TTL = 5;

        public const int LOGIN_TOKEN_TTL = 30;

        public const string CACHE_SHOP_TYPES_KEY = "cache:shopTypes";

        public const int CACHE_SHOP_TYPES_TTL = 1440;

        public const string CACHE_SHOP_KEY_PREFIX = "cache:shop:{0}";

        public const int CACHE_SHOP_KEY_TTL = 720;

        public const int CACHE_SHOP_KEY_EMPTY_TTL = 2;

        public const string CACHE_SHOP_NULL_DEFAULT = "null";
    }
}
