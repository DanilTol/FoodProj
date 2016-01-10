(function (app) {
    'use strict';

    app.factory('dishsetService', [
        '$http', '$q', function ($http, $q) {

            function getDayMenu(date) {
                date = date || new Date().getTime();

                var deferred = $q.defer();
                $http.get('api/dishset/getdishmenu?miliSecFrom1970=' + date).
                    success(function (data) {
                        deferred.resolve(data);
                    }).
                    error(function (status) {
                        deferred.reject(status);
                    });
                return deferred.promise;
            }

            function editDayMenu(date,dishes) {
                var deferred = $q.defer();
                var setOnDay = [];
                setOnDay.DishId = dishes;
                setOnDay.Date = date;
                $http.post('api/dishset/editdishmenu', setOnDay).
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