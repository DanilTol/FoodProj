using System.Security.Principal;
using FoodService.DAL.Entity;

namespace FoodService.Business.Membership
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public User User { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}