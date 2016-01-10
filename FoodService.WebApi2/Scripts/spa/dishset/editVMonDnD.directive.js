(function (app) {
    'use strict';

    app.directive('editVMonDnD', ['$sce', function($sce) {
            return {
                restrict: 'A',
                require: '?ngModel',
                link: function(scope, element, attrs, ngModel) {
                    if (!ngModel) return;

                    // Specify how UI should be updated
                    ngModel.$render = function() {
                        element.html($sce.getTrustedHtml(ngModel.$viewValue || ''));
                    };

                    function read() {
                        var html = element.html();
                       
                        if (attrs.stripBr && html == '<br>') {
                            html = '';
                        }
                        ngModel.$setViewValue(html);
                    }

                    element.on('ondrop', function() {
                        scope.$evalAsync(read);
                    });
                    read();
                }
            };
        }
    ]);
})(angular.module('dishsetModule'));