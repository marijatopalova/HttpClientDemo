using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace HttpClientDemo
{
    public class NamedClientModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NamedClientModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IEnumerable<GitHubBranch> GitHubBranches { get; set; }

        public async Task OnGet()
        {
            var httpClient = _httpClientFactory.CreateClient("GitHub");
            var httpResponseMessage = await httpClient.GetAsync("repos/dotnet/AspNetCore.Docs/branches");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                GitHubBranches = await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(contentStream);
            }
        }
    }
}
