(function (app) {
    "use strict";

    app.controller("profileCtrl", [
        "$scope", "accountService", "notificationService", function ($scope, accountService, notificationService) {
            $scope.userProfile = accountService.getUserData();
            $scope.userProfile.Salt = "";
            //accountService.getUserAsync().
            //    then(function (data) {
            //        $scope.userProfile = data;
            //        $scope.userProfile.Salt = "";
            //    }, function () {

            //    });

            $scope.editProfile = function () {
                accountService.editProfile($scope.userProfile).
                    then(function () {
                        notificationService.displaySuccess("Profile changed");
                    }, function () {
                        notificationService.displayError("Can`t edit profile.");
                    });
            }
        }
    ]);
})(angular.module("accountModule"));