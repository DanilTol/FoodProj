using System;
using System.Collections.Generic;
using FoodService.Business.DTO;
using FoodService.DAL.Entity;

namespace FoodService.Business.ServiceInterfaces
{
    public interface IOrderService
    {
        IEnumerable<DishModelShortInfo> GetPlatesByDate(DateTime date, User user);
        void UpdateOrder(DateTime date, int[] arraInts,int[] dishNum, User user);
        IEnumerable<OrderInfo> GetOrderListOnWeek(DateTime date);
        void DeleteRangeOrders(int[] orderIds);
        //void SentMailToChef(DateTime date, string chefMail);
        int NumberOfUnchecked();
        IEnumerable<OrderInfo> NotCheckedOrders();
        void Dispose();
    }
}