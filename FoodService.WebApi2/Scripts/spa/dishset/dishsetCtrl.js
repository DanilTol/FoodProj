(function (app) {
    'use strict';
    app.controller('dishsetCtrl', [
        '$scope', '$location', 'dishService', 'dishsetService', function ($scope, $location, dishService, dishsetService) {
            
            $scope.dishes = [];
            $scope.dateInput = new Date();

            function convertDate(date) {
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

            $scope.DateShow = $location.search().Date || convertDate(new Date());

            function loadDishset() {
                $location.search('date', $scope.dateInput);
                $scope.DateShow = convertDate($scope.dateInput);

                dishsetService.getDayMenu($scope.dateInput).then(
                    //success
                    function(data) {
                        $scope.dishes.set = data;
                    });
            }

            $scope.$watch("dateInput", function () {
                $location.search('date', $scope.dateInput);
                loadDishset();
            });

            $scope.page = 0;
            $scope.pagesCount = 0;
            $scope.pageSize = 5;
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

            //$scope.clearSearch = function clearSearch() {
            //    $scope.filterDishes = '';
            //    $scope.search();
            //}

            //$scope.pageRoute = function (page) {
            //    $scope.page = page;
            //    //$scope.search();
            //}

            function onStartPage() {
                $scope.search();

                loadDishset();
            }

            onStartPage();
        }]);
})(angular.module('dishsetModule'));

