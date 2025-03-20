using StackExchange.Redis;
using System.ComponentModel;
using System.Reflection;

namespace Ivan.DianPing.V2.Extensions
{
    public static class RedisExtensions
    {
        // 对象 → 字典（适配 Redis Hash）
        public static HashEntry[] ToHashEntries<T>(T obj)
        {
            ArgumentNullException.ThrowIfNull(obj);

            var entries = new List<HashEntry>();
            var properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);
                entries.Add(new HashEntry(prop.Name, ConvertToString(value)));
            }

            return entries.ToArray();
        }

        // 字典 → 对象
        public static T FromHashEntries<T>(HashEntry[] entries) where T : new()
        {
            var obj = new T();
            var propDict = typeof(T)
                .GetProperties()
                .Where(p => p.CanWrite)
                .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

            foreach (var entry in entries)
            {
                if (propDict.TryGetValue(entry.Name, out var prop))
                    prop.SetValue(obj, ConvertFromString(entry.Value, prop.PropertyType));
            }

            return obj;
        }

        // 类型安全的值转换
        private static string ConvertToString(object value)
        {
            if (value == null) return null;
            if (value is DateTime dt) return dt.ToString("O"); // ISO 8601
            return TypeDescriptor.GetConverter(value.GetType()).ConvertToString(value);
        }

        private static object ConvertFromString(string value, Type targetType)
        {
            if (string.IsNullOrEmpty(value)) return GetDefaultValue(targetType);
            return TypeDescriptor.GetConverter(targetType).ConvertFromString(value);
        }

        private static object GetDefaultValue(Type type)
            => type.IsValueType ? Activator.CreateInstance(type) : null;
    }
}
