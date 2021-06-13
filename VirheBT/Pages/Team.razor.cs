using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.Enums;

namespace VirheBT.Pages
{

    public class UserShortDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
    }
    public partial class Team
    {
        [Parameter]
        public int ProjectId { get; set; }

        [Inject]
        IProjectService ProjectService { get; set; }

        [Inject]
        IApplicationUserService ApplicationUserService { get; set; }

        bool editable = true;
        bool sortable = true;
        bool filterable = true;
        bool showPager = true;
        DataGridEditMode editMode = DataGridEditMode.Popup;
        DataGridSortMode sortMode = DataGridSortMode.Single;
        DataGridSelectionMode selectionMode = DataGridSelectionMode.Single;
        DataGridCommandMode commandsMode = DataGridCommandMode.Default;
        public DataGrid<ApplicationUser> dataGrid;
        public int currentPage { get; set; } = 1;


        List<ApplicationUser> data = new List<ApplicationUser>();

        List<ApplicationUser> appUsers = new List<ApplicationUser>();
        protected async override Task OnInitializedAsync()
        {
            data = await ProjectService.GetProjectUsersAsync(ProjectId);
            appUsers = await ApplicationUserService.GetApplicationUsersAsync();
        }



        private string selectedSearchValue { get; set; }

        void MySearchHandler(string newValue)
        {
            selectedSearchValue = newValue;
            Console.WriteLine("search value =" + selectedSearchValue);
        }

        async Task OnAddUserToProject()
        {
            await ProjectService.AddUserToProjectAsync(selectedSearchValue, ProjectId);
            data = await ProjectService.GetProjectUsersAsync(ProjectId);
            StateHasChanged();
        }

        async Task OnRowRemoved(ApplicationUser applicationUser)
        {

            await ProjectService.RemoveUserFromProject(applicationUser.Id, ProjectId);
            data = await ProjectService.GetProjectUsersAsync(ProjectId);
            StateHasChanged();
        }


        //List<UserShortDto> data = new List<UserShortDto>
        //{
        //    new UserShortDto
        //    {
        //        ID = 1,
        //        Name = "Michał Nowak",
        //        Email = "michał@virhe.com",
        //        UserRole = UserRole.Tester
        //    },
        //    new UserShortDto
        //    {
        //        ID = 2,
        //        Name = "Jan Kowalski",
        //        Email = "jan@virhe.com",
        //        UserRole = UserRole.Programmer
        //    },
        //    new UserShortDto
        //    {
        //        ID = 3,
        //        Name = "Tomasz Kowalski",
        //        Email = "tomek@virhe.com",
        //        UserRole = UserRole.ProjectManager
        //    },
        //    new UserShortDto
        //    {
        //        ID = 4,
        //        Name = "Jakub Podleś",
        //        Email = "jakub@virhe.com",
        //        UserRole = UserRole.Programmer
        //    },
        //    new UserShortDto
        //    {
        //        ID = 5,
        //        Name = "Adam Nowak",
        //        Email = "adam@virhe.com",
        //        UserRole = UserRole.Tester
        //    },
        //    new UserShortDto
        //    {
        //        ID = 5,
        //        Name = "Adam Nowak",
        //        Email = "adam@virhe.com",
        //        UserRole = UserRole.Admin
        //    }
        //};
    }


}
