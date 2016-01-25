(function (app) {
    "use strict";
    app.controller("dishsetCtrl", [
        "$scope", "$location", "dishService", "dishsetService", "notificationService", function ($scope, $location, dishService, dishsetService, notificationService) {

            $scope.dishes = [];
            $scope.dateInput = new Date();
            $scope.dateInputMiliSec = $scope.dateInput.getTime();
            
            $scope.editDayMenu = function () {
                var dishId = [];
                for (var i = 0; i <  $scope.dishes.set.length; i++) {
                    dishId.push($scope.dishes.set[i].ID);
                }
                dishsetService.editDayMenu($scope.dateInputMiliSec, dishId).then(
                    //success
                    function () {
                        notificationService.displaySuccess("Menu has been edited.");
                    }, function() {
                        notificationService.displayError("Can`t edit menu. Try again later.");
                    });
            }

            function loadDishset() {
                $location.search("date", $scope.dateInputMiliSec);
                $scope.dishes.set = [];

                dishsetService.filterDayMenu($scope.dateInputMiliSec,"").then(
                    //success
                    function (data) {
                        //$scope.dishes.set = data;
                        for (var i = 0; i < data.length; i++) {
                            var dish = {};
                            dish.ID = data[i].ID;
                            dish.ImagePath = data[i].ImagePath;
                            dish.Name = data[i].Name;
                            

                            $scope.dishes.set.push(dish);
                        }
                    });
            }

            $scope.$watch("dateInput", function () {
                $scope.dateInputMiliSec = $scope.dateInput.getTime();
                loadDishset();
            });

            $scope.page = 0;
            $scope.pagesCount = 0;
            $scope.pageSize = 16;
            $scope.filterDishes = "";

            function search() {
                dishService.search($scope.page, $scope.pageSize, $scope.filterDishes)
                     .then(
                         //success
                         function (data) {
                             $scope.dishes.allDishes = data.Items;
                             $scope.page = data.Page;
                             $scope.pagesCount = data.TotalPages;
                             $scope.totalCount = data.TotalCount;
                         }, function() {
                            notificationService.displayError("Can`t load availible dishes.");
                        });
            }

            $scope.clearSearch = function () {
                $scope.filterDishes = "";
                $scope.page = 0;
                search();
            }

            $scope.pageRoute = function (page) {
                $scope.page = page;
                search();
            }

            $scope.clearMenu = function() {
                $scope.dishes.set = [];
            }

            $scope.filterClick = function() {
                $scope.page = 0;
                search();
            }

            $scope.editDishSet = function(someDish) {
                var flag = true;
                for (var i = 0; i < $scope.dishes.set.length; i++) {
                    if ($scope.dishes.set[i].ID == someDish.ID) {
                        flag = false;
                        break;
                    }
                }
                if (flag) {
                    $scope.dishes.set.push(someDish);
                }
                $scope.$apply();
            }
            
            $scope.removeClickBtn = function (el) {
                var parent = document.getElementById("dishMenu");
                var child = el.target.parentNode.parentNode;
                var id = el.target.parentNode.attributes["id"].value;
                parent.removeChild(child);
                $scope.dishes.set = $.grep($scope.dishes.set, function (e) { return e.ID != id; });

            }

            $scope.showContent = function ($fileContent) {
                var c = $fileContent.split("\n");
                var k = [];
                for (var row in c) {
                    k.push(c[row].split(","));
                }
                k.pop(k[0]);
                $scope.dishes.set = [];
                for (var index in k) {
                    var dish = {};
                    if (k[index][0] != "" && k[index][1] != "" && k[index][2] != "") {
                        dish.ID = k[index][0];
                        dish.Name = k[index][2];
                        dish.ImagePath = k[index][1];
                        $scope.dishes.set.push(dish);
                    }
                }
            };

            search();
        }]);
})(angular.module("dishsetModule"));

