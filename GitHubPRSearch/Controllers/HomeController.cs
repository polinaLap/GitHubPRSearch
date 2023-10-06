using GitHubPRSearch.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GitHubPRSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Search(string owner, string repository, string tag, string keywords)
        {
            var mockResult = new SearchResultModel
            {
                Groups = new[]
                {
                    new PullRequestGroupModel
                {
                        GroupName = "Active",
                    PullRequests = new[]
                    {
                        new PullRequestModel
                        {
                            Uri = new Uri("https://google.com"),
                            Title = "Title1",
                            Description = "Description1",
                            CommentsAmmount = 1,
                            CreationDate = DateTime.Now.AddDays(-2),
                            Creator = new UserModel
                            {
                                Login = "login1",
                                AvatarLink = new Uri("https://avatars.githubusercontent.com/in/15368?v=4")
                            },
                            Commits = new[]
                            {
                                new CommitModel
                                {
                                    Hash = Guid.NewGuid().ToString(),
                                    Date = DateTime.Now.AddDays(-1),
                                    Message = "message1",
                                    Author = new UserModel
                            {
                                Login = "login1",
                                AvatarLink = new Uri("https://google.com")
                            }
                                }
                            }
                        },
                        new PullRequestModel
                        {
                            Uri = new Uri("https://google.com"),
                            Title = "Title2",
                            Description = "Description2",
                            CommentsAmmount = 2,
                            CreationDate = DateTime.Now.AddDays(-1),
                            Creator = new UserModel
                            {
                                Login = "login2",
                                AvatarLink = new Uri("https://avatars.githubusercontent.com/in/15368?v=4")
                            },
                            Commits = new[]
                            {
                                new CommitModel
                                {
                                    Hash = Guid.NewGuid().ToString(),
                                    Date = DateTime.Now.AddDays(-1),
                                    Message = "message2",
                                    Author = new UserModel
                            {
                                Login = "login2",
                                AvatarLink = new Uri("https://google.com")
                            }
                                }
                            }
                        }
                    },
                    AvarageDaysAmount = 1,
                },
                    new PullRequestGroupModel
                {
                        GroupName ="Draft",
                    PullRequests = new[]
                    {
                        new PullRequestModel
                        {
                            Uri = new Uri("https://google.com"),
                            Title = "Title1",
                            Description = "Description1",
                            CommentsAmmount = 1,
                            CreationDate = DateTime.Now.AddDays(-2),
                            Creator = new UserModel
                            {
                                Login = "login1",
                                AvatarLink = new Uri("https://avatars.githubusercontent.com/in/15368?v=4")
                            },
                            Commits = new[]
                            {
                                new CommitModel
                                {
                                    Hash = Guid.NewGuid().ToString(),
                                    Date = DateTime.Now.AddDays(-1),
                                    Message = "message1",
                                    Author = new UserModel
                            {
                                Login = "login1",
                                AvatarLink = new Uri("https://avatars.githubusercontent.com/in/15368?v=4")
                            }
                                }
                            }
                        }
                    },
                    AvarageDaysAmount = 1,
                },
                    new PullRequestGroupModel
                {
                        GroupName ="Draft",
                    PullRequests = new List<PullRequestModel> { },
                    AvarageDaysAmount = 0
                } },
                AvarageDaysAmount = 4
            };

            return View("SearchResult", mockResult);
        }
    }
}