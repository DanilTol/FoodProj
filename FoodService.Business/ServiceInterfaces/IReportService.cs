using System;
using System.Collections.Generic;
using FoodService.Business.DTO;

namespace FoodService.Business.ServiceInterfaces
{
    public interface IReportService
    {
        List<ReportDTO> GetReports();
        string ReportsForMatch(DateTime dateTime);
        void SentMailToChef(DateTime date, string chefMail);
        void Dispose();

    }
}