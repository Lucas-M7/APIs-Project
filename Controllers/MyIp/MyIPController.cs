using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyIp.Controller;


[ApiController]
[Route("api/")]
public class MyIPController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public MyIPController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://www.myip.com/");
    }

    [HttpGet("GetMyIP")]
    public async Task<IActionResult> GetMyIP()
    {
        try
        {
            string apiUrl = "https://api.myip.com";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var myIp = JsonConvert.DeserializeObject<object>(content);
                string formattedJson = JsonConvert.SerializeObject(myIp, Formatting.Indented);

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