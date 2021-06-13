using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;


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
        public DataGrid<Project> dataGrid;
        public List<Project> data = new List<Project>();
        public int currentPage { get; set; } = 1;



        [Inject]
        IProjectService ProjectService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            data = await ProjectService.GetProjectsAsync();

        }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    data = await ProjectService.GetProjectsAsync();

        //}




    }



}
