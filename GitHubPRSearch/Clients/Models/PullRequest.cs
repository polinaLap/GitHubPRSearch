using Newtonsoft.Json;

namespace GitHubPRSearch.Clients.Models
{
    public class PullRequest
    {
        [JsonProperty("html_url")]
        public string Uri { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Description { get; set; }

        [JsonProperty("draft")]
        public bool IsDraft { get; set; }

        [JsonProperty("comments")]
        public int CommentsCount { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("user")]
        public User Creator { get; set; }

        [JsonIgnore]
        public IList<Commit> Commits { get; set; }
    }
}
