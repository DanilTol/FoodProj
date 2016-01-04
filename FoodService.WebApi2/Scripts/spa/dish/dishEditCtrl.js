(function (app) {
    'use strict';

    app.controller('dishEditCtrl', [
        '$scope', '$routeParams', 'dishService', function($scope, $routeParams, dishService) {

            $scope.UpdateDish = updateDish;
            $scope.prepareFiles = prepareFiles;

            var dishImage = null;

            function updateDish() {
                //if (dishImage) {
                //    fileUploadService.uploadImage(dishImage, $scope.dish.ID, updateDishModel);
                //}
                //else

                dishService.updateDish($scope.dish).then(
                    //success
                    function(data) {
                        $scope.dish = data;
                        dishImage = null;
                    });;
            }

            function prepareFiles($files) {
                dishImage = $files;
            }


            function loadDetails() {
                dishService.loadDishDetails($routeParams.id).then(
                    //success
                    function(data) {
                        $scope.dish = data;
                    });;
            }

            loadDetails();

        }
    ]);
})(angular.module('dishModule'));