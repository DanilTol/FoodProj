using System.Collections.Generic;
using FoodService.Business.DTO;

namespace FoodService.Business.ServiceInterfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO userDto);
        void EditUser(UserDTO userDto);
        bool Login(LogInUser inUser);
        UserDTO GetUserInfo(string email);
        IEnumerable<UserDTO> GetAllUsers();
    }
}