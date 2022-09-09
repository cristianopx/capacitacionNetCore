using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<RoomEntity>> GetAll();
        Task<RoomEntity?> GetRoomById(int roomId);
        Task SaveRoom(RoomEntity room);
        Task ModifyRoom(RoomEntity room);
        Task DeleteRoom(RoomEntity roomId);
    }
}
