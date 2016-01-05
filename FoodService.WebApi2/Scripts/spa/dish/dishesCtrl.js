(function (app) {
    'use strict';

    app.controller('dishesCtrl', [
        '$scope', '$routeParams', '$location', 'dishService', function($scope, $routeParams, $location, dishService) {
            $scope.page = 0;
            $scope.pagesCount = 0;
            $scope.pageSize = 5;
            $scope.filterDishes = '';

            $scope.search = function () {
                //$scope.page = page || 0;
                $scope.page = $routeParams.page || 0;
                $scope.pageSize = $routeParams.pageSize || 5;

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

            $scope.range = function () {
                if (!$scope.pagesCount) { return []; }
                var step = 2;
                var doubleStep = step * 2;
                var start = Math.max(0, $scope.page - step);
                var end = start + 1 + doubleStep;
                if (end > $scope.pagesCount) { end = $scope.pagesCount; }

                var ret = [];
                for (var i = start; i != end; ++i) {
                    ret.push(i);
                }

                return ret;
            };

            $scope.pageRoute = function (page) {
                $location.path('/dishes/'+ page + "/" + $scope.pageSize);
            }







            $scope.search();
        }
    ]);
})(angular.module('dishModule'));