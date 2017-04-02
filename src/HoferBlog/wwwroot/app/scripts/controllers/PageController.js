app.controller('PageController', function ($scope) {
    $scope.itemsPerPage = 5;

    $scope.$watch('list', function (val) {
        if (undefined !== val) {
            $scope.items = [];

            $scope.items.push({ "number": 1, "state": "" });

            for (var i = 2; i <= Math.ceil($scope.list.length / $scope.itemsPerPage) ; i++) {
                $scope.items.push({ "number": i, "state": "" });
            }

            $scope.pageNumber = 1;

            $scope.selectPage($scope.pageNumber);
        }
    }, true);

    $scope.selectPage = function (number) {
        $scope.items[$scope.pageNumber - 1].state = "";
        $scope.items[number - 1].state = "active";

        $scope.pageNumber = number;

        $scope.from = $scope.itemsPerPage * $scope.pageNumber;

        if ($scope.from <= $scope.list.length) {
            $scope.to = $scope.itemsPerPage * -1;
        }
        else {
            $scope.to = ($scope.itemsPerPage - ($scope.from - $scope.list.length)) * -1;
        }
    }
});