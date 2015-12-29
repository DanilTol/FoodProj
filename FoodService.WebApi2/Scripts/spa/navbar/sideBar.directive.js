(function(app) {
    'use strict';

    function sideBar() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/Scripts/spa/navbar/sideBar.html"
        }
    }

    app.directive('sideBar', sideBar);
})(angular.module('common.ui'));