(function (app) {
    'use strict';

    app.controller('dishesCtrl', [
        '$scope', '$routeParams', '$location', 'dishService', function($scope, $routeParams, $location, dishService) {
            $scope.page = $location.search().page || 0;
            $scope.pagesCount = 0;
            $scope.pageSize = $location.search().pageSize || 5;
            $scope.filterDishes = '';

            $scope.search = function () {
                $scope.page = $location.search().page || 0;
                $scope.pageSize = $location.search().pageSize || 5;

                dishService.search($scope.page, $scope.pageSize, $scope.filterDishes)
                    .then(
                        //success
                        function(data) {
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

            $scope.pageRoute = function (page) {
                $location.search('page', page);
                $location.search('pageSize', $scope.pageSize);
                $scope.search();
            }

            $scope.search();
        }
    ]);
})(angular.module('dishModule'));