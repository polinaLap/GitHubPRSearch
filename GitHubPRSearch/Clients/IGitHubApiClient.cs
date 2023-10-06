using GitHubPRSearch.Clients.Models;

namespace GitHubPRSearch.Clients
{
    public interface IGitHubApiClient
    {
        Task<SearchResult> SearchPullRequests(SearchRequest request, Pagination pagination);
    }
}