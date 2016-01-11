(function (app) {
    "use strict";
    app.controller('dishsetCtrl', [
        '$scope', '$location', '$filter', 'dishService', 'dishsetService', function ($scope, $location, $filter, dishService, dishsetService) {

            $scope.dishes = [];
            $scope.dateInput = new Date();
            $scope.dateInputMiliSec = $scope.dateInput.getTime();
            $scope.DateShow = new Date($location.search().date);

            $scope.editDayMenu = function () {
                var dishId = [];
                for (var i = 0; i <  $scope.dishes.set.length; i++) {
                    dishId.push($scope.dishes.set[i].ID);
                }
                dishsetService.editDayMenu($scope.dateInputMiliSec, dishId).then(
                    //success
                    function (data) {
                        var k = 10;
                        k++;
                    });
            }

            //$scope.$watch("menuDiv", function () {
            //    //$scope.allDishesFilter = $filter('unchosenDishes')($scope.dishes.allDishes, $scope.dishes.set);
            //    console.log("menu div change");
            //});


            //$scope.$watch("dishes.set.length", function () {
            //    //$scope.allDishesFilter = $filter('unchosenDishes')($scope.dishes.allDishes, $scope.dishes.set);
            //    console.log("menu div change");
            //}, true);


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

            function loadDishset() {
                $location.search("date", $scope.dateInputMiliSec);
                $scope.DateShow = convertDate($scope.dateInputMiliSec);
               
                //var myEl = angular.element(document.querySelector('#emptyMe'));
                //myEl.empty();
                //$scope.dishes.set = {};

                dishsetService.getDayMenu($scope.dateInputMiliSec).then(
                    //success
                    function (data) {
                        $scope.dishes.set = data;
                        //$scope.allDishesFilter = $filter('unchosenDishes')($scope.dishes.allDishes, $scope.dishes.set);
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
                             //$scope.dishes.allDishesFilter = $filter('unchosenDishes')($scope.dishes.allDishes, $scope.dishes.set);
                         });;
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

            $scope.filterClick = function() {
                $scope.page = 0;
                search();
            }

            $scope.editDishSet = function(dishId) {
                var inSet = {};
                inSet.ID = dishId;
                $scope.dishes.set.push(inSet);
                //$scope.dishes.allDishesFilter = $filter('unchosenDishes')($scope.dishes.allDishes, $scope.dishes.set);
            }



            search();
        }]);
})(angular.module('dishsetModule'));

