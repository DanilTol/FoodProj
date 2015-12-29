using System.Collections.Generic;
using FoodService.DAL.Entity;

namespace FoodService.Business.Membership.Interface
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, int[] roles);
        User GetUser(int userId);
        List<RolesForUser> GetUserRoles(string username);
    }
}