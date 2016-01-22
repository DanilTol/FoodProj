using System;

namespace FoodService.Business.ServiceInterfaces
{
    public interface IReportService
    {
        string[] ReportsForMatch(DateTime dateTime);
        void SentMailToChef(DateTime date, string chefMail);
        void Dispose();

    }
}