app.controller('AlertController', function ($scope) {
    $scope.alerts = [];

    $scope.addSucess = function (text) {
        $scope.addAlert({ "success": true, "text": text });
    };

    $scope.addError = function (text) {
        $scope.addAlert({ "success": false, "text": text});
    };

    $scope.addAlert = function (alert) {
        $scope.alerts.push(alert);
    };

    $scope.removeAlert = function (alert) {
        $scope.alerts.splice($scope.alerts.indexOf(alert), 1);
    };
});