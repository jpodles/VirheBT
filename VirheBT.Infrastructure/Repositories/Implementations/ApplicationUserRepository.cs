using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;

namespace VirheBT.Infrastructure.Repositories.Implementations
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        public async void DeactivateUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async void EdituserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetApplicationUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
