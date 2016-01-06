(function (app) {
    'use strict';

    app.factory('dishsetService', [
        '$http', '$q', function ($http, $q) {

            function getDayMenu(date) {
                date = date || new Date();

                var deferred = $q.defer();
                $http.get('api/dishset/getdishmenu?datestr=' + date).
                    success(function (data) {
                        deferred.resolve(data);
                    }).
                    error(function (status) {
                        deferred.reject(status);
                    });
                return deferred.promise;
            }

            function editDayMenu(dishes) {
                var deferred = $q.defer();
                $http.post('api/dishset/editdishmenu', dishes).
                    success(function (data) {
                        deferred.resolve(data);
                    }).
                    error(function (status) {
                        deferred.reject(status);
                    });
                return deferred.promise;
            }

            var service = {
                getDayMenu: getDayMenu,
                editDayMenu: editDayMenu
            }
            return service;
        }
    ]);
})(angular.module('dishsetModule'))