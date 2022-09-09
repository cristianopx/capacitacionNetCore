using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _appDbContext;
        public RoomRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<RoomEntity>> GetAll()
        {
            return await _appDbContext.Rooms.ToListAsync();
        }

        public async Task<RoomEntity?> GetRoomById(int roomId)
        {
            return await _appDbContext.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId);
        }

        public async Task SaveRoom(RoomEntity room)
        {
            _appDbContext.Rooms.Add(room);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ModifyRoom(RoomEntity room)
        {
            _appDbContext.Update(room);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteRoom(RoomEntity room)
        {
            _appDbContext.Rooms.Remove(room);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
