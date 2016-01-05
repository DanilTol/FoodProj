﻿(function() {
    'use strict';
    angular.module('dishsetModule', [ 'common.core'])
         .config(function($routeProvider) {
                $routeProvider
                     .when("/dishset", {
                         templateUrl: "scripts/spa/dishset/dishset.html",
                         controller: "dishsetCtrl"
                     }).otherwise({ redirectTo: "/" });
        });
})();