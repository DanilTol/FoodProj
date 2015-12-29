(function (app) {
    'use strict';

    app.controller('dishDetailsCtrl', dishDetailsCtrl);

    dishDetailsCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService'];

    function dishDetailsCtrl($scope, $location, $routeParams, apiService) {
        $scope.pageClass = 'page-dishes';
   
        function loadDish() {
            apiService.get('/api/dishes/details/' + $routeParams.id, null,
            dishLoadCompleted,
            dishLoadFailed);
        }

        function dishLoadCompleted(result) {
            $scope.dish = result.data;
            $scope.loadingMovie = false;
        }

        function dishLoadFailed(response) {
            //notificationService.displayError(response.data);
        }

        loadDish();
    }

})(angular.module('dishModule'));
//(angular.module('homeFoodService'));