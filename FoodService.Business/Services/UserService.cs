using System.Collections.Generic;
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
            var user =  Mapper.Map<UserDTO, User>(userDto);
            Database.User.Add(user);
            Database.Save();
        }

        public void EditUser(UserDTO userDto)
        {
            var user = Mapper.Map<UserDTO, User>(userDto);
            Database.User.Update(user);
            Database.Save();
        }

        public bool Login(LogInUser inUser)
        {
            return  Database.User.QueryToTable.Any(x => x.EmailAddress == inUser.Email && x.Salt == inUser.Salt);
            //Database.User.QueryToTable.Where(x => x.EmailAddress == inUser.Email && x.Salt == inUser.Salt);
            //return answer;
        }

        public UserDTO GetUserInfo(string email)
        {
            var z = Database.User.QueryToTable.FirstOrDefault(x => x.EmailAddress == email);
            var userdto = Mapper.Map<User, UserDTO>(z);
            userdto.Role = z.Role.Name;
            return userdto;
        }

        public UserDTO GetUserById(int id)
        {
            return Mapper.Map<User,UserDTO>(Database.User.QueryToTable.FirstOrDefault(x => x.ID == id));
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }
    }
}