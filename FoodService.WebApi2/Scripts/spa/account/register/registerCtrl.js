(function (app) {
    "use strict";
    app.controller("registerCtrl", [
        "$scope", "accountService", "$location", function($scope, accountService, $location) {
            $scope.user = {};

            $scope.register = function() {
                accountService.register($scope.user).then(
                    //success
                    function() {
                        $location.path("/");
                        $scope.userData.LogInUser = $scope.user;
                    });

            }
        }]);
})(angular.module("accountModule"));