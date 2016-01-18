using System.Linq;
using AutoMapper;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.Business.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateUser(UserDTO userDto)
        {
            var user = Mapper.Map<UserDTO, User>(userDto);
            user.Role = Database.Role.QueryToTable.FirstOrDefault(x => x.Name == "user");
            Database.User.Add(user);
            Database.Save();
        }

        public void EditUser(UserDTO userDto)
        {
            var user = Mapper.Map<UserDTO, User>(userDto);
            user.Role = Database.Role.QueryToTable.FirstOrDefault(x => x.Name == userDto.Role);
            Database.User.Update(user);
            Database.Save();
        }

        public bool Login(LogInUser inUser)
        {
            return Database.User.QueryToTable.Any(x => x.EmailAddress == inUser.Email && x.Salt == inUser.Salt);
        }

        public UserDTO GetUserInfo(string email)
        {
            var userFromDb = Database.User.QueryToTable.FirstOrDefault(x => x.EmailAddress == email);
            var userdto = Mapper.Map<User, UserDTO>(userFromDb);
            if (userFromDb != null) userdto.Role = userFromDb.Role.Name;
            return userdto;
        }

        public User GetUser(int id)
        {
            return Database.User.QueryToTable.FirstOrDefault(x => x.id == id);
        }
    }
}