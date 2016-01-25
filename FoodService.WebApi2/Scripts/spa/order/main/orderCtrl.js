(function (app) {
    "use strict";
    app.controller("orderCtrl",
        ["$scope", "$location", "orderService", "dishsetService", "notificationService", function ($scope, $location, orderService, dishsetService, notificationService) {

        $scope.dishes = [];
        $scope.dateInput = new Date();
        $scope.dateInputMiliSec = $scope.dateInput.getTime();
        $scope.filterDishes = "";
        var maxNumberOfDish = 20;

        $scope.editUserSet = function () {
            var dishId = [];
            var dishNum = [];
            for (var x in $scope.dishes.userSet) {
                   dishId.push($scope.dishes.userSet[x].ID);
                   dishNum.push($scope.dishes.userSet[x].Count);
            }
            orderService.editUserSet($scope.dateInputMiliSec, dishId, dishNum).then(
                //success
                function (data) {
                    notificationService.displaySuccess("Order edited.");
                },function() {
                    notificationService.displayError("Can`t edit order. Try again later.");
                }
                );
        }

       
        $scope.filterClick = function () {
            dishsetService.filterDayMenu($scope.dateInputMiliSec, $scope.filterDishes).then(
                //success
                function (data) {
                    $scope.dishes.set = data;
                });
        }

        function loadUserSet() {
            $location.search("date", $scope.dateInputMiliSec);

            orderService.getUserSet($scope.dateInputMiliSec).then(
                //success
                function(data) {
                    $scope.dishes.userSet = [];
                    if (data != null) {
                        $scope.dishes.userSet = data;
                    }
                });
        }

        $scope.$watch("dateInput", function () {
            $scope.dateInputMiliSec = $scope.dateInput.getTime();
            loadUserSet();
            $scope.filterClick();
        });

        $scope.clearSearch = function () {
            $scope.filterDishes = "";
            $scope.filterClick();
        }

        $scope.clearOrder = function () {
            $scope.dishes.userSet = [];
        }

        $scope.editDishSet = function (someDish) {
            var k = 10;
            var flag = true;
            for (var dish in $scope.dishes.userSet) {
                
                if ($scope.dishes.userSet[dish].ID === someDish.ID) {
                    
                    if ($scope.dishes.userSet[dish].Count > 0) {
                        flag = false;
                    }
                    if (($scope.dishes.userSet[dish].Count || 0) < maxNumberOfDish)
                        $scope.dishes.userSet[dish].Count = 1 + ($scope.dishes.userSet[dish].Count || 0);
                    break;
                }
                
            }
            if (flag) {
                someDish.Count = 1;
                $scope.dishes.userSet.push(someDish);
            }
            
            $scope.$apply();
        }

        $scope.removeClickBtn = function (el) {
            var id = el.target.parentNode.attributes["id"].value;

            for (var dish in $scope.dishes.userSet) {
                var flag = false;
                if ($scope.dishes.userSet[dish].ID == id) {
                    --$scope.dishes.userSet[dish].Count;
                    if ($scope.dishes.userSet[dish].Count < 1) {
                        flag = true;
                    }
                }
                if (flag) {
                    var parent = document.getElementById("dishMenu");
                    var child = el.target.parentNode.parentNode;
                    parent.removeChild(child);
                    $scope.dishes.userSet.splice(dish, 1);
                }
            }
        }
        $scope.filterClick();
    }]);
})(angular.module("orderModule"));

