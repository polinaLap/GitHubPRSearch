using GitHubPRSearch.Clients.Models;
using GitHubPRSearch.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GitHubPRSearch.Clients
{
    public class GitHubApiClient : IGitHubApiClient
    {
        private readonly HttpClient _httpClient;

        public GitHubApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<SearchResult> SearchPullRequests(SearchRequest request)
        {
            try
            {
                var searchResponse = await SearchPullRequestsImpl(request);
                await GetCommits(request, searchResponse);

                return searchResponse;
            }
            catch (Exception ex)
            {
                throw new ApiException("An error occurred while querying GitHub API.", ex);
            }
        }

        private async Task GetCommits(SearchRequest request, SearchResult searchResponse)
        {
            var prUri = $"repos/{request.RepositoryOwner}/{request.RepositoryName}/pulls";

            foreach (var pullRequest in searchResponse.PullRequests)
            {
                var commitsUri = $"{prUri}/{pullRequest.Number}/commits";
                var commitsResponse = await _httpClient.GetAsync(commitsUri);

                if (!commitsResponse.IsSuccessStatusCode)
                {
                    throw new ApiException($"GitHub API request failed: {commitsResponse.ReasonPhrase}");
                }
                var commitsJson = await commitsResponse.Content.ReadAsStringAsync();
                pullRequest.Commits = JsonConvert.DeserializeObject<List<Commit>>(commitsJson);
            }
        }

        private async Task<SearchResult> SearchPullRequestsImpl(SearchRequest request)
        {
            string searchUri = $"search/issues?q={request.Keywords}+repo:{request.RepositoryOwner}/{request.RepositoryName}" +
                    $"+is:pr+label:{request.PrLabel}+state:open" +
                    $"&sort=created&order=desc";

            var response = await _httpClient.GetAsync(searchUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException($"GitHub API request failed: {response.ReasonPhrase}");
            }
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SearchResult>(json);
        }
    }

    public class ApiException : Exception
    {
        public ApiException(string message, Exception innerException = null) : base(message, innerException) { }
    }

    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException)
            {
                context.Result = new RedirectResult("~/Home/Error");
                context.ExceptionHandled = true;
            }
        }
    }
}
