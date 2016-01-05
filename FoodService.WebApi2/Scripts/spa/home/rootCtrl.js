(function (app) {
    'use strict';

    app.controller('rootCtrl', [
        '$scope', '$location', 'accountService',
        function($scope, $location, accountService) {
            $scope.logout = function() {
                accountService.logoutUser();
                $location.path('#/');
            }
        }
    ]);

})(angular.module('homeFoodService'));

