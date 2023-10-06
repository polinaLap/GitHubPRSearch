
namespace GitHubPRSearch.Models
{
    public class SearchResultModel
    {
        public IList<PullRequestGroupModel> Groups { get; set; }
        public double AvarageDaysAmount { get; set; }
    }


    public class PullRequestGroupModel
    {
        public string GroupName { get; set; }
        public IList<PullRequestModel> PullRequests { get; set; }
        public double AvarageDaysAmount { get; set; }

    }
    public class PullRequestModel
    {
        public Uri Uri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CommentsAmmount { get; set; }
        public DateTime CreationDate { get; set; }
        public UserModel Creator { get; set; }
        public IList<CommitModel> Commits { get; set; }
    }

    public class UserModel
    {
        public string Login { get; set; }
        public Uri AvatarLink { get; set; }
    }

    public class CommitModel
    {
        public string Hash { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public UserModel Author { get; set; }
    }
}
