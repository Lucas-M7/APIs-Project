using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api_Que_ConsomeOutrasApi.Controllers.FipeVeiculos;

[ApiController]
[Route("api/")]
public class VeiculosController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public VeiculosController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://parallelum.com.br/fipe/api/v1");
    }

    [HttpGet("carros/marcas")]
    public async Task<IActionResult> GetCars()
    {
        try
        {
            string apiUrl = "https://parallelum.com.br/fipe/api/v1/carros/marcas";

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

    [HttpGet("motos/marcas")]
    public async Task<IActionResult> GetBikers()
    {
        try
        {
            string apiUrl = "https://parallelum.com.br/fipe/api/v1/motos/marcas";

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

    [HttpGet("carros/marcas{id}/modelos")]
    public async Task<IActionResult> GetCarsModel(int id)
    {
        try
        {
            string apiUrl = $"https://parallelum.com.br/fipe/api/v1/carros/marcas/{id}/modelos";

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