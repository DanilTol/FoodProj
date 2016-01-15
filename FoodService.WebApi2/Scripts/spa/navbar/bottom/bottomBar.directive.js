(function(app) {
    "use strict";
    app.directive("bottomBar", function() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/Scripts/spa/navbar/bottom/bottomBar.html"
        }
    });
})(angular.module("navbarModule"));