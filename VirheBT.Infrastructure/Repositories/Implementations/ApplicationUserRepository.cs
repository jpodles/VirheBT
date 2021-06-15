using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using VirheBT.Infrastructure.Data;
using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;
using VirheBT.Shared.Enums;

namespace VirheBT.Infrastructure.Repositories.Implementations
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ApplicationUserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async void DeactivateUserAsync(string userId)
        {
            var userEntity = await GetApplicationUserAsync(userId);
            userEntity.UserStatus = UserStatus.Inactive;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(ApplicationUser applicationUser, string userId)
        {
            var userEntity = await GetApplicationUserAsync(userId);
            userEntity.FirstName = applicationUser.FirstName;
            userEntity.LastName = applicationUser.LastName;
            userEntity.UserStatus = applicationUser.UserStatus;
            //_mapper.Map(applicationUser, userEntity);
            _context.SaveChanges();



        }

        public async Task<ApplicationUser> GetApplicationUserAsync(string userId)
        {
            return await _context.ApplicationUsers.Include(x => x.Projects)
                .Where(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetApplicationUserByEmailAsync(string userEmail)
        {
            return await _context.ApplicationUsers
                .Where(u => u.Email == userEmail).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync()
        {
            return await _context.ApplicationUsers.ToListAsync();
        }
    }
}
