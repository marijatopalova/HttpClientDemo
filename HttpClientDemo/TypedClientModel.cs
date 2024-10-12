using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HttpClientDemo
{
    public class TypedClientModel : PageModel
    {
        private readonly GitHubService _gitHubService;

        public TypedClientModel(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public IEnumerable<GitHubBranch> GitHubBranches { get; set; }

        public async Task OnGet()
        {
            try
            {
                GitHubBranches = await _gitHubService.GetAspNetCoreDocsBranchesAsync();
            }
            catch (HttpRequestException)
            {

                throw;
            }
        }
    }
}
