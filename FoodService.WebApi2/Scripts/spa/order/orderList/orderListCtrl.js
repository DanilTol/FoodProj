(function (app) {
    "use strict";
    app.controller("orderListCtrl",
        ["$scope", "$location", "orderService", "notificationService", "reportService", function ($scope, $location, orderService, notificationService, reportService) {

            $scope.dishes = [];
            $scope.dishes.orderList = [];
            $scope.dishes.deletedOrders = [];
            $scope.dateInput = getMonday(new Date());
            $scope.chefMail = "";
            $scope.headerArray = ["Order id", "User", "Dishes","Date", "Sent"];

            function getMonday(date) {
                var d = new Date(date);
                var day = d.getDay(),
                    diff = d.getDate() - day + (day == 0 ? -6 : 1); // adjust when day is sunday
                return  new Date(d.setDate(diff));
            }

            function loadList() {
                var dateInputMiliSec = getMonday($scope.dateInput).getTime();
                $location.search("date", dateInputMiliSec);

                orderService.getOrderList(dateInputMiliSec).then(
                    //success
                    function (data) {
                        $scope.dishes.orderList = data;
                        for (var i = 0; i < $scope.dishes.orderList.length; i++) {
                            $scope.dishes.orderList[i].Date = $scope.dishes.orderList[i].Date.substring(0, 10);
                        }
                    });
            }

            $scope.deleteOrder = function (order) {
                $scope.dishes.deletedOrders.push(order.Id);

                for (var index in $scope.dishes.orderList) {
                    if ($scope.dishes.orderList[index].Id == order.Id) {
                        $scope.dishes.orderList[index].Dishes = "";
                        $scope.dishes.orderList[index].Checked = false;
                    }
                }
                
                //$scope.dishes.orderList.splice(order, 1);
            }

            $scope.saveChanges = function () {
               
                orderService.deleteOrders($scope.dishes.deletedOrders).then(
                    function(data) {
                        notificationService.displaySuccess("Changes has been saved.");
                    }, function(status) {
                        notificationService.displayError("Can`t save changes. Try again later.");
                    });
            }

            $scope.$watch("dateInput", function () {
                loadList();
            });

            $scope.sendToChef = function() {
                reportService.sendMailToChef(getMonday($scope.dateInput).getTime(), $scope.chefMail).then(
                    function (data) {
                        notificationService.displaySuccess("Mail send.");
                    }, function (status) {
                        notificationService.displayError("Can`t send mail. Try again later.");
                    });
            }
        }]);
})(angular.module("orderModule"));

