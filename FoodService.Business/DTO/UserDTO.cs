namespace FoodService.Business.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
    }
}