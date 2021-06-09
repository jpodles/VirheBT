using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazorise.DataGrid;

using VirheBT.Shared.Enums;

namespace VirheBT.Pages
{
    public partial class ProjectDetails
    {
        bool editable = true;
        bool sortable = true;
        bool filterable = true;
        bool showPager = true;
        DataGridEditMode editMode = DataGridEditMode.Popup;
        DataGridSortMode sortMode = DataGridSortMode.Single;
        DataGridSelectionMode selectionMode = DataGridSelectionMode.Single;
        DataGridCommandMode commandsMode = DataGridCommandMode.Default;
        public DataGrid<UserShortDto> dataGrid;
        public int currentPage { get; set; } = 1;

        List<UserShortDto> data = new List<UserShortDto>
        {
            new UserShortDto
            {
                ID = 1,
                Name = "Michał Nowak",
                Email = "michał@virhe.com",
                UserRole = UserRole.Tester
            },
            new UserShortDto
            {
                ID = 2,
                Name = "Jan Kowalski",
                Email = "jan@virhe.com",
                UserRole = UserRole.Programmer
            },
            new UserShortDto
            {
                ID = 3,
                Name = "Tomasz Kowalski",
                Email = "tomek@virhe.com",
                UserRole = UserRole.ProjectManager
            },
            new UserShortDto
            {
                ID = 4,
                Name = "Jakub Podleś",
                Email = "jakub@virhe.com",
                UserRole = UserRole.Programmer
            },
            new UserShortDto
            {
                ID = 5,
                Name = "Adam Nowak",
                Email = "adam@virhe.com",
                UserRole = UserRole.Tester
            },
            new UserShortDto
            {
                ID = 5,
                Name = "Adam Nowak",
                Email = "adam@virhe.com",
                UserRole = UserRole.Admin
            }
        };
    }
}
