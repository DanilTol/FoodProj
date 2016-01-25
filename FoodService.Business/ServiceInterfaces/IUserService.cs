using System.Collections.Generic;
using FoodService.Business.DTO;
using FoodService.DAL.Entity;

namespace FoodService.Business.ServiceInterfaces
{
    public interface IUserService
    {
        bool CreateUser(UserDTO userDto);
        bool EditUser(UserEdit userDto);
        User Login(LogInUser inUser);
        UserDTO GetUser(int id);
        User GetUserEntity(int id);
        List<UserDTO> GetAll(int currentUserID);
        void DeleteUser(int userID);
        void EditAsAdmin(UserEdit userEdit);
    }
}