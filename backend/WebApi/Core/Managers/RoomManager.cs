using AutoMapper;
using Core.Interfaces;
using Model.DTOs;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Managers
{
    public class RoomManager : IRoomManager
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public RoomManager(IMapper mapper, IRoomRepository roomRepository)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
        }
        public async Task<IEnumerable<RoomDTO>> GetAll()
        {
            var rooms = await _roomRepository.GetAll();
            return rooms.Select(r => _mapper.Map<RoomDTO>(r));
        }

        public async Task<RoomDTO?> GetRoomById(int roomId)
        {
            var room = await _roomRepository.GetRoomById(roomId);
            return _mapper.Map<RoomDTO>(room);
        }
        public async Task SaveRoom(RoomDTO room)
        {
            var newRoom = _mapper.Map<RoomEntity>(room);
            await _roomRepository.SaveRoom(newRoom);
        }

        public async Task ModifyRoom(RoomDTO room)
        {
            await _roomRepository.ModifyRoom(_mapper.Map<RoomEntity>(room));
        }

        public async Task DeleteRoom(RoomDTO room)
        {
            await _roomRepository.DeleteRoom(_mapper.Map<RoomEntity>(room));
        }


    }
}
