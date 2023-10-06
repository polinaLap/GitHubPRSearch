using Newtonsoft.Json;

namespace GitHubPRSearch.Clients.Models
{
    public class Commit
    {
        [JsonProperty("sha")]
        public string Hash { get; set; }

        [JsonProperty("commit")]
        public CommitDetails Message { get; set; }

        [JsonProperty("author")]
        public User Author { get; set; }
    }

    public class CommitDetails
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("committer")]
        public Committer Committer { get; set; }
    }

    public class Committer
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
