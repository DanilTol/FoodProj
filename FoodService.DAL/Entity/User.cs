using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodService.DAL.Entity
{
    public class User : CommonClass
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Salt { get; set; }
        public bool IsLocked { get; set; }

        public virtual RolesForUser Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; } 
    }
}