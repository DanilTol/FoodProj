using System.Collections.Generic;
using FoodService.Business.DTO;
using FoodService.DAL.Entity;

namespace FoodService.Business.ServiceInterfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO userDto);
        bool EditUser(UserDTO userDto);
        User Login(LogInUser inUser);
        UserDTO GetUser(int id);
        User GetUserEntity(int id);
    }
}