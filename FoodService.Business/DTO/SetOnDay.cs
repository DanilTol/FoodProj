namespace FoodService.Business.DTO
{
    public class SetOnDay
    {
        public long Date { get; set; }
        public int[] DishId { get; set; }
        public int[] DishNum { get; set; }
    }
}