using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodService.DAL.Entity
{
    public class Report : BaseEntity
    {
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        public string ChefReport { get; set; }
    }
}