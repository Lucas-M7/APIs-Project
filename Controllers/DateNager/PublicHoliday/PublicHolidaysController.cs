using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DateNager.HolidayPublic.Controllers;

[ApiController]
[Route("api/")]
public class PublicHolidaysController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public PublicHolidaysController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://date.nager.at");
    }

    [HttpGet("NextPublicHolidays/{countryCode}")]
    public async Task<IActionResult> GetDate(string countryCode)
    {
        try
        {
            string apiUrl = $"/api/v3/NextPublicHolidays/{countryCode}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var holidays = JsonConvert.DeserializeObject<object>(content);
                string formattedJson = JsonConvert.SerializeObject(holidays, Formatting.Indented);
                return Ok(formattedJson);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
        catch (HttpRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("AllPublicHolidays/{year}/{countryCode}")]
    public async Task<IActionResult> GetDateYear(string year, string countryCode)
    {
        try
        {
            string apiUrl = $"/api/v3/PublicHolidays/{year}/{countryCode}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var holidays = JsonConvert.DeserializeObject<object>(content);
                string formattedJson = JsonConvert.SerializeObject(holidays, Formatting.Indented);
                return Ok(formattedJson);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
        catch (HttpRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
