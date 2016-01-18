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
            Mapper.CreateMap<Dish, DishModelDetailsInfo>();

            Mapper.CreateMap<DishModelDetailsInfo, Dish>();

            Mapper.CreateMap<UserDTO, LogInUser>();
            Mapper.CreateMap<UserDTO, User>();
            Mapper.CreateMap<User, UserDTO>();
        }
    }
}