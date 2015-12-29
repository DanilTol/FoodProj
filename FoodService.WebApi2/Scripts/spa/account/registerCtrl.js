(function (app) {
    'use strict';

    app.controller('registerCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope', 'accountService', '$location'];


    function registerCtrl($scope, accountService, $location) {
        $scope.pageClass = 'page-login';
        $scope.user = {};

        $scope.register = function () {
            accountService.register($scope.user).then(
            //success
            function () {
                $location.path('/');
                $scope.userData.LogInUser = $scope.user;
            });
            
        }
    }

})(angular.module('accountModule'));