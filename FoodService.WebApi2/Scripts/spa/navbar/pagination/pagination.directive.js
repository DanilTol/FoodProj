(function (app) {
    "use strict";

    app.directive("pagination", function () {
        return {
            scope: {
                page: "@",
                pagesCount: "@",
                totalCount: "@",
                pageRoute: "="
            },
            restrict: "E",
            replace: true,
            templateUrl: "/Scripts/spa/navbar/pagination/pagination.html",
            controller: [
                "$scope", function ($scope) {
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
                        for (var i = start; i !== end; ++i) {
                            ret.push(i);
                        }
                        return ret;
                    }

                    $scope.$watch(
                        function (scope) { return scope.page },
                        function () {
                            $scope.range = range();
                        });

                    $scope.$watch(
                        function (scope) { return scope.pagesCount },
                        function () {
                            $scope.range = range();
                        });;
                }
            ]
        }
    });
})(angular.module("navbarModule"));