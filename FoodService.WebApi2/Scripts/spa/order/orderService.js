(function (app) {
    "use strict";

    app.factory("orderService", [
        "$http", "$q", function ($http, $q) {
            return {
                getUserSet: function(date) {
                    date = date || new Date().getTime();

                    var deferred = $q.defer();
                    $http.get("api/order/getuserset?miliSecFrom1970=" + date).
                        success(function(data) {
                            deferred.resolve(data);
                        }).
                        error(function(status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },
                editUserSet: function(date, dishes, number) {
                    var deferred = $q.defer();
                    var setOnDay = {};
                    setOnDay.DishId = dishes;
                    setOnDay.Date = date;
                    setOnDay.DishNum = number;
                    $http.post("api/order/edituserset", setOnDay).
                        success(function(data) {
                            deferred.resolve(data);
                        }).
                        error(function(status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },
                getOrderList: function(date) {
                    var deferred = $q.defer();
                    $http.get("api/order/orderlist?miliSecFrom1970=" + date).
                        success(function(data) {
                            deferred.resolve(data);
                        }).
                        error(function(status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },
                deleteOrders: function(orders) {
                    var deferred = $q.defer();
                    $http.post("api/order/ordersdelete", orders).
                        success(function(data) {
                            deferred.resolve(data);
                        }).
                        error(function(status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },
                sendMailToChef: function(dateInputMiliSec, mail) {
                    var deferred = $q.defer();
                    $http.get("api/order/sentmail?miliSecFrom1970=" + dateInputMiliSec + "&chefMail=" + mail).
                        success(function(data) {
                            deferred.resolve(data);
                        }).
                        error(function(status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },
                notCheckedNotification: function() {
                    var deferred = $q.defer();
                    $http.get("api/order/notificationcheckorders").
                        success(function(data) {
                            deferred.resolve(data);
                        }).
                        error(function(status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },
                notCheckedOrder: function() {
                var deferred = $q.defer();
                    $http.get("api/order/uncheckorders").
                    success(function(data) {
                        deferred.resolve(data);
                    }).
                    error(function(status) {
                        deferred.reject(status);
                    });
                return deferred.promise;
            }
            }
        }]);
})(angular.module("orderModule"))