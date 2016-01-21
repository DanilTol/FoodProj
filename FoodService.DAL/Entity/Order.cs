using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodService.DAL.Entity
{
    public class Order: BaseEntity
    {
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        public bool Checked { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderDish> OrderDishes { get; set; } 
    }
}
