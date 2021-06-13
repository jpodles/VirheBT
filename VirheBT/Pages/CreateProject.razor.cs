using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;


using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;

namespace VirheBT.Pages
{
    public partial class CreateProject
    {
        string title;
        string description;


        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        IProjectService ProjectService { get; set; }

        public async void OnCreateAsync()
        {
            var authState = await AuthenticationStateProvider
                .GetAuthenticationStateAsync();
            var user = authState.User.Identity.Name;

            ProjectService.CreateProject(title, description, user);



        }
    }
}
