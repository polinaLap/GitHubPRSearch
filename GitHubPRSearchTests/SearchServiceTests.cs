using AutoMapper;
using GitHubPRSearch.Clients;
using GitHubPRSearch.Clients.Models;
using GitHubPRSearch.Models;
using GitHubPRSearch.Services;
using Moq;

namespace GitHubPRSearchTests
{

    [TestFixture]
    public class SearchServiceTests
    {
        private SearchService _searchService;
        private Mock<IGitHubApiClient> _mockGitHubApiClient;
        private IMapper _mapper;

        private readonly SearchRequest DefaultSearchRequest = new("owner", "reponame", null, null);

        private const string ActiveGroup = "Active";
        private const string DraftGroup = "Draft";
        private const string StaleGroup = "Stale";
        private static DateTime Now = DateTime.Now;

        [SetUp]
        public void SetUp()
        {
            _mockGitHubApiClient = new Mock<IGitHubApiClient>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _searchService = new SearchService(_mockGitHubApiClient.Object, _mapper);
        }

        [Test]
        [TestCaseSource(nameof(GenerateTestData))]
        public async Task Search_Success(SearchResult apiResponse, SearchResultModel expectedResult)
        {
            // Arrange
            _mockGitHubApiClient.Setup(x => x.SearchPullRequests(DefaultSearchRequest))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _searchService.Search(DefaultSearchRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(expectedResult.TotalPRCount, Is.EqualTo(result.TotalPRCount));
            Assert.That(expectedResult.AvarageDaysAmount, Is.EqualTo(result.AvarageDaysAmount));
            Assert.Multiple(() =>
            {
                Assert.That(result.Groups.ContainsKey(ActiveGroup));
                Assert.That(result.Groups[ActiveGroup].PullRequests.Count, Is.EqualTo(expectedResult.Groups[ActiveGroup].PullRequests.Count));
                Assert.That(result.Groups[ActiveGroup].AvarageDaysAmount, Is.EqualTo(expectedResult.Groups[ActiveGroup].AvarageDaysAmount));
                CollectionAssert.AreEqual(expectedResult.Groups[ActiveGroup].PullRequests.Select(x => x.Title),
                    result.Groups[ActiveGroup].PullRequests.Select(x => x.Title));
            });
            Assert.Multiple(() =>
            {
                Assert.That(result.Groups.ContainsKey(DraftGroup));
                Assert.That(result.Groups[DraftGroup].PullRequests.Count, Is.EqualTo(expectedResult.Groups[DraftGroup].PullRequests.Count));
                Assert.That(result.Groups[DraftGroup].AvarageDaysAmount, Is.EqualTo(expectedResult.Groups[DraftGroup].AvarageDaysAmount));
                CollectionAssert.AreEqual(expectedResult.Groups[DraftGroup].PullRequests.Select(x => x.Title),
                    result.Groups[DraftGroup].PullRequests.Select(x => x.Title));
            });
            Assert.Multiple(() =>
            {
                Assert.That(result.Groups.ContainsKey(StaleGroup));
                Assert.That(result.Groups[StaleGroup].PullRequests.Count, Is.EqualTo(expectedResult.Groups[StaleGroup].PullRequests.Count));
                Assert.That(result.Groups[StaleGroup].AvarageDaysAmount, Is.EqualTo(expectedResult.Groups[StaleGroup].AvarageDaysAmount));
                CollectionAssert.AreEqual(expectedResult.Groups[StaleGroup].PullRequests.Select(x => (x.Title, x.StaleFor)),
                    result.Groups[StaleGroup].PullRequests.Select(x => (x.Title, x.StaleFor)));
            });
        }

        public static IEnumerable<TestCaseData> GenerateTestData()
        {
            yield return new TestCaseData(
                new SearchResult
                {
                    TotalCount = 0,
                    PullRequests = new List<PullRequest> { }
                },
                new SearchResultModel
                {
                    TotalPRCount = 0,
                    AvarageDaysAmount = 0,
                    Groups = new Dictionary<string, PullRequestGroupModel>
                    {
                       { ActiveGroup, new PullRequestGroupModel { } },
                       { DraftGroup, new PullRequestGroupModel { } },
                       { StaleGroup, new PullRequestGroupModel { } }
                    }
                })
                .SetName("Empty response");

            yield return new TestCaseData(
                new SearchResult
                {
                    TotalCount = 6,
                    PullRequests = new List<PullRequest>
                    {
                        new PullRequest
                        {
                            Title = "PR1",
                            CreationDate = Now.AddDays(-2)
                        },
                        new PullRequest
                        {
                            Title = "PR2",
                            CreationDate = Now.AddDays(-10)
                        },
                        new PullRequest
                        {
                            Title = "PR3",
                            CreationDate = Now.AddDays(-10),
                            IsDraft = true,
                        },
                        new PullRequest
                        {
                            Title = "PR4",
                            CreationDate = Now.AddDays(-40),
                            IsDraft = true,
                        },
                        new PullRequest
                        {
                            Title = "PR5",
                            CreationDate = Now.AddDays(-40)
                        },
                        new PullRequest
                        {
                            Title = "PR6",
                            CreationDate = Now.AddDays(-31)
                        }
                    }
                },
                new SearchResultModel
                {
                    TotalPRCount = 6,
                    AvarageDaysAmount = 22,
                    Groups = new Dictionary<string, PullRequestGroupModel>
                    {
                       { ActiveGroup,
                            new PullRequestGroupModel
                            {
                                AvarageDaysAmount = 6,
                                PullRequests = new List<PullRequestModel>
                                {
                                    new PullRequestModel{Title = "PR1" },
                                    new PullRequestModel{Title = "PR2" }
                                }
                            }
                       },
                       { DraftGroup,
                            new PullRequestGroupModel
                            {
                                AvarageDaysAmount = 25,
                                PullRequests = new List<PullRequestModel>
                                {
                                    new PullRequestModel{Title = "PR3" },
                                    new PullRequestModel{Title = "PR4" }
                                }
                            }
                        },
                       { StaleGroup,
                            new PullRequestGroupModel
                            {
                                AvarageDaysAmount = 35,
                                PullRequests = new List<PullRequestModel>
                                {
                                    new PullRequestModel{Title = "PR5", StaleFor = 10 },
                                    new PullRequestModel{Title = "PR6", StaleFor = 1 }
                                }
                            }
                       }
                    }
                })
                .SetName("PRs for each group");

            yield return new TestCaseData(
                new SearchResult
                {
                    TotalCount = 1,
                    PullRequests = new List<PullRequest>
                    {
                        new PullRequest
                        {
                            Title = "PR1",
                            CreationDate = Now.AddDays(-5)
                        }
                    }
                },
                new SearchResultModel
                {
                    TotalPRCount = 1,
                    AvarageDaysAmount = 5,
                    Groups = new Dictionary<string, PullRequestGroupModel>
                    {
                        { ActiveGroup,
                            new PullRequestGroupModel
                            {
                                AvarageDaysAmount = 5,
                                PullRequests = new List<PullRequestModel>
                                {
                                    new PullRequestModel { Title = "PR1" }
                                }
                            }
                        },
                        { DraftGroup, new PullRequestGroupModel { } },
                        { StaleGroup, new PullRequestGroupModel { } }
                    }
                })
                .SetName("Single active PR");

            yield return new TestCaseData(
                new SearchResult
                {
                    TotalCount = 3,
                    PullRequests = new List<PullRequest>
                    {
                        new PullRequest
                        {
                            Title = "PR1",
                            CreationDate = Now.AddDays(-3),
                            IsDraft = true
                        },
                        new PullRequest
                        {
                            Title = "PR2",
                            CreationDate = Now.AddDays(-5),
                            IsDraft = true
                        },
                        new PullRequest
                        {
                            Title = "PR3",
                            CreationDate = Now.AddDays(-2),
                            IsDraft = true
                        }
                    }
                },
                new SearchResultModel
                {
                    TotalPRCount = 3,
                    AvarageDaysAmount = 3,
                    Groups = new Dictionary<string, PullRequestGroupModel>
                    {
                        { ActiveGroup, new PullRequestGroupModel { } },
                        { DraftGroup,
                            new PullRequestGroupModel
                            {
                                AvarageDaysAmount = 3,
                                PullRequests = new List<PullRequestModel>
                                {
                                    new PullRequestModel { Title = "PR1" },
                                    new PullRequestModel { Title = "PR2" },
                                    new PullRequestModel { Title = "PR3" }
                                }
                            }
                        },
                        { StaleGroup, new PullRequestGroupModel { } }
                    }
                })
                .SetName("Only draft PRs");

            yield return new TestCaseData(
                new SearchResult
                {
                    TotalCount = 2,
                    PullRequests = new List<PullRequest>
                    {
                        new PullRequest
                        {
                            Title = "PR1",
                            CreationDate = Now.AddDays(-35)
                        },
                        new PullRequest
                        {
                            Title = "PR2",
                            CreationDate = Now.AddDays(-40)
                        }
                    }
                },
                new SearchResultModel
                {
                    TotalPRCount = 2,
                    AvarageDaysAmount = 37,
                    Groups = new Dictionary<string, PullRequestGroupModel>
                    {
                        { ActiveGroup, new PullRequestGroupModel { } },
                        { DraftGroup, new PullRequestGroupModel { } },
                        { StaleGroup,
                            new PullRequestGroupModel
                            {
                                AvarageDaysAmount = 37,
                                PullRequests = new List<PullRequestModel>
                                {
                                    new PullRequestModel { Title = "PR1", StaleFor = 5 },
                                    new PullRequestModel { Title = "PR2", StaleFor = 10 }
                                }
                            }
                        }
                    }
                })
                .SetName("Only stale PRs");
        }
    }
}