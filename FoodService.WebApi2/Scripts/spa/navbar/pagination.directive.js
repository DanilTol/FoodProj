(function (app) {
    'use strict';

    app.directive('pagination', pagination);

    function pagination() {
        return {
            scope: {
                page: '@',
                pagesCount: '@',
                totalCount: '@',
                pageRoute: '='
            },
            restrict: "E",
            replace: true,
            templateUrl: "/Scripts/spa/navbar/pagination.html",
            controller: ['$scope', function ($scope) {
                //$scope.search  = function(i) {
                //    if ($scope.pageRoute) {
                //        $scope.pageRoute({ page: i });
                //    }
                //}

                $scope.$watch(
                    function (scope) { return scope.page },
                    function () {
                        $scope.range = range();
                    });

                $scope.$watch(
                   function (scope) { return scope.pagesCount },
                   function () {
                       $scope.range = range();
                   });

                function range() {
                    if (!$scope.pagesCount) {
                        return [];
                    }
                    var step = 2;
                    var doubleStep = step * 2;
                    var start = Math.max(0, $scope.page - step);
                    var end = start + 1 + doubleStep;
                    if (end > $scope.pagesCount) {
                        end = $scope.pagesCount;
                    }

                    var ret = [];
                    for (var i = start; i != end; ++i) {
                        ret.push(i);
                    }

                    return ret;
                };
            }]

        }
    }

})(angular.module('common.ui'));