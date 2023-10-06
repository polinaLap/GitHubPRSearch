using GitHubPRSearch.Clients.Models;
using GitHubPRSearch.Models;

namespace GitHubPRSearch.Clients
{
    public interface IGitHubApiClient
    {
        Task<SearchResult> SearchPullRequests(SearchRequest request);
    }
}