(function (app) {
    "use strict";

    app.factory("accountService", ["$http", "$q", function ($http, $q) {
            var userProfile;

            function setProfileInfo() {
                var deferred = $q.defer();
                $http.get("/api/account/profileInfo").
                    success(function (data1) {
                        userProfile = data1;
                        deferred.resolve(data1);
                    }).
                    error(function (data1, status) {
                        deferred.resolve(status);
                    });
                return deferred.promise;
            }

            function getUserData() {
                if (angular.isUndefined(userProfile)) {
                    userProfile = {};
                    userProfile.Name = "";
                    $http.get("/api/account/profileInfo").
                    success(function (data1) {
                        userProfile = data1;
                    }).
                    error(function (data1, status) {
                    });
                }
                return userProfile;
            }

            function login(user) {
                var deferred = $q.defer();
                $http.post("/api/account/login", user).
                    success(function (data) {

                        sessionStorage.foodServiceToken = data;

                        $http.get("/api/account/profileInfo").
                    success(function (data1) {
                        userProfile = data1;
                    }).
                    error(function (data1, status) {
                    });
                        deferred.resolve(data);
                    }).
                    error(function (data, status) {
                        deferred.reject(status);
                    });

                return deferred.promise;
            }

            function register(user) {
                var deferred = $q.defer();
                $http.post("/api/account/register", user).
                    success(function (data) {
                        sessionStorage.foodServiceToken = data;

                        deferred.resolve(data);
                    }).
                    error(function (data, status) {

                        deferred.reject(status);
                    });
                return deferred.promise;
            }

            function logoutUser() {
                sessionStorage.clear();
            }

            function isUserLoggedIn() {
                return sessionStorage.foodServiceToken != null;
            }

            var service = {
                login: login,
                register: register,
                logoutUser: logoutUser,
                isUserLoggedIn: isUserLoggedIn,
                getUserData: getUserData
            }
            return service;
        }
    ]);
})(angular.module("accountModule"));
