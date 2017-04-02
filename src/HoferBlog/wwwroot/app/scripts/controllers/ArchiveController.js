app.controller('ArchiveController', function ($scope, $route, $location, posts) {
    $scope.posts = posts;
    $scope.month = $route.current.params.month;
    $scope.year = $route.current.params.year;

    $scope.visitArchive = function () {
        if ($scope.month <= 0) {
            $scope.month = 12;
        }
        else if ($scope.month > 12)
        {
            $scope.month = 1;
        }

        if ($scope.year < 0)
        {
            $scope.year = 2000;
        }

        $location.path('/archive/' + $scope.year + '/' + $scope.month);
    };

    $scope.previousYear = function () {
        $scope.year--;
        $scope.visitArchive();
    };

    $scope.nextYear = function () {
        $scope.year++;
        $scope.visitArchive();
    };

    $scope.previousMonth = function () {
        $scope.month--;
        $scope.visitArchive();
    };

    $scope.nextMonth = function () {
        $scope.month++;
        $scope.visitArchive();
    };
});