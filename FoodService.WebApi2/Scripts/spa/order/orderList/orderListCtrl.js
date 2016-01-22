(function (app) {
    "use strict";
    app.controller("orderListCtrl",
        ["$scope", "$location", "orderService", "notificationService", "reportService", function ($scope, $location, orderService, notificationService, reportService) {

            $scope.dishes = [];
            $scope.dishes.orderList = [];
            $scope.dishes.deletedOrders = [];
            $scope.dateInput = new Date();
            $scope.dateInputMiliSec = $scope.dateInput.getTime();
            $scope.chefMail = "";
            $scope.headerArray = ["Order id", "Date", "User", "Role", "Dishes"];
            $scope.csvArr = [];

            function prepareArrayToCsv() {
                var csvArr = [];
                for (var order in $scope.dishes.orderList) {
                    var row = {};
                    row.id = $scope.dishes.orderList[order].Id;
                    row.date = $scope.dishes.orderList[order].Date;
                    row.user = $scope.dishes.orderList[order].User.Name;
                    row.role = $scope.dishes.orderList[order].User.Role;
                    row.dishes = "";
                    for (var dish in $scope.dishes.orderList[order].Dishes) {
                        row.dishes += $scope.dishes.orderList[order].Dishes[dish].Name + " * " + $scope.dishes.orderList[order].Dishes[dish].Number + " ";
                    }
                    csvArr.push(row);
                }
                $scope.csvArr = csvArr;
                //return csvArr;
            }
            
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
                                orderItem.Dishes = [];
                                if (!angular.isUndefined(data[order].Dishes.length)) {
                                    for (var i = 0; i < data[order].Dishes.length; i++) {
                                        if (angular.isUndefined(orderItem.Dishes[data[order].Dishes[i]])) {
                                            orderItem.Dishes[data[order].Dishes[i].ID] = {};
                                        }
                                        orderItem.Dishes[data[order].Dishes[i].ID].Number = 1 + (orderItem.Dishes[data[order].Dishes[i].ID].Number || 0);
                                        if (orderItem.Dishes[data[order].Dishes[i].ID].Number == 1) {
                                            orderItem.Dishes[data[order].Dishes[i].ID].ID = data[order].Dishes[i].ID;
                                            orderItem.Dishes[data[order].Dishes[i].ID].Name = data[order].Dishes[i].Name;
                                            orderItem.Dishes[data[order].Dishes[i].ID].ImagePath = data[order].Dishes[i].ImagePath;
                                        }
                                    }
                                    $scope.dishes.orderList.push(orderItem);
                                }
                            }
                           prepareArrayToCsv();
                        }
                    });
            }

            $scope.deleteOrder = function (order) {
                $scope.dishes.deletedOrders.push(order.Id);
                $scope.dishes.orderList.splice(order, 1);
                prepareArrayToCsv();
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
                reportService.sendMailToChef($scope.dateInputMiliSec, $scope.chefMail).then(
                    function (data) {
                        notificationService.displaySuccess("Mail send.");
                    }, function (status) {
                        notificationService.displayError("Can`t send mail. Try again later.");
                    });
            }
        }]);
})(angular.module("orderModule"));

