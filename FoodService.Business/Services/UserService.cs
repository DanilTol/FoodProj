﻿using System.Collections.Generic;
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

        public void EditAsAdmin(UserEdit userEdit)
        {
            if (userEdit.Id == 0)
            {
                Database.User.Add(new User() {EmailAddress = userEdit.EmailAddress,Name = userEdit.Name, Role = Database.Role.QueryToTable.FirstOrDefault(x => x.Name == userEdit.Role), Salt = userEdit.Salt});
                Database.Save();
               
            }
            else
            {


                var userDb = Database.User.QueryToTable.FirstOrDefault(x => x.id == userEdit.Id);
                userDb.Name = userEdit.Name;
                userDb.EmailAddress = userEdit.EmailAddress;
                if (!string.IsNullOrEmpty(userEdit.Salt))
                    userDb.Salt = userEdit.Salt.GetHashCode().ToString();
                userDb.Role = Database.Role.QueryToTable.FirstOrDefault(x => x.Name == userEdit.Role);
                Database.User.Update(userDb);
                Database.Save();
            }
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

        public List<UserDTO> GetAll(int currentUserID)
        {
            var userFromDb = Database.User.QueryToTable.Where(x => x.id>1 && x.id != currentUserID);
            var usersdto = Mapper.Map<IQueryable<User>, List<UserDTO>>(userFromDb);
            foreach (var user in usersdto)
            {
                user.Role = userFromDb.FirstOrDefault(x => x.id == user.Id).Role.Name;
                user.Salt = "";
            }
            return usersdto;

        }

        public void DeleteUser(int userID)
        {
            Database.User.Delete(Database.User.QueryToTable.FirstOrDefault(x => x.id == userID));
            Database.Save();
        }

       }
}