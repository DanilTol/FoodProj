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
                  .when("/reportdif", {
                      templateUrl: "scripts/spa/report/reportDif/reportDif.html",
                      controller: "reportDifCtrl",
                      reloadOnSearch: false
                  })
                 .otherwise({ redirectTo: "/" });
         });
})();