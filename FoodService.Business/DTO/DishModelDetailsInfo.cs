namespace FoodService.Business.DTO
{
    public class DishModelDetailsInfo
    {
      //public DishModelDetailsInfo(int id, string name, string ingridients, int energy, int weight, int price, string description, string[] image)
      //  {
      //      ID = id;
      //      Name = name;
      //      Ingridients = ingridients;
      //      Energy = energy;
      //      Weight = weight;
      //      Price = price;
      //      Description = description;
      //      ImagePath = image;
      //  }

      //  public DishModelDetailsInfo()
      //  { }

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
