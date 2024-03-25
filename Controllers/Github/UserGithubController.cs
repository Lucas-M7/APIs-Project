using GithubAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GithubAPI.Controllers;

[ApiController]
[Route("api/")]
public class UserGithubController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UserGithubController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("user/{username}")]
    public async Task<IActionResult> GetUser(string username)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "GithubAPI");

            var response = await client.GetFromJsonAsync<UserGitHub>($"users/{username}");
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
        catch
        {
            throw new ArgumentException("Falha ao fazer o consumo da API");
        }
    }
}
