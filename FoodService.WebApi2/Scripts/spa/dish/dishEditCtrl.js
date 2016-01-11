(function (app) {
    'use strict';

    app.controller('dishEditCtrl', [
        '$scope', '$routeParams', 'dishService', function ($scope, $routeParams, dishService) {

            $scope.UpdateDish = function () {
                dishService.updateDish($scope.dish).then(
                    //success
                    function (data) {
                        $scope.dish = data;
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
                    });;
            }


            loadDetails();
        }]);
})(angular.module('dishModule'));