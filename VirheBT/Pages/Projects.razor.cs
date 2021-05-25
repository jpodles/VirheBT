using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazorise.DataGrid;

using VirheBT.Data.Enums;

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
        public int currentPage { get; set; } = 1;

        List<ProjectShortDto> data = new List<ProjectShortDto>
        {
            new ProjectShortDto
            {
                ID = 1,
                Name = "test1",
                Status = ProjectStatus.Finished,
                Maintainer = "Michałek kox"
            },
            new ProjectShortDto
            {
                ID = 2,
                Name = "test1",
                Status = ProjectStatus.OnTrack,
                Maintainer = "Michałek kox"
            },
            new ProjectShortDto
            {
                ID = 3,
                Name = "test2",
                Status = ProjectStatus.Canceled,
                Maintainer = "Michałek kox"
            },
            new ProjectShortDto
            {
                ID = 4,
                Name = "test3",
                Status = ProjectStatus.Finished,
                Maintainer = "Mareczek Pogczamp"
            }
        };
    }

    public class ProjectShortDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ProjectStatus Status { get; set; }
        public string Maintainer { get; set; }
    }

}
