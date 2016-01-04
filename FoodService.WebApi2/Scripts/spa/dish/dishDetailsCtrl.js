(function (app) {
    'use strict';

    app.controller('dishDetailsCtrl', dishDetailsCtrl);

    dishDetailsCtrl.$inject = ['$scope', '$routeParams', 'dishService'];

    function dishDetailsCtrl($scope, $routeParams, dishService) {

         function loadDetails() {
             dishService.loadDishDetails($routeParams.id).then(
            //success
            function (data) {
                $scope.dish = data;
            });;
        }
        loadDetails();
    }
})(angular.module('dishModule'));
