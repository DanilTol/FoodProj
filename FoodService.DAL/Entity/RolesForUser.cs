using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodService.DAL.Entity
{
    public class RolesForUser : CommonClass
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}