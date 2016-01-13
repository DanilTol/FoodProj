(function (app) {
    "use strict";
    app.controller("orderCtrl", ["$scope", "$location", "$filter", "orderService", "dishsetService", function ($scope, $location, $filter, orderService, dishsetService) {

        $scope.dishes = [];
        $scope.dateInput = new Date();
        $scope.dateInputMiliSec = $scope.dateInput.getTime();
        $scope.DateShow = new Date($location.search().date);
        $scope.filterDishes = "";
        var maxNumberOfDish = 20;

        $scope.editUserSet = function () {
            var dishId = [];
            for (var x in $scope.dishes.userSet) {
                for (var j = 0; j < $scope.dishes.userSet[x].Number; j++) {
                    dishId.push($scope.dishes.userSet[x].ID);
                }
            }
            orderService.editUserSet($scope.dateInputMiliSec, dishId).then(
                //success
                function (data) {
                });
        }

        function convertDate(date) {
            date = new Date(date);
            var day = date.getDate();        // yields day
            day = day < 10 ? "0" + day : day;
            var month = (date.getMonth() + 1);    // yields month
            month = month < 10 ? "0" + month : month;
            var year = date.getFullYear();  // yields year

            // After this construct a string with the above results as below
            var time = day + "/" + month + "/" + year;
            console.log(time);

            return time;
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
            $scope.DateShow = convertDate($scope.dateInputMiliSec);

            

            orderService.getUserSet($scope.dateInputMiliSec).then(
                //success
                function (data) {
                    //$scope.dishes.userSet = data;
                    //$scope.dishes.userSet[0] = {};
                    //$scope.dishes.userSet[0].ID = 1;
                    //$scope.dishes.userSet[0].Name = 'Hello';
                    //$scope.dishes.userSet[0].ImagePath = "01.jpg";
                    //$scope.dishes.userSet[1] = {};
                    //$scope.dishes.userSet[1].ID = 1;
                    //$scope.dishes.userSet[1].Name = 'Hello';
                    //$scope.dishes.userSet[1].ImagePath = "01.jpg";

                    //$scope.dishes.userSet = data;
                    //data = [];
                    //data[0] = {};
                    //data[0].ID = 1;
                    //data[0].Name = 'Hello';
                    //data[0].ImagePath = "01.jpg";
                    //data[1] = {};
                    //data[1].ID = 1;
                    //data[1].Name = 'Hello';
                    //data[1].ImagePath = "01.jpg";

                    $scope.dishes.userSet = [];
                    //$scope.dishes.userSet[1] = {};

                    for (var i = 0; i < data.length; i++) {
                        if (angular.isUndefined($scope.dishes.userSet[data[i].ID])) {
                            $scope.dishes.userSet[data[i].ID] = {};
                        }
                        $scope.dishes.userSet[data[i].ID].Number = 1 + ($scope.dishes.userSet[data[i].ID].Number || 0);
                        if ($scope.dishes.userSet[data[i].ID].Number == 1) {
                            $scope.dishes.userSet[data[i].ID].ID = data[i].ID;
                            $scope.dishes.userSet[data[i].ID].Name = data[i].Name;
                            $scope.dishes.userSet[data[i].ID].ImagePath = data[i].ImagePath;
                        }
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
            var flag = true;
            for (var dish in $scope.dishes.userSet) {
                if ($scope.dishes.userSet[someDish.ID] || false)
                    flag = false;
            }
            if (flag) {
                $scope.dishes.userSet[someDish.ID] = someDish;
            }
            if (($scope.dishes.userSet[someDish.ID].Number || 0) < maxNumberOfDish)
                $scope.dishes.userSet[someDish.ID].Number = 1 + ($scope.dishes.userSet[someDish.ID].Number || 0);
            $scope.$apply();
        }

        $scope.removeClickBtn = function (el) {
            var id = el.target.parentNode.attributes["id"].value;
            var flag = true;

            if ($scope.dishes.userSet[id] || false) {
                --$scope.dishes.userSet[id].Number;
                if ($scope.dishes.userSet[id].Number > 0) {
                    flag = false;
                }
            }
            if (flag) {
                var parent = document.getElementById('dishMenu');
                var child = el.target.parentNode.parentNode;
                parent.removeChild(child);
                $scope.dishes.userSet.splice(id, 1);
                //$scope.dishes.userSet = $.grep($scope.dishes.userSet, function (e) { return e.ID != id; });
            }
        }
        $scope.filterClick();
    }]);
})(angular.module("orderModule"));

