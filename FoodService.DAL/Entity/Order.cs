using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodService.DAL.Entity
{
    public class Order:CommonClass
    {
        [Key]
        public int ID { get; set; }
        //public int UserId { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        //public virtual ICollection<Dish> OrderDishesDishes { get; set; } 
        public virtual ICollection<UserSet> UserSetes { get; set; }
    }
}
