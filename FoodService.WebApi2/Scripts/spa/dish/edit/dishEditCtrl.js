(function (app) {
    "use strict";

    app.controller("dishEditCtrl", [
        "$scope", "$routeParams", "dishService", "notificationService", function ($scope, $routeParams, dishService, notificationService) {

            $scope.UpdateDish = function () {
                dishService.updateDish($scope.dish).then(
                    //success
                    function (data) {
                        notificationService.displaySuccess("Dish edited");
                        $scope.dish = data;
                    }, function() {
                        notificationService.displayError("Can`t edit dish. Try again later.");
                    });;
            }

            function loadDetails() {
                dishService.loadDishDetails($routeParams.id).then(
                    //success
                    function (data) {
                        $scope.dish = data;
                    });;
            }

            $scope.deleteDish = function() {
                dishService.deleteDish($scope.dish.ID).then(
                    //success
                    function (data) {
                        notificationService.displaySuccess("Dish has been deleted.");
                    },function() {
                        notificationService.displayError("Can`t delete this dish.");
                    }
                    );;
            }


            loadDetails();
        }]);
})(angular.module("dishModule"));