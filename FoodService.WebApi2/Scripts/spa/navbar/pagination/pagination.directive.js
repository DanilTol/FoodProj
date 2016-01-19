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
            link: function ($scope) {
                    function range() {
                        if (!$scope.pagesCount) {
                            return [];
                        }
                        var step = 2;
                        var doubleStep = step * 2;
                        var start = Math.max(0, parseInt($scope.page) - step);
                        var end = start + 1 + doubleStep;
                        if (end > parseInt($scope.pagesCount)) {
                            end = parseInt($scope.pagesCount);
                        }
                        var ret = [];
                        for (var i = start; i != end; ++i) {
                            ret.push(i);
                        }
                        return ret;
                    }

                    $scope.$watch(
                        function () { return $scope.page },
                        function (v,o) {
                            $scope.range = range();
                        });

                    $scope.$watch(
                        function() {
                            return $scope.pagesCount;
                        },
                        function (v,o) {
                            debugger;
                            $scope.range = range();
                        });;
                }
        }
    });
})(angular.module("navbarModule"));