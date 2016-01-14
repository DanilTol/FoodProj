(function(app) {
    'use strict';
    app.factory('sessionInjector', ['$location', 'notificationService',function($location, notificationService) {
        var sessionInjector = {
            request: function(config) {
                if (sessionStorage.foodServiceToken) {
                    config.headers['Token'] = sessionStorage.foodServiceToken;
                }
                return config;
            },
            'responseError': function(rejection) {
                if (rejection.status == 401) {
                    $location.path('/');
                    notificationService.displayError('Authentication required.');
                }
            }
        };
        return sessionInjector;
    }]);
    app.config([
        '$httpProvider', function($httpProvider) {
            $httpProvider.interceptors.push('sessionInjector');
        }
    ]);
})(angular.module("common.core"));