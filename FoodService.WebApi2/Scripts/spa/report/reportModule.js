(function () {
    "use strict";
    angular.module("reportModule", ["common.core"])
         .config(function ($routeProvider) {
             $routeProvider
                  .when("/report", {
                      templateUrl: "scripts/spa/report/main/report.html",
                      controller: "reportCtrl",
                      reloadOnSearch: false
                  })
                 .otherwise({ redirectTo: "/" });
         });
})();