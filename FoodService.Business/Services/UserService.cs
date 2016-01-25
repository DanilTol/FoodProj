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

        public bool CreateUser(UserDTO userDto)
        {
            if (Database.User.QueryToTable.Any(x => x.EmailAddress == userDto.EmailAddress))
            {
                return false;
            }
            var user = Mapper.Map<UserDTO, User>(userDto);
            user.Role = Database.Role.QueryToTable.FirstOrDefault(x => x.Name == "user");
            Database.User.Add(user);
            Database.Save();
            return true;
        }

        public bool EditUser(UserEdit userEdit)
        {
            var userDb = Database.User.QueryToTable.FirstOrDefault(x => x.id == userEdit.Id);
            if (userEdit.Salt.GetHashCode().ToString() != userDb.Salt)
            {
                return false;
            }
            userDb.Name = userEdit.Name;
            userDb.EmailAddress = userEdit.EmailAddress;
            if(!string.IsNullOrEmpty(userEdit.NewSalt))
                userDb.Salt = userEdit.NewSalt.GetHashCode().ToString();
            Database.User.Update(userDb);
            Database.Save();

            return true;
        }

        public User Login(LogInUser inUser)
        {
            return Database.User.QueryToTable.FirstOrDefault(x => x.EmailAddress == inUser.Email && x.Salt == inUser.Salt);
        }

        public UserDTO GetUser(int id)
        {
            var userFromDb = Database.User.QueryToTable.FirstOrDefault(x => x.id == id);
            var userdto = Mapper.Map<User, UserDTO>(userFromDb);
            if (userFromDb != null) userdto.Role = userFromDb.Role.Name;
            return userdto;
        }

        public User GetUserEntity(int id)
        {
            return Database.User.QueryToTable.FirstOrDefault(x => x.id == id);
        }

       }
}