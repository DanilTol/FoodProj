(function() {
    'use strict';
    angular.module('accountModule', ['common.core'])
        .config(function($routeProvider) {
            $routeProvider
               .when("/login", {
                    templateUrl: "scripts/spa/account/login.html",
                    controller: "loginCtrl"
                })
                .when("/register", {
                    templateUrl: "scripts/spa/account/register.html",
                    controller: "registerCtrl"
                }).otherwise({ redirectTo: "/" });
        });
})();