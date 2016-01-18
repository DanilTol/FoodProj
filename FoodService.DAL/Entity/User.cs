using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodService.DAL.Entity
{
    public class User : CommonClass
    {
       
        public string Name { get; set; }
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string EmailAddress { get; set; }
        public string Salt { get; set; }
        public bool IsLocked { get; set; }

        public virtual RolesForUser Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; } 
    }
}