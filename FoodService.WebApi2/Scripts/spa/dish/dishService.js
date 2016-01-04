﻿(function (app) {
    'use strict';

    app.factory('dishService', dishService);

    dishService.$inject = ['$http', '$q', 'fileUploadService'];

    function dishService($http, $q, fileUploadService) {


        function search(page, pageSize, filterDishes) {
            page = page || 0;
            pageSize = pageSize || 6;

            var deferred = $q.defer();
            $http.get('api/dishes/search?page=' + page + '&pageSize=' + pageSize + '&filter=' + filterDishes).
              success(function (data) {
                  deferred.resolve(data);
              }).
             error(function (data, status) {


                 deferred.reject(status);
             });

            return deferred.promise;
        }

        function loadDishDetails(idDish) {
            var deferred = $q.defer();
            $http.get('/api/dishes/details/' + idDish).
              success(function (data) {
                  deferred.resolve(data);
              }).
             error(function (data, status) {
                 deferred.reject(status);
             });

            return deferred.promise;
        }


        function updateDish(dish) {
            var deferred = $q.defer();
            $http.post('/api/dishes/update', dish).
              success(function (data) {
                  deferred.resolve(data);
              }).
             error(function (data, status) {
                 deferred.reject(status);
             });

            return deferred.promise;
        }

        function addDish(dish) {
            var deferred = $q.defer();
            $http.post('/api/dishes/add', dish).
              success(function (data) {
                  deferred.resolve(data);
              }).
             error(function (status) {
                 deferred.reject(status);
             });

            return deferred.promise;
        }
        

        var service = {
            search: search,
            loadDishDetails: loadDishDetails,
            updateDish: updateDish,
            addDish: addDish
        }
        return service;
    }
})(angular.module('dishModule'))