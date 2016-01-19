(function (app) {
    "use strict";
    app.controller("dishDetailsCtrl", [
        "$scope", "$routeParams", "$location", "dishService", function($scope, $routeParams, $location, dishService) {
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

            loadDetails();
        }]);
})(angular.module("dishModule"));
