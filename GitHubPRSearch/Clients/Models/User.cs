using Newtonsoft.Json;

namespace GitHubPRSearch.Clients.Models
{
    public class User
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
