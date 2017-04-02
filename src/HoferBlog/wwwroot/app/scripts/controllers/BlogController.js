app.controller('BlogFilterController', function ($scope, Blog) {
    $scope.filterModes = [
        {
            "description": "All", "expression": function () {
                return {};
            }
        },
        {
            "description": "Name", "expression": function () {
                return { name: $scope.filterText };
            }
        },
    ];

    $scope.filterMode = $scope.filterModes[0];

    $scope.filterText = "";

    $scope.sortModes = [
        { "description": "Last edited", "expression": "lastEdited" },
        { "description": "Posts", "expression": "postsCount" },
        { "description": "Name", "expression": "name" }
    ];

    $scope.sortMode = $scope.sortModes[0];

    $scope.orderModes = [
        { "description": "Descending", "expression": "-" },
        { "description": "Ascending", "expression": "" }
    ];

    $scope.orderMode = $scope.orderModes[0]

    $scope.customOrdering = function () {
        return $scope.orderMode.expression + '' + $scope.sortMode.expression;
    };

    $scope.addBlog = function (blog) {
        $scope.blogs.push(blog);
    };

    $scope.removeBlog = function (blog) {
        $scope.blogs.splice($scope.blogs.indexOf(blog), 1);
    };
});

app.controller('BlogController', function ($scope, $location, Blog) {
    $scope.createBlog = function (callback, errorCallback) {
        $scope.blog.$save().then(
                function (value) {
                    callback(value);
                },
                function (error) {
                    errorCallback(error);
                }
            );
    };

    $scope.updateBlog = function (callback, errorCallback) {
        $scope.blog.$update().then(
                function (value) {
                    callback(value);
                },
                function (error) {
                    errorCallback(error);
                }
            );
    };

    $scope.deleteBlog = function (callback, errorCallback) {
        $scope.blog.$delete().then(
                function (value) {
                    callback(value);
                },
                function (error) {
                    errorCallback(error);
                }
            );
    };

    $scope.visitUser = function (blog) {
        $location.path('/users/' + blog.userName);
    };

    $scope.visitBlog = function (blog) {
        $scope.visitBlogId(blog.id);
    };

    $scope.visitBlogId = function (id) {
        $location.path('/blogs/' + id);
    };

    $scope.handleBlogError = function (error) {
        $scope.addError(error.data.text);
    };
});

app.controller('BlogListviewController', function ($scope, blogs, listTitle) {
    $scope.blogs = blogs;
    $scope.listTitle = listTitle;
});

app.controller('BlogDetailsController', function ($scope, blog, posts) {
    $scope.blog = blog;
    $scope.posts = posts;
});

app.controller('BlogAddController', function ($scope, Blog) {
    var newBlog = new Blog();
    newBlog.name = "New blog";

    $scope.blog = newBlog;
});