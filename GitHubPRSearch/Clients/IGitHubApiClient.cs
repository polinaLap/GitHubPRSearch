using GitHubPRSearch.Clients.Models;

namespace GitHubPRSearch.Clients
{
    public interface IGitHubApiClient
    {
        Task<SearchResponse> SearchPullRequests(SearchRequest request, Pagination pagination);
    }
}