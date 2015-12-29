(function (app) {
    'use strict';

    app.controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', 'accountService', '$rootScope', '$location'];

    function loginCtrl($scope, accountService, $rootScope, $location) {
        $scope.pageClass = 'page-login';
        $scope.user = {};

        $scope.login = function () {
            accountService.login($scope.user)
            .then(
            //success
            function() {
                if ($rootScope.previousState)
                    $location.path($rootScope.previousState);
                else
                    $location.path('/');
            });;
        }
    }
})(angular.module('accountModule'));