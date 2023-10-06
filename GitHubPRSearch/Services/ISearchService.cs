using GitHubPRSearch.Models;

namespace GitHubPRSearch.Services
{
    public interface ISearchService
    {
        Task<SearchResultModel> Search(SearchRequest request);
    }
}