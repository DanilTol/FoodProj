(function (app) {
    "use strict";
    app.controller("orderListCtrl",
        ["$scope", "$location", "orderService", "notificationService", function ($scope, $location, orderService, notificationService) {

            $scope.dishes = [];
            $scope.dishes.orderList = [];
            $scope.dishes.deletedOrders = [];
            $scope.dateInput = new Date();
            $scope.dateInputMiliSec = $scope.dateInput.getTime();
            $scope.chefMail = "";//"example@mail.com";
            $scope.headerArray = ["Order id", "Date", "User", "Role", "Dishes"];

            function loadList() {
                $location.search("date", $scope.dateInputMiliSec);
                orderService.getOrderList($scope.dateInputMiliSec).then(
                    //success
                    function (data) {
                        $scope.dishes.orderList = [];
                        if (data != null) {
                            for (var order in data) {
                                var orderItem = {};
                                orderItem.Id = data[order].Id;
                                orderItem.Date = data[order].Date.substring(0, 10);
                                orderItem.User = data[order].User;
                                //                                orderItem.User.Role = "Good";
                                orderItem.dishes = [];
                                if (!angular.isUndefined(data[order].Dishes.length)) {
                                    for (var i = 0; i < data[order].Dishes.length; i++) {
                                        if (angular.isUndefined(orderItem.dishes[data[order].Dishes[i]])) {
                                            orderItem.dishes[data[order].Dishes[i].ID] = {};
                                        }
                                        orderItem.dishes[data[order].Dishes[i].ID].Number = 1 + (orderItem.dishes[data[order].Dishes[i].ID].Number || 0);
                                        if (orderItem.dishes[data[order].Dishes[i].ID].Number == 1) {
                                            orderItem.dishes[data[order].Dishes[i].ID].ID = data[order].Dishes[i].ID;
                                            orderItem.dishes[data[order].Dishes[i].ID].Name = data[order].Dishes[i].Name;
                                            orderItem.dishes[data[order].Dishes[i].ID].ImagePath = data[order].Dishes[i].ImagePath;
                                        }
                                    }
                                    $scope.dishes.orderList.push(orderItem);
                                }
                            }
                        }
                    });
            }

            $scope.deleteOrder = function (order) {
                $scope.dishes.deletedOrders.push(order.Id);
                $scope.dishes.orderList.splice(order, 1);
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
                var day = $scope.dateInput.getDay() + 6;
                $scope.dateInput.setDate($scope.dateInput.getDate() - day);
                $scope.dateInputMiliSec = $scope.dateInput.getTime();
                loadList();
            });

            $scope.sendToChef = function() {
                orderService.sendMailToChef($scope.chefMail).then(
                    function (data) {
                        notificationService.displaySuccess("Mail send.");
                    }, function (status) {
                        notificationService.displayError("Can`t send mail. Try again later.");
                    });
            }

            $scope.prepareArrayToCSV = function() {
                var csvArr = [];
                for (var order in $scope.orderList) {
                    var row = {};
                    row.id = order.id;
                    row.date = order.date;//9
                    row.user = order.User.Name;
                    row.role = order.User.Role;
                    row.dishes = "";
                    for (var dish in order.Dishes) {
                        row.dishes += dish.Name + " * " + dish.Number + " ";
                    }
                    csvArr.push(row);
                }
                return csvArr;
            }

        }]);
})(angular.module("orderModule"));

