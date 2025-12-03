     namespace MaktabGram.Presentation.RazorPages.Extentions
{
    using Azure.Core;
    using Microsoft.AspNetCore.Http;
    using System.Text.Json;

    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _accessor;

        public CookieService(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }


        public bool UserIsLoggedIn()
        {
            return Context.Request.Cookies.Any(x => x.Key == "Id");
        }

        public int GetUserId()
        {
            if (Context.Request.Cookies.TryGetValue("Id", out var userIdStr) &&
                int.TryParse(userIdStr, out var userIdFromCookie))
            {
                return userIdFromCookie;
            }

            throw new Exception("User is not logged in.");
        }

        private HttpContext Context => _accessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is not available. This service can only be used within an HTTP request.");

        // ذخیره مقدار ساده
        public void Set(string key, string value, CookieOptions? options = null)
        {
            options ??= new CookieOptions
            {
                HttpOnly = true,
                Secure = Context.Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            };

            Context.Response.Cookies.Append(key, value, options);
        }

        // ذخیره آبجکت (JSON)
        public void SetObject<T>(string key, T value, CookieOptions? options = null)
        {
            var json = JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = false });
            Set(key, json, options);
        }

        // خواندن مقدار
        public string? Get(string key)
        {
            return Context.Request.Cookies.TryGetValue(key, out var value) ? value : null;
        }

        // خواندن آبجکت
        public T? GetObject<T>(string key)
        {
            var json = Get(key);
            if (string.IsNullOrEmpty(json)) return default;
            try
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                return default;
            }
        }

        // حذف کوکی
        public void Delete(string key)
        {
            Context.Response.Cookies.Delete(key);
        }

        // وجود کوکی
        public bool Exists(string key)
        {
            return Context.Request.Cookies.ContainsKey(key);
        }
    }
}
