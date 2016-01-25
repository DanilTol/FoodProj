(function (app) {
    "use strict";
    app.controller("usereditCtrl",
        ["$scope", "$location", "notificationService", "accountService", function ($scope, $location, notificationService, accountService) {

            $scope.VM = {};
            $scope.VM.users = {};

            $scope.saveUser = function (user) {
                if (angular.isUndefined(user.EmailAddress) && angular.isUndefined(user.Salt)) {
                    notificationService.displayWarning("Enter email and password, please.");
                    return;
                }
                accountService.editAsAdmin(user).then(function() {
                    notificationService.displaySuccess("User edited.");
                });
            }

            $scope.removeUser = function(user) {
                accountService.removeUser(user.Id).then(function() {
                    notificationService.displaySuccess("User removed.");
                });
                $scope.VM.users.pop(user);
            }

            function loadUsers() {
                accountService.getAllUsers().then(function(data) {
                    $scope.VM.users = data;
                });
            }

            $scope.createRow = function() {
                var user = {};
                user.Name = "New User";
                user.Role = "user";
                user.Salt = "";
                $scope.VM.users.push(user);
            }


            loadUsers();
        }]);
})(angular.module("orderModule"));

