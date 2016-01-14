(function () {
    "use strict";
    angular.module("dishsetModule", ["common.core"])
         .config(function ($routeProvider) {
             $routeProvider
                  .when("/dishset", {
                      templateUrl: "scripts/spa/dishset/main/dishset.html",
                      controller: "dishsetCtrl",
                      reloadOnSearch: false
                  })
                 .otherwise({ redirectTo: "/" });
         });
})();