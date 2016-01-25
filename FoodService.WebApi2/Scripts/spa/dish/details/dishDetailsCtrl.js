(function (app) {
    "use strict";
    app.controller("dishDetailsCtrl", [
        "$scope", "$routeParams", "$location", "dishService", "accountService", function ($scope, $routeParams, $location, dishService, accountService) {
            function loadDetails() {
                dishService.loadDishDetails($routeParams.id).then(
                    //success
                    function(data) {
                        $scope.dish = data;
                    });
                dishService.search(0, 3, "-random").then(
                    function(data) {
                        $scope.seeAlso = data.Items;
                    }
                );
            }

            $scope.detailsRedirect = function (path) {
                $location.path(path);
            }

            $scope.userProfile = {};

            accountService.getUserAsync().
                then(function (data) {
                    $scope.userProfile = data;
                    $scope.userProfile.Salt = "";
                }, function () {

                });

            loadDetails();
        }]);
})(angular.module("dishModule"));
