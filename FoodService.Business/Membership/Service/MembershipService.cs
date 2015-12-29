//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using FoodService.Business.Membership.Interface;
//using FoodService.Business.ServiceInterfaces;
//using FoodService.DAL.Entity;
//using FoodService.DAL.Interfaces;

//namespace FoodService.Business.Membership.Service
//{
//    public class MembershipService : IMembershipService
//    {
//        private readonly IUnitOfWork Database;
        
//        public MembershipService(IUnitOfWork uow)
//        {
//            Database = uow;
//        }
        
//        public MembershipContext ValidateUser(string email, string password)
//        {
//            var membershipCtx = new MembershipContext();

//            var user = Database.User.GetAll.FirstOrDefault(x => x.EmailAddress == email);
//            if (user != null && isUserValid(user, password))
//            {
//                var userRoles = GetUserRoles(user.Username);
//                membershipCtx.User = user;

//                var identity = new GenericIdentity(user.Username);
//                membershipCtx.Principal = new GenericPrincipal(
//                    identity,
//                    userRoles.Select(x => x.Name).ToArray());
//            }

//            return membershipCtx;
//        }

//        public User CreateUser(string username, string email, string password, int[] roles)
//        {
//            var existingUser = _userRepository.GetSingleByUsername(username);

//            if (existingUser != null)
//            {
//                throw new Exception("Username is already in use");
//            }

//            var passwordSalt = _encryptionService.CreateSalt();

//            var user = new User()
//            {
//                Username = username,
//                Salt = passwordSalt,
//                Email = email,
//                IsLocked = false,
//                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
//                DateCreated = DateTime.Now
//            };

//            _userRepository.Add(user);

//            _unitOfWork.Commit();

//            if (roles != null || roles.Length > 0)
//            {
//                foreach (var role in roles)
//                {
//                    addUserToRole(user, role);
//                }
//            }

//            _unitOfWork.Commit();

//            return user;
//        }

//        public User GetUser(int userId)
//        {
//            return _userService..GetSingle(userId);
//        }

//        public List<Role> GetUserRoles(string username)
//        {
//            List<Role> _result = new List<Role>();

//            var existingUser = _userRepository.GetSingleByUsername(username);

//            if (existingUser != null)
//            {
//                foreach (var userRole in existingUser.UserRoles)
//                {
//                    _result.Add(userRole.Role);
//                }
//            }

//            return _result.Distinct().ToList();
//        }


//        #region Helper methods

//        private void addUserToRole(User user, int roleId)
//        {
//            var role = Database.Role.FindById(roleId);
//            if (role == null)
//                throw new ApplicationException("Role doesn't exist.");

//            //var userRole = new RolesForUser();
//            //{
//            //    RoleId = role.ID,
//            //    UserId = user.ID
//            //};
//            //_userRoleRepository.Add(userRole);
//        }

//        private bool isPasswordValid(User user, string password)
//        {
//            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
//        }

//        private bool isUserValid(User user, string password)
//        {
//            if (isPasswordValid(user, password))
//            {
//                return !user.IsLocked;
//            }

//            return false;
//        }

//        #endregion
//    }
//}