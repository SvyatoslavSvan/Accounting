using Accounting.Domain.Models;
using System.Text.Json;

namespace Accounting.Extensions
{
    public static class SessionExtensions
    {
#nullable disable
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionValue = session.GetString(key);
            return sessionValue == null ? default(T) : JsonSerializer.Deserialize<T>(sessionValue);
        }
    }
}
