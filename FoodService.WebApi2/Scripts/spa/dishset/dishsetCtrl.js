(function (app) {
    'use strict';
    app.controller('dishsetCtrl', [
        '$scope', '$routeParams', '$location', 'dishsetService', function($scope, $routeParams,$location, dishsetService) {


            function convertDate(date) {
                var day = date.getDate();
                var monthIndex = date.getMonth() + 1;
                var year = date.getFullYear();

                console.log(day, monthIndex, year);



                return day + "/" + monthIndex + "/" + year;
            }

            $scope.DayDate = $location.search().Date || convertDate(new Date());

            function loadDishset() {
                $scope.DayDate = $location.search().Date || convertDate(new Date());

                dishsetService.getDayMenu($scope.DayDate).then(
                    //success
                    function(data) {
                        $scope.dishes = data;
                    });
            }

            
            loadDishset();
        }]);
})(angular.module('dishsetModule'));
