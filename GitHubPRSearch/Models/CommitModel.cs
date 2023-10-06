
namespace GitHubPRSearch.Models
{
    public class CommitModel
    {
        public string Hash { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public UserModel Author { get; set; }
    }
}
