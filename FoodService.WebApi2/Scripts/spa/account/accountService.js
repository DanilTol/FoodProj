(function (app) {
    'use strict';

    app.factory('accountService', accountService);

    accountService.$inject = ['$http', '$q'];

    function accountService($http, $q) {
        var userProfile = {};
        var userName = "";

        function getUserData() {
            userName = userProfile.Name;
            userName = "Hello";
            return userName;
        }

        function loginFailed(response) {
            userName = "Fail";
            // notificationService.displayError(response.data);
        }

        function login(user) {

            var deferred = $q.defer();
            $http.post('/api/account/login', user).
             success(function (data) {

                 sessionStorage.foodServiceToken = data;

                 $http({ method: 'GET', url: '/api/account/profileInfo' }).
                  success(function (data1) {
                      userProfile = data1;
                  }).
                 error(function (data1, status) {
                 });
                 
                 deferred.resolve(data);
             }).
            error(function (data, status) {
                loginFailed(status);
                deferred.reject(status);
            });

            return deferred.promise;
        }

        function registrationFailed(response) {
            // notificationService.displayError('Registration failed. Try again.');
        }


        function register(user) {

             var deferred = $q.defer();
            $http.post('/api/account/register', user).
              success(function (data) {
                  sessionStorage.foodServiceToken = data;

                    deferred.resolve(data);
              }).
             error(function (data, status) {
                 registrationFailed(status);
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
            getUserData: getUserData,
        }
        return service;
    }



})(angular.module('accountModule'));