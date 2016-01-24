using System.Web;
using System.Web.Optimization;

namespace FoodService.WebApi2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Vendors/jquery").Include(
                        "~/Scripts/Vendors/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/Vendors/modernizr").Include(
                        "~/Scripts/Vendors/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/Vendors/bootstrap").Include(
            //   // "~/Scripts/Vendors/jquery.js",
            //    "~/Scripts/Vendors/bootstrap.js",
            //          "~/Scripts/Vendors/respond.js"

            //          ));

            bundles.Add(new ScriptBundle("~/bundles/Vendors").Include(
              //"~/Scripts/Vendors/jquery.js",
              "~/Scripts/Vendors/jquery-1.10.2.min.js",
              "~/Scripts/Vendors/bootstrap.min.js",
              //"~/Scripts/Vendors/respond.src.js",
              "~/Scripts/Vendors/diff_match_patch.js",
              "~/Scripts/Vendors/angular.min.js",
              "~/Scripts/Vendors/angular-route.min.js",
              "~/Scripts/Vendors/angular-cookies.min.js",
              //"~/Scripts/Vendors/angular-validator.js",
              "~/Scripts/Vendors/toastr.min.js",
              "~/Scripts/Vendors/ng-csv.min.js",
              "~/Scripts/Vendors/angular-sanitize.min.js"
              

              ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                //"~/Scripts/Vendors/jquery-1.10.2.min.js",
                //main
                "~/Scripts/spa/app.js",


                //module
                "~/Scripts/spa/navbar/navbarModule.js",
                "~/Scripts/spa/home/homeModule.js",
                "~/Scripts/spa/dish/dishModule.js",
                "~/Scripts/spa/account/accountModule.js",
                "~/Scripts/spa/services/common.core.js",
                
                "~/Scripts/spa/dishset/dishsetModule.js",
                "~/Scripts/spa/order/orderModule.js",
                "~/Scripts/spa/report/reportModule.js",


               //Services(common)
               "~/Scripts/spa/services/sessionInjector.js",
               "~/Scripts/spa/services/fileUploadService.js",
               "~/Scripts/spa/services/notificationService.js",
               "~/Scripts/spa/services/DnD.directive.js",
               "~/Scripts/spa/services/FileRead.directive.js",

               

                //home
                "~/Scripts/spa/home/index/indexCtrl.js",

                //account
                "~/Scripts/spa/account/accountService.js",
                "~/Scripts/spa/account/login/loginCtrl.js",
                "~/Scripts/spa/account/register/registerCtrl.js",
                "~/Scripts/spa/account/profile/profileCtrl.js",

                //dish
                "~/Scripts/spa/dish/dishService.js",
                "~/Scripts/spa/dish/details/dishDetailsCtrl.js",
                "~/Scripts/spa/dish/add/dishAddCtrl.js",
                "~/Scripts/spa/dish/edit/dishEditCtrl.js",
                "~/Scripts/spa/dish/main/dishesCtrl.js",

                //dishset
                "~/Scripts/spa/dishset/dishsetService.js",
                "~/Scripts/spa/dishset/main/dishsetCtrl.js",
                "~/Scripts/spa/dishset/main/FilterForChosenDishes.js",
                

                //order
                "~/Scripts/spa/order/orderService.js",
                "~/Scripts/spa/order/main/orderCtrl.js",
                "~/Scripts/spa/order/orderList/orderListCtrl.js",

                //report
                "~/Scripts/spa/report/reportService.js",
                "~/Scripts/spa/report/main/reportCtrl.js",
                "~/Scripts/spa/report/reportDif/reportDifCtrl.js",

                //Nav bars
                "~/Scripts/spa/navbar/side/sideBar.directive.js",
                "~/Scripts/spa/navbar/top/topBar.directive.js",
                "~/Scripts/spa/navbar/bottom/bottomBar.directive.js",
                "~/Scripts/spa/navbar/pagination/pagination.directive.js"
                
        ));
            

        }
    }
}
