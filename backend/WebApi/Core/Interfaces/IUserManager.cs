using Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserManager
    {
        IEnumerable<UserDto> GetAll();
        UserDto? GetById(int id);
        UserDto? GetByUsername(string username);
        Task<UserDto> SaveAsync(UserDto user);
        Task PutAsync(int id, UserDto user);
        Task DeleteAsync(int id);
    }
}
