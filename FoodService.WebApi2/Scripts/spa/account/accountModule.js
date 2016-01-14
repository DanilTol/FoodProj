(function() {
    "use strict";
    angular.module("accountModule", ["common.core"])
        .config(function($routeProvider) {
            $routeProvider
               .when("/login", {
                    templateUrl: "scripts/spa/account/login/login.html",
                    controller: "loginCtrl"
                })
                .when("/register", {
                    templateUrl: "scripts/spa/account/register/register.html",
                    controller: "registerCtrl"
                }).otherwise({ redirectTo: "/" });
        });
})();