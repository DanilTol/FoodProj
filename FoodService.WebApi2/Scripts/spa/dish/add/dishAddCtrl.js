(function (app) {
    "use strict";
    app.controller("dishAddCtrl", [
        "$scope", "$location", "dishService", "notificationService", function ($scope, $location, dishService, notificationService) {
            $scope.addDish = function () {
                dishService.addDish($scope.dish).then(
                    //success
                    function () {
                        notificationService.displaySuccess("Dish successfully added.");
                        $location.path("/dishes");
                    }, function () {
                        notificationService.displayError("Can`t add dish try again later.");
                    });;
            };
        }
    ]);
})(angular.module("dishModule"));