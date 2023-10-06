using Newtonsoft.Json;

namespace GitHubPRSearch.Clients.Models
{

    public class SearchResult
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("items")]
        public List<PullRequest> PullRequests { get; set; }
    }
}
