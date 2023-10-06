using GitHubPRSearch.Models;
using GitHubPRSearch.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GitHubPRSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchService _searchService;

        public HomeController(ILogger<HomeController> logger, ISearchService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchRequest request)
        {
            var result = await _searchService.Search(request);

            return View("SearchResult", result);
        }
    }
}