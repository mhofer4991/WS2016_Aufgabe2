app.controller('MainController', function ($scope, $location) {
    $scope.searchTerm = '';

    $scope.visitHome = function()
    {
        $location.path('/');
    }

    $scope.goBack = function () {
        window.history.back();
    };
});