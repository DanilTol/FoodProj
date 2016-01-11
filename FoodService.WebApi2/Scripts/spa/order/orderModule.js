﻿(function () {
    "use strict";
    angular.module("orderModule", ["common.core"])
         .config(function ($routeProvider) {
             $routeProvider
                  .when("/order", {
                      templateUrl: "scripts/spa/order/order.html",
                      controller: "orderCtrl",
                      reloadOnSearch: false
                  })
                 .otherwise({ redirectTo: "/" });
         });
})();