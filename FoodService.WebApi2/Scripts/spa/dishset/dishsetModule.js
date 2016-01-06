(function () {
    'use strict';
    angular.module('dishsetModule', ['common.core'])
         .config(function ($routeProvider) {
             $routeProvider
                  .when("/dishset", {
                      templateUrl: "scripts/spa/dishset/dishset.html",
                      controller: "dishsetCtrl",
                      reloadOnSearch: false
                  })
                  //.when("/dishset/edit/:date", {
                      .when("/dishset/edit", {
                      templateUrl: "scripts/spa/dishset/dishsetEdit.html",
                      controller: "dishsetEditCtrl"
                  })
                 .otherwise({ redirectTo: "/" });
         });
})();