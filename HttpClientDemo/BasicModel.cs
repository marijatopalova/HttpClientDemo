using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace HttpClientDemo
{
    public class BasicModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasicModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IEnumerable<GitHubBranch>? GitHubBranches { get; set; }

        public async Task OnGet() 
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/dotnet/AspNetCore.Docs/branches");

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                GitHubBranches = await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(contentStream);
            }
        }
    }
}
