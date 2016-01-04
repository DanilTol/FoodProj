using System.Linq;
using AutoMapper;
using FoodService.Business.DTO;
using FoodService.DAL.Entity;

namespace FoodService.Business.Mapping
{
    public class InitMap
    {
        public void InitAllMaps()
        {
            Mapper.CreateMap<Dish, DishModelShortInfo>();
            Mapper.CreateMap<DishModelDetailsInfo, Dish>();

            Mapper.CreateMap<DishModelDetailsWithFile, Dish>();
            Mapper.CreateMap<Dish,DishModelDetailsInfo>();
            Mapper.CreateMap<UserDTO, User>();
            Mapper.CreateMap<User, UserDTO>();
            Mapper.CreateMap<UserDTO, LogInUser>();
        }
    }
}