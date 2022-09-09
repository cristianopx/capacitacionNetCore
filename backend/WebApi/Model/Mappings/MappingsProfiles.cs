using AutoMapper;
using Model.DTOs;
using Model.Entity;

namespace Model.Mappings
{
    public class MappingsProfiles : Profile
    {
        public MappingsProfiles()
        {
            UserMapping();
        }

        private void UserMapping()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserDto, UserEntity>();
            CreateMap<RoomEntity,RoomDTO>();
            CreateMap<RoomDTO,RoomEntity>();
        }
    }
}
