using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace Api_Que_ConsomeOutrasApi.Controllers.APICep;

[ApiController]
[Route("api/[controller]")]
public class SearchCepController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public SearchCepController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://cdn.apicep.com/file/apicep/{cep}.json");
    }
}
