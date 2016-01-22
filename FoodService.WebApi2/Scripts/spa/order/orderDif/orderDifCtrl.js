(function (app) {
    "use strict";
    app.controller("orderDifCtrl",
        ["$scope", "orderService", "notificationService", function ($scope, orderService, notificationService) {

            $scope.unCheckedOrders = {};

            $scope.loadUnChecked = function() {
                orderService.notCheckedOrder().then(function(data) {
                    $scope.unCheckedOrders = data;
                }, function(data) {
                    notificationService.displayError("Can`t show unchecked orders/");
                });
            }

            $scope.loadUnChecked();
        }]);
})(angular.module("orderModule"));

