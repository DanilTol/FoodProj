(function (app) {
    'use strict';
    app.controller('dishsetCtrl', [
        '$scope', '$routeParams', 'dishsetService', function($scope, $routeParams, dishsetService) {
            function loadDishset() {
                dishsetService.loadDishset($routeParams.date).then(
                    //success
                    function(data) {
                        $scope.dishes = data;
                    });
            }
            loadDishset();
        }]);
})(angular.module('dishsetModule'));
