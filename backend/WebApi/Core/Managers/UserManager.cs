using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces;
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

        public async Task<UserDto> Save(UserDto user)
        {
            var newUser = _mapper.Map<UserEntity>(user);
            newUser.DateCreated = DateTime.Now;
            _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<UserDto>(newUser);
        }
    }
}
