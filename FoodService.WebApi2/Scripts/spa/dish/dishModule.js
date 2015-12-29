(function() {
    'use strict';
    angular.module('dishModule', ['common.core'])
         .config(function($routeProvider) {
                $routeProvider
                    .when("/dishes", {
                        templateUrl: "scripts/spa/dish/dishes.html",
                        controller: "dishesCtrl"
                    })
                    .when("/dishes/add", {
                        templateUrl: "scripts/spa/dish/add.html",
                        controller: "dishAddCtrl"
                    })
                    .when("/dishes/:id", {
                        templateUrl: "scripts/spa/dish/details.html",
                        controller: "dishDetailsCtrl"
                    })
                    .when("/dishes/edit/:id", {
                        templateUrl: "scripts/spa/dish/edit.html",
                        controller: "dishEditCtrl"
                    }).otherwise({ redirectTo: "/" });
        });
})();