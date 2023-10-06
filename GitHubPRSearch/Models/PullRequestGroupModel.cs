
namespace GitHubPRSearch.Models
{
    public class PullRequestGroupModel
    {
        public IList<PullRequestModel> PullRequests { get; set; } = new List<PullRequestModel>();
        public int AvarageDaysAmount { get; set; }

    }
}
