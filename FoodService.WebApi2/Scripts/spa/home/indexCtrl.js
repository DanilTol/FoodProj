(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', '$location', '$rootScope'];
    function indexCtrl($scope, $location, $rootScope) {
        $scope.pageClass = "page-home";
    }

})(angular.module('homeFoodService'));