using System.Text.Json;
using System.Text.Json.Serialization;

namespace Torget__Blocket_klon_.Data.Services;

public class ZipCodeHandler
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string ZIPCODE_API_KEY = "1a25eefcded7ac844a4bf65134cf8670d99515fd";

    public ZipCodeHandler(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;

    public async Task<ZipCodeApiResult> GetZipCodeInformation(string zipCode)
    {
        var zipCodeInformationString = await GetZipCodeInformationFromApi(zipCode);
        var apiResults = JsonSerializer.Deserialize<ApiResults>(zipCodeInformationString);

        var zipCodeInformation = apiResults.Results[0];

        return zipCodeInformation;
    }

    private async Task<string> GetZipCodeInformationFromApi(string zipCode)
    {
        var client = _httpClientFactory.CreateClient();

        var zipCodeResponseMessage = await client.GetAsync(
            "https://api.papapi.se/lite/" +
            $"?query={zipCode}" +
            "&format=json" +
            $"&apikey={ZIPCODE_API_KEY}");

        if (!zipCodeResponseMessage.IsSuccessStatusCode)
            throw new ZipCodeNotValidException();

        return await zipCodeResponseMessage.Content.ReadAsStringAsync();
    }
}

public class ZipCodeNotValidException : Exception
{
    public ZipCodeNotValidException() : base("Postnumret finns ej eller är felskrivet")
    {
    }
}

public class ZipCodeApiResult
{
    [JsonPropertyName("postal_code")] public string PostalCode { get; set; }

    [JsonPropertyName("city")] public string City { get; set; }

    [JsonPropertyName("latitude")] public string Latitude { get; set; }

    [JsonPropertyName("longitude")] public string Longitude { get; set; }

    [JsonPropertyName("county_code")] public string CountyCode { get; set; }

    [JsonPropertyName("county")] public string County { get; set; }

    [JsonPropertyName("state_code")] public string StateCode { get; set; }

    [JsonPropertyName("state")] public string State { get; set; }

    [JsonPropertyName("maps")] public List<string> Maps { get; set; }

    [JsonPropertyName("note")] public string Note { get; set; }

    [JsonPropertyName("created")] public string Created { get; set; }

    [JsonPropertyName("updated")] public string Updated { get; set; }
}

public class ApiResults
{
    [JsonPropertyName("results")] public List<ZipCodeApiResult> Results { get; set; }
}