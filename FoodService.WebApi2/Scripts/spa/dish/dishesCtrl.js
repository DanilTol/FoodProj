(function (app) {
    'use strict';

    app.controller('dishesCtrl', dishesCtrl);

    dishesCtrl.$inject = ['$scope', 'apiService', '$q', '$http'];

    function dishesCtrl($scope, apiService, $q,  $http) {
        $scope.pageClass = 'page-dishes';
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pageSize = 5;
        $scope.filterDishes = '';

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        
        function search(page) {
            page = page || 0;
            var pageSize = 6;

            //var config = {
            //    params: {
            //        page: page,
            //        pageSize: 6,
            //        filter: $scope.filterDishes
            //    }
            //};

            var deferred = $q.defer();
            $http.get('api/dishes/search?page=' + page + '&pageSize='+ pageSize + '&filter=' + $scope.filterDishes).
              success(function (data) {


                  $scope.Dishes = data;
                  $scope.page = data.Page;
                  $scope.pagesCount = data.TotalPages;
                  $scope.totalCount = data.TotalCount;


                    deferred.resolve(data);
              }).
             error(function (data, status) {


                 deferred.reject(status);
             });

             return deferred.promise;



            //apiService.get('/api/dishapi', config,
            //    dishesLoadCompleted,
            //    dishesLoadFailed);
        }

        //function dishesLoadCompleted(result) {
        //    $scope.Dishes = result.data;
        //    $scope.page = result.data.Page;
        //    $scope.pagesCount = result.data.TotalPages;
        //    $scope.totalCount = result.data.TotalCount;

        //    //if ($scope.filterMovies && $scope.filterMovies.length) {
        //    //    notificationService.displayInfo(result.data.Items.length + ' dishes found');
        //    //}

        //}

        function dishesLoadFailed(response) {
            //notificationService.displayError(response.data);
        }


        function clearSearch() {
            $scope.filterDishes = '';
            search();
        }

        $scope.search();
    }


})(angular.module('dishModule'));