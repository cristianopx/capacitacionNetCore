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
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto?> GetById(int id);
        Task<UserDto?> GetByUsername(string username);
        Task SaveAsync(UserDto newUser);
        Task ChangePassword(int userId, string password);
        Task DeleteAsync(int id);
    }
}
