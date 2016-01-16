namespace FoodService.Business.DTO
{
    public class DishModelDetailsInfo
    {
        public int ID { get; set; }
        public string Name { get;set; }
        public string Ingridients { get; set; }
        public int Energy { get; set; }

        public int Weight { get; set; }
        public int Price { get;  set; }
        public string[] ImagePath { get; set; }
        public string Description { get; set; }
    }
}
