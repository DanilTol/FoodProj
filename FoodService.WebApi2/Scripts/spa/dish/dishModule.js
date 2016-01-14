(function() {
    "use strict";
    angular.module("dishModule", ["common.core"])
         .config(function($routeProvider) {
                $routeProvider
                    .when("/dishes", {
                        templateUrl: "scripts/spa/dish/main/dishes.html",
                        controller: "dishesCtrl",
                        reloadOnSearch: false
                    })
                    .when("/dishes/add", {
                        templateUrl: "scripts/spa/dish/add/add.html",
                        controller: "dishAddCtrl"
                    })
                    .when("/dishes/:id", {
                        templateUrl: "scripts/spa/dish/details/details.html",
                        controller: "dishDetailsCtrl"
                    })
                    .when("/dishes/edit/:id", {
                        templateUrl: "scripts/spa/dish/edit/edit.html",
                        controller: "dishEditCtrl"
                    }).otherwise({ redirectTo: "/" });
        });
})();