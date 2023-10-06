
namespace GitHubPRSearch.Models
{
    public class PullRequestModel
    {
        public Uri Uri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CommentsAmmount { get; set; }
        public int? StaleFor { get; set; }
        public bool IsDraft { get; set; }
        public DateTime CreationDate { get; set; }
        public UserModel Creator { get; set; }
        public IList<CommitModel> Commits { get; set; }
    }
}
