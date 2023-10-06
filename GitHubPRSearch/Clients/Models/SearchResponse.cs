﻿using Newtonsoft.Json;

namespace GitHubPRSearch.Clients.Models
{

    public class SearchResponse
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("items")]
        public List<PullRequest> PullRequests { get; set; }
    }
}
