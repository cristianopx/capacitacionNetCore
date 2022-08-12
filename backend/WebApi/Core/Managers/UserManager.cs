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
            return _appDbContext
                        .Users
                        .AsEnumerable()
                        .Select(x => _mapper.Map<UserEntity, UserDto>(x))
                        .ToList();
        }
    }
}
