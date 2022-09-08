using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<UserEntity?> GetById(int userId)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<UserEntity?> GetByUsername(string username)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public async Task SaveUser(UserEntity user)
        {
            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ModifyUser(int userId, UserEntity userModified)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.Username = userModified.Username;
                user.Password = userModified.Password;
                user.Fullname = userModified.Fullname;
            }
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
                _appDbContext.Remove(user);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
