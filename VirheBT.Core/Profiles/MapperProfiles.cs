using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Shared.DTOs;

namespace VirheBT.Core.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Project, ProjectShortDto>();
            CreateMap<Project, Project>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUser>();
        }
    }
}
