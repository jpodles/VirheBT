using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;

using VirheBT.Services.Interfaces;
using VirheBT.Shared.DTOs;
using VirheBT.Shared.Enums;

namespace VirheBT.Pages
{

    public partial class Projects
    {
        bool editable = true;
        bool sortable = true;
        bool filterable = true;
        bool showPager = true;
        DataGridEditMode editMode = DataGridEditMode.Popup;
        DataGridSortMode sortMode = DataGridSortMode.Single;
        DataGridSelectionMode selectionMode = DataGridSelectionMode.Single;
        DataGridCommandMode commandsMode = DataGridCommandMode.Default;
        public DataGrid<ProjectShortDto> dataGrid;
        //public List<ProjectShortDto> data;
        public int currentPage { get; set; } = 1;

        //[Inject]
        //IProjectService ProjectService { get; set; }

        //protected async override Task OnInitializedAsync()
        //{
        //    data = await ProjectService.GetProjectsAsync();
        //}

        List<ProjectShortDto> data = new List<ProjectShortDto>
        {
            new ProjectShortDto
            {
                ProjectId = 1,
                Name = "test1",
                Status = ProjectStatus.Finished,
                Maintainer = null
            },

        };
    }



}
