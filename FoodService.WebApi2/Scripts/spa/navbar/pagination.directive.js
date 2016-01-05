(function(app) {
    'use strict';

    app.directive('pagination', pagination);

    function pagination() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/Scripts/spa/navbar/pagination.html"
        }
    }

})(angular.module('common.ui'));