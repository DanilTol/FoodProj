(function(app) {
    'use strict';

    app.directive('bottomBar', bottomBar);

    function bottomBar() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/Scripts/spa/navbar/bottomBar.html"
        }
    }

})(angular.module('common.ui'));