using System.Collections.Generic;

namespace FoodService.DAL.Entity
{
    public class RolesForUser : CommonClass
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}