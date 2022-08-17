using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.DTOs;
using Model.Entity;

namespace Core.Managers
{
    public class UserManager : IUserManager
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UserManager(AppDbContext appDbContext, IMapper  mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public IEnumerable<UserDto> GetAll()
        {
            Console.WriteLine(_appDbContext.Users);
            return _appDbContext
                        .Users
                        .AsEnumerable()
                        .Select(x => _mapper.Map<UserEntity, UserDto>(x))
                        .ToList();
        }

        public UserDto? GetById(int id)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Id == id);
            return _mapper.Map<UserDto>(user);
        }

        public UserDto? GetByUsername(string username)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Username == username);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> SaveAsync(UserDto user)
        {
            var newUser = _mapper.Map<UserEntity>(user);
            newUser.DateCreated = DateTime.Now;
            _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<UserDto>(newUser);
        }

        public async Task PutAsync(int id, UserDto user)
        {
            var userToModified = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userToModified != null)
            {
                userToModified.Password = user.Password;
                userToModified.Fullname = user.Fullname;
                _appDbContext.Users.Update(userToModified);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
                _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
