using AutoMapper;
using FriendsOrganizer.Data.Models;
using FriendsOrganizer.Friends.Service.DTOs;
using FriendsOrganizer.UI.Models;
using FriendsOrganizer.UI.ViewModels;

namespace FriendsOrganizer.UI.Mapper
{
    public class AppAutoMapperProfile : Profile
    {
        public AppAutoMapperProfile()
        {
            CreateMap<FriendDTO, FriendModel>();
            CreateMap<FriendModel, FriendDTO>();
            CreateMap<Friend, FriendDTO>();
            CreateMap<FriendDTO, Friend>();

            CreateMap<FriendDTO, NavigationViewItemModel>()
                .ForCtorParam("id", opt => opt.MapFrom(s => s.Id))
                .ForCtorParam("displayProperty", opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}
