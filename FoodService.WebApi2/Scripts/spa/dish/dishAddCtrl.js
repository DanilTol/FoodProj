(function (app) {
    'use strict';

    app.controller('dishAddCtrl', [
        '$scope', '$location', 'dishService', function ($scope, $location, dishService) {

            var dishImage = null;

            $scope.addDish = function() {
                //if (dishImage) {
                //    fileUploadService.uploadImage(dishImage, $scope.dish.ID, updateDishModel);
                //}
                //else

                dishService.addDish($scope.dish).then(
                    //success
                    function (data) {
                        $scope.dish = data;
                        dishImage = null;
                        $location.path('/dishes');
                    });;
            }

            $scope.prepareFiles = function($files) {
                dishImage = $files;
            }

        }
    ]);
})(angular.module('dishModule'));