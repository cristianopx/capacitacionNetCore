using Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRoomManager
    {
        Task<IEnumerable<RoomDTO>> GetAll();
        Task<RoomDTO?> GetRoomById(int roomId);
        Task SaveRoom(RoomDTO room);
        Task ModifyRoom(RoomDTO room);
        Task DeleteRoom(RoomDTO room);
    }
}
