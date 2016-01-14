(function (app) {
    "use strict";

    app.controller("loginCtrl", [
        "$scope", "accountService", "$rootScope", "$location", "notificationService", function ($scope, accountService, $rootScope, $location, notificationService) {
            $scope.user = {};
            $scope.login = function () {
                accountService.login($scope.user)
                    .then(
                        //success
                        function () {
                            notificationService.displaySuccess("Welcome");
                            if ($rootScope.previousState)
                                $location.path($rootScope.previousState);
                            else
                                $location.path("/");
                        }, function () {
                            notificationService.displayError("Login failed. Try again.");
                        });;
            }
        }
    ]);
})(angular.module("accountModule"));