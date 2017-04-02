app.controller('UserController', function ($scope, User) {
    $scope.updateUser = function () {
        $scope.user.$update(function () {
        });
    };
});

app.controller('UserDetailsController', function ($scope, user, posts) {
    $scope.user = user;
    $scope.posts = posts;
});