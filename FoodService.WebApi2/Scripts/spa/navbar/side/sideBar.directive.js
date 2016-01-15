(function(app) {
    "use strict";
    app.directive("sideBar", function() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/Scripts/spa/navbar/side/sideBar.html"
        }
    });
})(angular.module("navbarModule"));