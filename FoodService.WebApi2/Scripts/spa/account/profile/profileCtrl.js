(function (app) {
    "use strict";

    app.controller("profileCtrl", [
        "$scope", "accountService", "notificationService", function ($scope, accountService, notificationService) {
            $scope.userProfile = {};

            accountService.getUserAsync().
                then(function(data) {
                    $scope.userProfile = data;
                    $scope.userProfile.Salt = "";
                    $scope.$apply();
                },function(data) {
                    
                });
            
            
            $scope.editProfile = function() {
                accountService.editProfile($scope.userProfile).
                    then(function() {
                        notificationService.displaySuccess("Profile changed");
                    }, function() {
                        notificationService.displayError("Can`t edit profile.");
                    });
            }
        }
    ]);
})(angular.module("accountModule"));