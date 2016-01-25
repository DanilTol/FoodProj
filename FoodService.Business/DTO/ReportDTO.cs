using System;

namespace FoodService.Business.DTO
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public string ChefReport { get; set; }
        public int State { get; set; }
    }
}