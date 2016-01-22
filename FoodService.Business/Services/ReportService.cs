using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.Business.Services
{
    public class ReportService : IReportService
    {
        IUnitOfWork Database { get; set; }

        public ReportService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public string[] ReportsForMatch(DateTime dateTime)
        {
            string oldReport = Database.Report.QueryToTable.FirstOrDefault(x => x.Date == dateTime.Date).ChefReport;

                 var friday = dateTime.AddDays(4);
            var ordersDb = Database.Order.QueryToTable.Where(x => x.Date >= dateTime.Date && x.Date <= friday);
            var dishOrder = new Dictionary<string, int>();

            foreach (var order in ordersDb)
            {
                foreach (var orderDish in order.OrderDishes)
                {
                    if (dishOrder.ContainsKey(orderDish.Dish.Name))
                        dishOrder[orderDish.Dish.Name] += orderDish.Count;
                    else
                        dishOrder.Add(orderDish.Dish.Name, orderDish.Count);
                }
            }

            string chefReport = "";
            foreach (var dish in dishOrder)
            {
                chefReport += dish.Key + " * " + dish.Value + "; ";
            }

            return new[] {chefReport,oldReport};
        }

        public void SentMailToChef(DateTime date, string chefMail)
        {
            var friday = date.AddDays(4);
            var ordersDb = Database.Order.QueryToTable.Where(x => x.Date >= date && x.Date <= friday);
            var dishOrder = new Dictionary<string, int>();

            foreach (var order in ordersDb)
            {
                foreach (var orderDish in order.OrderDishes)
                {
                    if (dishOrder.ContainsKey(orderDish.Dish.Name))
                        dishOrder[orderDish.Dish.Name] += orderDish.Count;
                    else
                        dishOrder.Add(orderDish.Dish.Name, orderDish.Count);
                }
            }

            string messageBody = "<table style='border: 1px solid black'><tr><th>Dish name</th><th>Amount</th></tr>";
            string chefReport = "";
            foreach (var dish in dishOrder)
            {
                messageBody += "<tr><td>" + dish.Key + "</td><td>" + dish.Value + "</td></tr>";
                chefReport += dish.Key + " * " + dish.Value + "; ";
            }

            var oldReport = Database.Report.QueryToTable.FirstOrDefault(x => x.Date == date.Date && x.EmailAddress == chefMail);
            if (oldReport != null)
            {
                oldReport.ChefReport = chefReport;
                Database.Report.Update(oldReport);
            }
            else
            {
                Database.Report.Add(new Report() { ChefReport = chefReport, Date = date.Date, EmailAddress = chefMail });
            }

            Database.Save();

            MailService.SentEmail(chefMail, "Orders on " + date.Date, messageBody);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}