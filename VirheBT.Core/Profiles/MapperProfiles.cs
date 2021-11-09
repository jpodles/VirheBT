using AutoMapper;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Shared.DTOs;

namespace VirheBT.Core.Profiles;
public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<Project, ProjectShortDto>();
        CreateMap<Project, Project>().ReverseMap();
        CreateMap<IssueHistory, IssueHistoryDto>();
        CreateMap<IssueComment, IssueCommentDto>();
        CreateMap<ApplicationUser, ApplicationUserDto>();
    }
}
