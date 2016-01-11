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

            bundles.Add(new ScriptBundle("~/bundles/Vendors/bootstrap").Include(
                "~/Scripts/Vendors/jquery.js",
                "~/Scripts/Vendors/bootstrap.js",
                      "~/Scripts/Vendors/respond.js"

                      ));

            bundles.Add(new ScriptBundle("~/bundles/Vendors").Include(
              "~/Scripts/Vendors/jquery.js",
              "~/Scripts/Vendors/jquery-1.10.2.min.js",
              "~/Scripts/Vendors/bootstrap.js",
              "~/Scripts/Vendors/respond.src.js",
              "~/Scripts/Vendors/angular.js",
              "~/Scripts/Vendors/angular-route.js",
              "~/Scripts/Vendors/angular-cookies.js",
              "~/Scripts/Vendors/angular-validator.js"

              ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                "~/Scripts/Vendors/jquery-1.10.2.min.js",
                //main
                "~/Scripts/spa/app.js",

                //module
                "~/Scripts/spa/dish/dishModule.js",
                "~/Scripts/spa/account/accountModule.js",
                "~/Scripts/spa/services/common.core.js",
                "~/Scripts/spa/navbar/common.ui.js",
                "~/Scripts/spa/dishset/dishsetModule.js",
                "~/Scripts/spa/order/orderModule.js",

               //Services(common)
               "~/Scripts/spa/services/sessionInjector.js",
               "~/Scripts/spa/services/fileUploadService.js",
               
                //home
                "~/Scripts/spa/home/rootCtrl.js",
                "~/Scripts/spa/home/indexCtrl.js",

                //account
                "~/Scripts/spa/account/accountService.js",
                "~/Scripts/spa/account/loginCtrl.js",
                "~/Scripts/spa/account/registerCtrl.js",

                //dish
                "~/Scripts/spa/dish/dishService.js",
                "~/Scripts/spa/dish/dishDetailsCtrl.js",
                "~/Scripts/spa/dish/dishAddCtrl.js",
                "~/Scripts/spa/dish/dishEditCtrl.js",
                "~/Scripts/spa/dish/dishesCtrl.js",

                //dishset
                "~/Scripts/spa/dishset/dishsetService.js",
                "~/Scripts/spa/dishset/dishsetCtrl.js",
                "~/Scripts/spa/dishset/dndDishset.js",
                "~/Scripts/spa/dishset/editVMonDnD.directive.js",
                "~/Scripts/spa/dishset/FilterForChosenDishes.js",
                "~/Scripts/spa/dishset/DnD.directive.js",

                //order
                "~/Scripts/spa/order/orderService.js",
                "~/Scripts/spa/order/orderCtrl.js",

                //Nav bars
                "~/Scripts/spa/navbar/sideBar.directive.js",
                "~/Scripts/spa/navbar/topBar.directive.js",
                "~/Scripts/spa/navbar/bottomBar.directive.js",
                "~/Scripts/spa/navbar/pagination.directive.js",
                "~/Scripts/spa/account/userName.directive.js"

                
        ));
            

        }
    }
}
