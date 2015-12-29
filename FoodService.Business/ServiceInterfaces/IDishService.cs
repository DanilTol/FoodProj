using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FoodService.Business.DTO;
using FoodService.DAL.Entity;

namespace FoodService.Business.ServiceInterfaces
{
    public interface IDishService
    {
        IEnumerable<DishModelShortInfo> GetAllDishes();
        IEnumerable<DishModelShortInfo> FilterDishes(int page, int pageSize, string filter = null);
        int TotalFilteredDish(string filter = null);
        void CreateDish(DishModelDetailsInfo dish);
        DishModelDetailsInfo GetDishById(int id);
        void EditDish(DishModelDetailsInfo dish);
        void DeleteDish(int id);
        void Dispose();
    }
}