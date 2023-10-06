# GitHub Pull-requests Search
## Task Description
Your task is to create an **ASP.NET Core MVC** applicaiton which has a controller with a method that searches Github and displays the latest **open** pull-requests in the specified repository, on specified topic, and with a list of commits which this pull-request consists of. 
The input parameters of this method are: `repository owner`, `repository name`, `pull-request label (tag)` and custom search keywords.
The output of the method should contain a list of **open** pull-requests, and inside every one of them there should be an array of commits. A **pull-request should contain**: a URL to its pull-request page, the title, shortened description, number of comments, creation date, creator's name and email, a link to their avatar. A **commit should include** the commit hash, the author name and email, link to their avatar, a commit date and a commit message. You may include other fields in the models, if you deem them worthy of being added to the API.

### Additional logic
Some pull-requests are explicitly marked as **drafts** and should be put into a separate group in the model.
If a pull-request has been open for **more than a month**, and it is **not a draft**, it is considered stale, and should be put into the stale requests array, separately from the others.

```
{
    ...
    "pullRequests":
    {
        "active": {}
        "draft": {}
        "stale": {}
    }
    ...
}
```

These stale PRs should include a field displaying how many days they have been stale.
Also, please calculate an average amount of days that these pull-requests have been open, for active, draft and stale group separately, and also for all groups together.

### Unit-testing
The logic under the "Additional logic" section above should be unit-tested. The preferred testign framework is NUnit. The data for the tests should be mocked, obviously.

### Additionally
You should make a Github-repository of this solution, and this repository should have a README.md file, describing how to launch and use this solution.

### References
I suggest debugging it using the [dotnet/runtime respository](<a target="_blank" class="c-link" data-stringify-link="https://github.com/dotnet/runtime/" delay="150" data-sk="tooltip_parent" href="https://github.com/dotnet/runtime/" rel="noopener noreferrer" style="box-sizing: inherit; color: inherit; text-decoration: none;">https://github.com/dotnet/runtime/</a>), which has rich sample of data for that purpose.
You have to directly query the Github API, docs to which can be cound here: [Getting started with the REST API](<a target="_blank" class="c-link" data-stringify-link="https://docs.github.com/en/rest/guides/getting-started-with-the-rest-api?apiVersion=2022-11-28" delay="150" data-sk="tooltip_parent" href="https://docs.github.com/en/rest/guides/getting-started-with-the-rest-api?apiVersion=2022-11-28" rel="noopener noreferrer" style="box-sizing: inherit; color: inherit; text-decoration: none;">https://docs.github.com/en/rest/guides/getting-started-with-the-rest-api?apiVersion=2022-11-28</a>). 
This task should be able to complete with anonymous requests to Github.

## How to launch?
1. Pull repository files
2. Run the application in IDE or with `dotnet run` command in the *GitHubPRSearch* folder
3. Open http://localhost:5153/ in the browser
4. Fill out the form and click Search.

Example of search input:

- Repository Owner: `dotnet` (mandatory field)
- Repository Name: `runtime` (mandatory field)
- PR Tag: `area-Infrastructure-mono`
- Custom Search Words: `enable`

