using System.ComponentModel.DataAnnotations;

namespace FoodService.DAL.Entity
{
    public class BaseEntity
    {
        [Key]
        public int id { get; set; }
    }
}
