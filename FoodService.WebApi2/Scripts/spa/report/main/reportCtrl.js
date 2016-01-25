(function (app) {
    "use strict";
    app.controller("reportCtrl",
        ["$scope", "notificationService", "reportService", function ($scope, notificationService, reportService) {

            $scope.VM = {};
            $scope.htmlDif = "";
            $scope.VM.chefMail = "fv";
            $scope.reports = [];
            $scope.dateReport = new Date();
            $scope.allowSend = false;
            
            function loadReports() {
                reportService.getallreports().then(function(data) {
                    $scope.reports = data;
                    for (var i = 0; i < $scope.reports.length; i++) {
                        $scope.reports[i].Date = $scope.reports[i].Date.substring(0, 10);
                    }
                }, function() {
                    notificationService.displayError("Can`t get reports.");
                });
            }

            $scope.reportDif = function(rep) {
                $scope.dateReport = new Date(rep.Date);
                $scope.allowSend = true;
                $scope.VM.chefMail = rep.Email;
                $scope.htmlDif = "<h2>Changes in report on " + rep.Date + "</h2>";
                if (rep.Id != 0) {
                    reportService.reportsformatch($scope.dateReport.getTime()).then(
                        function(data) {
                            var dmp = new diff_match_patch();
                            dmp.Diff_Timeout = 0;
                            dmp.Diff_EditCost = 4;

                            var d = dmp.diff_main(data, rep.ChefReport);
                            dmp.diff_cleanupSemantic(d);
                            
                            $scope.htmlDif += dmp.diff_prettyHtml(d).replace(new RegExp('[' + ";" + ']', 'g'), "<br/>");
                        }, function() {
                            notificationService.displayError("Can`t match reports.");
                        });
                } else {
                    $scope.htmlDif += rep.ChefReport.replace(new RegExp('[' + ";" + ']', 'g'), "<br/>");
                }
            }

            $scope.sendToChef = function () {
                reportService.sendMailToChef($scope.dateReport.getTime(), $scope.VM.chefMail).then(
                    function (data) {
                        notificationService.displaySuccess("Mail send.");
                    }, function (status) {
                        notificationService.displayError("Can`t send mail. Try again later.");
                    });
            }


            loadReports();
        }]);
})(angular.module("reportModule"));

