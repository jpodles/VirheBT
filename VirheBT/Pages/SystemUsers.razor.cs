﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazorise.DataGrid;

using VirheBT.Data.Enums;

namespace VirheBT.Pages
{
    public class UserDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
        public UserStatus UserStatus { get; set; }
    }

    public partial class SystemUsers
    {
        private bool editable = true;
        private bool sortable = true;
        private bool filterable = true;
        private bool showPager = true;
        private DataGridEditMode editMode = DataGridEditMode.Popup;
        private DataGridSortMode sortMode = DataGridSortMode.Single;
        private DataGridSelectionMode selectionMode = DataGridSelectionMode.Single;
        private DataGridCommandMode commandsMode = DataGridCommandMode.Default;
        public DataGrid<UserDto> dataGrid;
        public int currentPage { get; set; } = 1;

        private List<UserDto> data = new List<UserDto>
        {
            new UserDto
            {
                ID = 1,
                FirstName = "Michał",
                LastName =" Nowak",
                Email = "michał@virhe.com",
                UserRole = UserRole.Tester,
                UserStatus = UserStatus.Active
            },
            new UserDto
            {
                ID = 1,
                FirstName = "Michał",
                LastName =" Nowak",
                Email = "michał@virhe.com",
                UserRole = UserRole.Tester,
                UserStatus = UserStatus.Active
            },
            new UserDto
            {
                ID = 1,
                FirstName = "Michał",
                LastName =" Nowak",
                Email = "michał@virhe.com",
                UserRole = UserRole.Tester,
                UserStatus = UserStatus.Inactive
            },
            new UserDto
            {
                ID = 1,
                FirstName = "Michał",
                LastName =" Nowak",
                Email = "michał@virhe.com",
                UserRole = UserRole.Tester,
                UserStatus = UserStatus.Active
            },
             new UserDto
            {
                ID = 1,
                FirstName = "Michał",
                LastName =" Nowak",
                Email = "michał@virhe.com",
                UserRole = UserRole.Tester,
                UserStatus = UserStatus.Active
            }
        };
    }
}