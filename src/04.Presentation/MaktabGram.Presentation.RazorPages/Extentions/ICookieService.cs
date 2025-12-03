namespace MaktabGram.Presentation.RazorPages.Extentions
{
    public interface ICookieService
    {
        void Set(string key, string value, CookieOptions? options = null);
        void SetObject<T>(string key, T value, CookieOptions? options = null);

        string? Get(string key);
        T? GetObject<T>(string key);

        void Delete(string key);
        bool Exists(string key);
        public int GetUserId();
        public bool UserIsLoggedIn();
    }
}
