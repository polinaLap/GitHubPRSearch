
namespace GitHubPRSearch.Models
{
    public class SearchResultModel
    {
        public IDictionary<string, PullRequestGroupModel> Groups { get; set; }
        public int AvarageDaysAmount { get; set; }
        public int TotalPRCount { get; set; }
    }
}
