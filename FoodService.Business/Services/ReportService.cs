using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<ReportDTO> GetReports()
        {
            var reportsDb = Database.Report.QueryToTable;
            List<ReportDTO> reportDto = new List<ReportDTO>();

            //get monday of current week
            int diff = DateTime.Today.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }
            var today = DateTime.Today.AddDays(-1 * diff).Date;
            
            //check if report for current week was sent
            if (!reportsDb.Any(x => x.Date == today.Date))
            {
                //were not sent
                var friday = today.AddDays(6);
                var ordersDb = Database.Order.QueryToTable.Where(x => x.Date >= today.Date && x.Date <= friday.Date);
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


                var dto = new ReportDTO()
                {
                    Date = today,
                    ChefReport = chefReport,
                    State = -1
                };
                reportDto.Add(dto);
            }


            foreach (var report in reportsDb)
            {


                var dto = new ReportDTO()
                {
                    Date = report.Date,
                    Email = report.EmailAddress,
                    ChefReport = report.ChefReport,
                    Id =  report.id,
                 };

                bool sent = true;
                var friday = report.Date.AddDays(6);
                var ordersDb = Database.Order.QueryToTable.Where(x => x.Date >= report.Date && x.Date <= friday);
                foreach (var order in ordersDb)
                {
                    sent &= order.Checked;
                }
                dto.State = Convert.ToInt32(sent);


                if (!sent)
                {
                    //if was changed
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
                    dto.ChefReport = chefReport;
                }
                

                reportDto.Add(dto);
            }

            return reportDto;
        } 

        public string ReportsForMatch(DateTime dateTime)
        {
            return Database.Report.QueryToTable.FirstOrDefault(x => x.Date == dateTime.Date).ChefReport;
        }

        public void SentMailToChef(DateTime date, string chefMail)
        {
            var friday = date.AddDays(6);
            var ordersDb = Database.Order.QueryToTable.Where(x => x.Date >= date && x.Date <= friday);
            var dishOrder = new Dictionary<string, int>();

            foreach (var order in ordersDb)
            {
                if(order.OrderDishes.Count<1)
                    Database.Order.Delete(order);
                else
                {
                    order.Checked = true;
                    Database.Order.Update(order);
                }
            }

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