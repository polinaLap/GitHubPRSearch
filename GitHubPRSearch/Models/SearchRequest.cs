namespace GitHubPRSearch.Models
{
    public record SearchRequest(string RepositoryOwner, string RepositoryName, string PrLabel, string Keywords);
}
