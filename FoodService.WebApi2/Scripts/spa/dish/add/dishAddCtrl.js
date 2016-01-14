(function (app) {
    "use strict";
    app.controller("dishAddCtrl", [
        "$scope", "$location", "dishService", function ($scope, $location, dishService) {
            $scope.addDish = function () {
                dishService.addDish($scope.dish).then(
                    //success
                    function (data) {
                        $scope.dish = data;
                        $location.path("/dishes");
                    });;
            };
        }
    ]);
})(angular.module("dishModule"));