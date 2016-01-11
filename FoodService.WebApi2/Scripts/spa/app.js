(function() {
    'use strict';

    angular.module('homeFoodService', ['common.core', 'common.ui', 'dishModule', 'accountModule', 'dishsetModule', 'orderModule'])
        .config(function($routeProvider) {
                $routeProvider
                    .when("/", {
                        templateUrl: "scripts/spa/home/index.html",
                        controller: "indexCtrl"
                    })
                    .when("/customers", {
                        templateUrl: "scripts/spa/customers/customers.html",
                        controller: "customersCtrl"
                    })
                    .otherwise({ redirectTo: "/" });
            }
        );
})();