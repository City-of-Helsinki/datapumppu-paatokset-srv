
public interface IAhjoApiConnection
{
    HttpClient CreateConnection();
}

public class AhjoApiConnection : IAhjoApiConnection
{
    private readonly IConfiguration _configuration;

    public AhjoApiConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public HttpClient CreateConnection()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(_configuration["AHJO_API_URL"])
        };

        var apiKey = _configuration["AHJO_API_KEY"];
        httpClient.DefaultRequestHeaders.Add("api-key", apiKey);

        return httpClient;
    }
}