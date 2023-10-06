using AutoMapper;
using GitHubPRSearch.Clients.Models;
using GitHubPRSearch.Models;

namespace GitHubPRSearch.Services
{
    public class MapProfile: Profile
    {        public MapProfile()
        {
            CreateMap<PullRequest, PullRequestModel>();
            CreateMap<User, UserModel>();
            CreateMap<Commit, CommitModel>()
                .ForMember(x => x.Date, y => y.MapFrom(x => x.Message.Committer.Date))
                .ForMember(x => x.Message, y => y.MapFrom(x => x.Message.Message));
        }
    }
}
