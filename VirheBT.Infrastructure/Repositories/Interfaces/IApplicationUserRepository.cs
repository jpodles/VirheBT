﻿using System.Collections.Generic;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;

namespace VirheBT.Infrastructure.Repositories.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync();

        Task<ApplicationUser> GetApplicationUserAsync(string userId);

        void EdituserAsync(string userId);

        //to chyba nie ma miejsca
        //void AddUser(ApplicationUser user);

        //Delete/Deactivate
        void DeactivateUserAsync(string userId);


    }
}