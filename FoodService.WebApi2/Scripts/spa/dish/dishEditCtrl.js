(function (app) {
    'use strict';

    app.controller('dishEditCtrl', dishEditCtrl);

    dishEditCtrl.$inject = ['$scope',  '$routeParams', 'apiService'];

    function dishEditCtrl($scope, $routeParams, apiService) {
        $scope.pageClass = 'page-dishes';
        $scope.loadingdish = true;
        $scope.UpdateDish = updateDish;
        $scope.prepareFiles = prepareFiles;
       
        var dishImage = null;

        function dishLoadCompleted(result) {
            $scope.dish = result.data;
            $scope.loadingDish = false;

        }

        function dishLoadFailed(response) {
            // notificationService.displayError(response.data);
        }

        function loadDish() {

            $scope.loadingDish = true;

            apiService.get('/api/dishes/details/' + $routeParams.id, null,
            dishLoadCompleted,
            dishLoadFailed);
        }
        
        function updateDishSucceded(response) {
            console.log(response);
            //notificationService.displaySuccess($scope.movie.Title + ' has been updated');
            $scope.dish = response.data;
            dishImage = null;
        }

        function updateDishFailed(response) {
            //notificationService.displayError(response);
        }

        function updateDishModel() {
            apiService.post('/api/dishes/update', $scope.dish,
            updateDishSucceded,
            updateDishFailed);
        }

        function updateDish() {
            //if (dishImage) {
            //    fileUploadService.uploadImage(dishImage, $scope.dish.ID, updateDishModel);
            //}
            //else
                updateDishModel();
        }

        function prepareFiles($files) {
            dishImage = $files;
        }

       
        
        loadDish();
    }

})(angular.module('dishModule'));