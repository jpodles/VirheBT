using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;

using System.Collections.Generic;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;

namespace VirheBT.Pages
{
    public partial class Projects
    {
        private bool editable = true;
        private bool sortable = true;
        private bool filterable = true;
        private bool showPager = true;
        private DataGridEditMode editMode = DataGridEditMode.Popup;
        private DataGridSortMode sortMode = DataGridSortMode.Single;
        private DataGridSelectionMode selectionMode = DataGridSelectionMode.Single;
        private DataGridCommandMode commandsMode = DataGridCommandMode.Default;
        public DataGrid<Project> dataGrid;
        public List<Project> data = new List<Project>();
        public int currentPage { get; set; } = 1;

        [Inject]
        private IProjectService ProjectService { get; set; }

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