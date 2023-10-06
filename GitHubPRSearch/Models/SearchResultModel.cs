
namespace GitHubPRSearch.Models
{
    public class SearchResultModel
    {
        public IDictionary<string, PullRequestGroupModel> Groups { get; set; }
        public int AvarageDaysAmount { get; set; }
    }


    public class PullRequestGroupModel
    {
        public IList<PullRequestModel> PullRequests { get; set; } = new List<PullRequestModel>();
        public int AvarageDaysAmount { get; set; }

    }
    public class PullRequestModel
    {
        public Uri Uri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CommentsAmmount { get; set; }
        public bool IsDraft { get; set; }
        public DateTime CreationDate { get; set; }
        public UserModel Creator { get; set; }
        public IList<CommitModel> Commits { get; set; }
    }

    public class UserModel
    {
        public string Login { get; set; }
        public Uri AvatarUrl { get; set; }
    }

    public class CommitModel
    {
        public string Hash { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public UserModel Author { get; set; }
    }
}
