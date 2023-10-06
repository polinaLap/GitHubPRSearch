using AutoMapper;
using GitHubPRSearch.Clients;
using GitHubPRSearch.Models;

namespace GitHubPRSearch.Services
{
    public class SearchService : ISearchService
    {
        private const string ActiveGroup = "Active";
        private const string DraftGroup = "Draft";
        private const string StaleGroup = "Stale";

        private readonly IGitHubApiClient _gitHubApiClient;
        private readonly IMapper _mapper;

        public SearchService(IGitHubApiClient gitHubApiClient, IMapper mapper)
        {
            _gitHubApiClient = gitHubApiClient;
            _mapper = mapper;
        }

        public async Task<SearchResultModel> Search(SearchRequest request)
        {
            var searchResult = await _gitHubApiClient.SearchPullRequests(request);
            var pullRequestModels = searchResult.PullRequests.Select(pullRequest => _mapper.Map<PullRequestModel>(pullRequest)).ToList();

            var result = GroupPullRequests(pullRequestModels);

            return result;
        }

        private SearchResultModel GroupPullRequests(List<PullRequestModel> pullRequests)
        {
            var result = new SearchResultModel
            {
                TotalPRCount = pullRequests.Count,
                Groups = new Dictionary<string, PullRequestGroupModel>
            {
                { ActiveGroup, new PullRequestGroupModel() },
                { DraftGroup, new PullRequestGroupModel() },
                { StaleGroup, new PullRequestGroupModel() }
            }
            };
            var now = DateTime.Now;
            var totalDays = 0;
            var activeDays = 0;
            var draftDays = 0;
            var staleDays = 0;

            foreach (var pullRequest in pullRequests)
            {
                var daysOld = (now - pullRequest.CreationDate).Days;
                totalDays += daysOld;

                if (pullRequest.IsDraft)
                {
                    draftDays += daysOld;
                    result.Groups[DraftGroup].PullRequests.Add(pullRequest);
                }
                else if (daysOld > 30)
                {
                    staleDays += daysOld;
                    pullRequest.StaleFor = daysOld - 30;
                    result.Groups[StaleGroup].PullRequests.Add(pullRequest);
                }
                else
                {
                    activeDays += daysOld;
                    result.Groups[ActiveGroup].PullRequests.Add(pullRequest);
                }
            }

            if (pullRequests.Any())
            {
                result.AvarageDaysAmount = totalDays / pullRequests.Count;
            }

            UpdateAverageDaysForGroup(result.Groups[ActiveGroup], activeDays);
            UpdateAverageDaysForGroup(result.Groups[DraftGroup], draftDays);
            UpdateAverageDaysForGroup(result.Groups[StaleGroup], staleDays);

            return result;
        }

        private static void UpdateAverageDaysForGroup(PullRequestGroupModel group, int days)
        {
            if (group.PullRequests.Count > 0)
            {
                group.AvarageDaysAmount = days / group.PullRequests.Count;
            }
        }
    }
}
