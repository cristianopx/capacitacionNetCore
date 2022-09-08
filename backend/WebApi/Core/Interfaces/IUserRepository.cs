using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity?> GetById(int userId);
        Task<UserEntity?> GetByUsername(string username);
        Task SaveUser(UserEntity user);
        Task ModifyUser(int userId, UserEntity user);
        Task DeleteUser(int userId);
    }
}
