(function (app) {
    'use strict';

    app.controller('dishesCtrl', dishesCtrl);

    dishesCtrl.$inject = ['$scope', 'dishService'];

    function dishesCtrl($scope, dishService ) {
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pageSize = 5;
        $scope.filterDishes = '';

        $scope.search = function () {
            dishService.search($scope.page, $scope.pageSize, $scope.filterDishes)
            .then(
            //success
            function (data) {
                $scope.Dishes = data;
                $scope.page = data.Page;
                $scope.pagesCount = data.TotalPages;
                $scope.totalCount = data.TotalCount;
            });;
        }
        
        $scope.clearSearch = function clearSearch() {
            $scope.filterDishes = '';
            $scope.search();
        }

        $scope.search();
    }
})(angular.module('dishModule'));