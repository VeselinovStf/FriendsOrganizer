using AutoMapper;
using FriendsOrganizer.Data.Models;
using FriendsOrganizer.Friends.Service.DTOs;
using FriendsOrganizer.UI.Models;

namespace FriendsOrganizer.UI.Mapper
{
    public class AppAutoMapperProfile : Profile
    {
        public AppAutoMapperProfile()
        {
            CreateMap<FriendDTO, FriendModel>();
            CreateMap<Friend, FriendDTO>();

        }
    }
}
