(function (app) {
    'use strict';

    app.directive('topBar', [
         'accountService', function ( accountService) {
            return {
                restrict: 'E',
                replace: true,
                templateUrl: '/Scripts/spa/navbar/topBar.html',
                link: function (scope) {
                    scope.getUserName = accountService.getUserData;
                    scope.isUserLoggedIn = accountService.isUserLoggedIn;
                }
            }
        }]
            );

})(angular.module('common.ui'));