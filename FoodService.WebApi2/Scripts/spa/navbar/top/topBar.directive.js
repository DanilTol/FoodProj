(function (app) {
    "use strict";

    app.directive("topBar", [
         "accountService", "$location", "orderService", function (accountService, $location, orderService) {
             return {
                 restrict: "E",
                 replace: true,
                 templateUrl: "/Scripts/spa/navbar/top/topBar.html",
                 link: function (scope) {
                     scope.unCheckedOrders = function () {
                         orderService.notCheckedNotification
                             .then(function (data) {
                                 //scope.unCheckedOrders = data;
                                 return data;
                             }, function(data) {
                                 return 999;
                             });
                     }

                     
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