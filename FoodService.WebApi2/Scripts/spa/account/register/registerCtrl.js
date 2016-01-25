(function (app) {
    "use strict";
    app.controller("registerCtrl", [
        "$scope", "accountService", "$location", "notificationService", function ($scope, accountService, $location, notificationService) {
            $scope.user = {};

            $scope.register = function () {
                accountService.register($scope.user).then(
                    //success
                    function () {
                        $location.path("/");
                        notificationService.displaySuccess("Welcome " + $scope.user.Name);
                        
                    }, function () {
                        notificationService.displayError("Can`t registrate user.");
                    });

            }
        }]);
})(angular.module("accountModule"));