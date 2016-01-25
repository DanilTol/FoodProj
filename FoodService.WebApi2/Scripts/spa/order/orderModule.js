(function () {
    "use strict";
    angular.module("orderModule", ["common.core"])
         .config(function ($routeProvider) {
             $routeProvider
                  .when("/order", {
                      templateUrl: "scripts/spa/order/main/order.html",
                      controller: "orderCtrl",
                      reloadOnSearch: false
                  })
                  .when("/orderlist", {
                      templateUrl: "scripts/spa/order/orderList/orderList.html",
                      controller: "orderListCtrl"
                      ,reloadOnSearch: false
                  })
                 .otherwise({ redirectTo: "/" });
         });
})();