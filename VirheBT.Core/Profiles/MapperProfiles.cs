namespace VirheBT.Core.Profiles;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<IssueHistory, IssueHistoryDto>().ReverseMap();
        CreateMap<IssueComment, IssueCommentDto>().ReverseMap();
        
        CreateMap<CreateIssueDto, Issue>();
        CreateMap<EditIssueDto, Issue>();
        
        CreateMap<CreateCommentDto, IssueComment>();
        CreateMap<EditCommentDto, IssueComment>();
        
        CreateMap<CreateProjectDto, Project>();
        CreateMap<UpdateProjectDto, Project>();

        CreateMap<Issue, IssueDto>().ReverseMap();
        CreateMap<UpdateProjectDto, Project>().ReverseMap();
        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
    }
}