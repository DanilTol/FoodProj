(function (app) {
    "use strict";
    app.controller("dishesCtrl", ["$scope", "$location", "dishService", "notificationService", "accountService", function ($scope, $location, dishService, notificationService, accountService) {
        $scope.filterDishes = "";

        $scope.search = function () {
            $scope.page = $location.search().page || 0;
            $scope.pageSize = $location.search().pageSize || 6;

            dishService.search($scope.page, $scope.pageSize, $scope.filterDishes)
                .then(
                    //success
                    function (data) {
                        $scope.Dishes = data.Items;
                        $scope.page = data.Page;
                        $scope.pagesCount = data.TotalPages;
                        $scope.totalCount = data.TotalCount;
                    }, function () {
                        notificationService.displayError("Can`t open dishes.");
                    });
        }

        $scope.clearSearch = function () {
            $scope.filterDishes = "";
            $scope.search();
        }

        $scope.pageRoute = function (page) {
            $location.search("page", page);
            $location.search("pageSize", $scope.pageSize);
            $scope.search();
        }

        $scope.detailsRedirect = function (path) {
            $location.path(path);
        }

        $scope.search();
        $scope.userProfile = accountService.getUserData();
    }
    ]);
})(angular.module("dishModule"));