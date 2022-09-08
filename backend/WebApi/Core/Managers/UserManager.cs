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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManager( IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return users.Select(u => _mapper.Map<UserDto>(u));
        }
        public async Task<UserDto?> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetByUsername(string username)
        {
            var user = await _userRepository.GetByUsername(username);
            if(user != null)
                return _mapper.Map<UserDto>(user);
            return null;
        }

        public async Task SaveAsync(UserDto newUser)
        {
            newUser.DateCreated = DateTime.Now;
            await _userRepository.SaveUser(_mapper.Map<UserEntity>(newUser));
        }

        public async Task ChangePassword(int userId, string password)
        {
            var user = await _userRepository.GetById(userId);
            if (user != null)
            {
                user.Password = password;
                await _userRepository.ModifyUser(userId, user);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteUser(id);
        }
    }
}
