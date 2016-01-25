(function () {
    "use strict";
    angular.module("homeModule", ["common.core"])
         .config(function ($routeProvider) {
            $routeProvider
                .when("/", {
                    templateUrl: "scripts/spa/home/index/index.html"
                });
         });
})();