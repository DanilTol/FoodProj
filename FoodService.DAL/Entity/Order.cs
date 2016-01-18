﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodService.DAL.Entity
{
    public class Order: BaseEntity
    {
       
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; } 
    }
}
