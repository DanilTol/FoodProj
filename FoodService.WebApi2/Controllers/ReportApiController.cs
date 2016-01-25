using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodService.Business.ServiceInterfaces;
using FoodService.WebApi2.Attribute;

namespace FoodService.WebApi2.Controllers
{
    [MyAuth("admin")]
    [RoutePrefix("api/report")]
    public class ReportApiController : ApiController
    {
        private readonly IReportService _reportService;
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1);

        public ReportApiController(IReportService report)
        {
            _reportService = report;
        }

        [HttpGet]
        [Route("getallreports")]
        public HttpResponseMessage GetReports()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _reportService.GetReports());
        }

        [HttpGet]
        [Route("match")]
        public HttpResponseMessage GetReportForMatching(long miliSecFrom1970)
        {
            return Request.CreateResponse(HttpStatusCode.OK,_reportService.ReportsForMatch(Jan1St1970.AddMilliseconds(miliSecFrom1970)));
        }

        [HttpGet]
        [Route("sentmail")]
        public HttpResponseMessage SentOrderToChef(long miliSecFrom1970, string chefMail)
        {
            _reportService.SentMailToChef(Jan1St1970.AddMilliseconds(miliSecFrom1970).Date, chefMail);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
