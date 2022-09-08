using AutoMapper;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Managers
{
    public class LoginManager : ILoginManager
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public LoginManager(AppDbContext appDbContext, IMapper mapper)
        {
            this._appDbContext = appDbContext;
            this._mapper = mapper;
        }
        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            return user != null;
        }

        public Task<bool> LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
