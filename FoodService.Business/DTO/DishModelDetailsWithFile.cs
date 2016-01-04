using System.Web;

namespace FoodService.Business.DTO
{
    public class DishModelDetailsWithFile
    {
        public int ID { get; set; }
        public string Name { get;set; }
        public string Ingridients { get; set; }
        public int Energy { get; set; }

        public int Weight { get; set; }
        public int Price { get;  set; }
        HttpPostedFileBase Attachment { get; set; }
        public string Description { get; set; }
    }
}
