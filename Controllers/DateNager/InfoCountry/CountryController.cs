using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DateNager.InfoCountry.Controller;

[ApiController]
[Route("api/")]
public class CountryController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public CountryController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://date.nager.at");
    }

    [HttpGet("CountryInfo/{countryCode}")]
    public async Task<IActionResult> GetCountry(string countryCode)
    {
        try
        {
            string apiUrl = $"/api/v3/CountryInfo/{countryCode}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var country = JsonConvert.DeserializeObject<object>(content);
                string formattedJson = JsonConvert.SerializeObject(country, Formatting.Indented);
                return Ok(formattedJson);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
        catch
        {
            throw new ArgumentException("Falha ao fazer o consumo da API");
        }
    }

    [HttpGet("AvailableCountries")]
    public async Task<IActionResult> GetCountries()
    {
        try
        {
            string apiUrl = "/api/v3/AvailableCountries";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var country = JsonConvert.DeserializeObject<object>(content);
                string formattedJson = JsonConvert.SerializeObject(country, Formatting.Indented);
                return Ok(formattedJson);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
        catch
        {
            throw new ArgumentException("Falha ao fazer o consumo da API");
        }
    }
}