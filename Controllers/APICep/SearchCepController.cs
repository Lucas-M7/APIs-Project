using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api_Que_ConsomeOutrasApi.Controllers.APICep;

[ApiController]
[Route("api/[controller]")]
public class SearchCepController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public SearchCepController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://viacep.com.br/");
    }

    [HttpGet("GetYourCep/{UF}/{Cidade}/{Bairro}")]
    public async Task<IActionResult> GetYoutCep(string UF, string Cidade, string Bairro)
    {
        try
        {
            string apiUrl = $"https://viacep.com.br/ws/{UF}/{Cidade}/{Bairro}/json/";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var myCep = JsonConvert.DeserializeObject<object>(content);
                string formattedJson = JsonConvert.SerializeObject(myCep, Formatting.Indented);

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
