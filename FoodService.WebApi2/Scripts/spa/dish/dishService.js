(function (app) {
    'use strict';

    app.factory('dishService', dishService);

    dishService.$inject = ['$http', '$q'];

    function dishService($http, $q) {


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

        var service = {
            search: search
        }
        return service;
    }
})(angular.module('dishModule'))