
/// <summary>
/// Defines a factory for creating <see cref="HttpClient"/> instances configured to communicate with the Ahjo API.
/// </summary>
public interface IAhjoApiConnection
{
    /// <summary>
    /// Creates an <see cref="HttpClient"/> with the Ahjo API base address and API key header pre-configured.
    /// </summary>
    /// <returns>A ready-to-use <see cref="HttpClient"/>.</returns>
    HttpClient CreateConnection();
}

/// <summary>
/// Creates <see cref="HttpClient"/> instances for the Ahjo API using configuration values
/// <c>AHJO_API_URL</c> (base address) and <c>AHJO_API_KEY</c> (authentication header).
/// </summary>
public class AhjoApiConnection : IAhjoApiConnection
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initialises a new instance of <see cref="AhjoApiConnection"/>.
    /// </summary>
    /// <param name="configuration">Application configuration providing <c>AHJO_API_URL</c> and <c>AHJO_API_KEY</c>.</param>
    public AhjoApiConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc />
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