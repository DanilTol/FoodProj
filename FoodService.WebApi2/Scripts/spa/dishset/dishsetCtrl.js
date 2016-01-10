(function (app) {
    'use strict';
    app.controller('dishsetCtrl', [
        '$scope', '$location', 'dishService', 'dishsetService', function ($scope, $location, dishService, dishsetService) {
            
            $scope.dishes = [];
            $scope.dateInput = new Date();
            $scope.dateInputMiliSec = $scope.dateInput.getTime();
            $scope.DateShow = new Date($location.search().date);

            $scope.editDayMenu = function () {
                dishsetService.editDayMenu($scope.dateInputMiliSec, $scope.dishes.set).then(
                    //success
                    function (data) {
                        var k = 10;
                        k++;
                    });
            }


            function loadDishset() {
                $location.search('date', $scope.dateInputMiliSec);
                $scope.DateShow = convertDate($scope.dateInputMiliSec);

                dishsetService.getDayMenu($scope.dateInputMiliSec).then(
                    //success
                    function (data) {
                        $scope.dishes.set = data;
                    });
            }
            
            function convertDate(date) {
                date = new Date(date); 
                var day =  date.getDate();        // yields day
                day = day < 10 ? "0" + day :day;
                var month = (date.getMonth() + 1);    // yields month
                month = month < 10 ? "0" + month : month;
                var year = date.getFullYear();  // yields year
               
                // After this construct a string with the above results as below
                var time = day + "/" + month + "/" + year;
                console.log(time);

                return time;
            }

            

            

            $scope.$watch("dateInput", function () {
                $scope.dateInputMiliSec = $scope.dateInput.getTime();
                loadDishset();
            });

            $scope.page = 0;
            $scope.pagesCount = 0;
            $scope.pageSize = 6;
            $scope.filterDishes = '';

            $scope.search = function () {
               dishService.search($scope.page, $scope.pageSize, $scope.filterDishes)
                    .then(
                        //success
                        function (data) {
                            $scope.dishes.allDishes = data.Items;
                            $scope.page = data.Page;
                            $scope.pagesCount = data.TotalPages;
                            $scope.totalCount = data.TotalCount;
                        });;
            }

            $scope.clearSearch = function clearSearch() {
                $scope.filterDishes = '';
                $scope.search();
            }

            $scope.pageRoute = function (page) {
                $scope.page = page;
                $scope.search();
            }

            function onStartPage() {
                $scope.search();

                //loadDishset();
            }


            $scope.$watch("menuDiv",
              function () {
                  $scope.DateShow = $scope.DateShow;
              }
                );


            onStartPage();
        }]);
})(angular.module('dishsetModule'));

