(function (app) {
    "use strict";

    app.directive("topBar", [
         "accountService",  "$location", function (accountService,  $location) {
            return {
                restrict: "E",
                replace: true,
                templateUrl: "/Scripts/spa/navbar/top/topBar.html",
                link: function (scope) {
                    scope.userProfile = accountService.getUserData;
                    scope.isUserLoggedIn = accountService.isUserLoggedIn;
                    scope.logout = function () {
                        accountService.logoutUser();
                        $location.path("#/");
                    }
                }
            }
        }]
            );

})(angular.module("navbarModule"));