namespace BingeBuddy.API.Utility;

public static class Auth
{
    private static string? _apiKey;

    public static string ApiKey
    {
        get
        {
            if (_apiKey != null)
            {
                return _apiKey;
            }
            
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(rootPath, "apikey.txt");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("API key file not found", filePath);

            _apiKey = File.ReadAllText(filePath).Trim();

            Console.WriteLine(_apiKey);
            return _apiKey;
        }
    }
}