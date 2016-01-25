namespace FoodService.Business.DTO
{
    public class UserEdit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string EmailAddress { get; set; }
        public string NewSalt { get; set; }
        public string Salt { get; set; }
    }
}