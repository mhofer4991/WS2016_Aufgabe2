app.controller('PostFilterController', function ($scope, Post, filterFilter) {
    $scope.filterModes = [
        {
            "description": "All", "expression": function () {
                return {};
            }
        },
        {
            "description": "Tag", "expression": function () {
                return { tags: { name: $scope.filterText } };
            }
        },
        {
            "description": "Text", "expression": function () {
                return { text: $scope.filterText };
            }
        },
        {
            "description": "Title", "expression": function () {
                return { title: $scope.filterText };
            }
        },
    ];

    $scope.filterMode = $scope.filterModes[0];

    $scope.filterText = "";

    $scope.sortModes = [
        { "description": "Time", "expression": "time" },
        { "description": "Title", "expression": "title" }
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

    $scope.addPost = function (post) {
        $scope.posts.push(post);
    };

    $scope.removePost = function (post) {
        $scope.posts.splice($scope.posts.indexOf(post), 1);
    };

    $scope.list2 = filterFilter($scope.posts, $scope.filterText);
});

app.controller('PostController', function ($scope, $location, $sce, Post) {
    $scope.safeText = $sce.trustAsHtml($scope.post.text);

    $scope.createPost = function (callback, errorCallback) {
        $scope.post.$save().then(
                function (value) {
                    callback(value);
                },
                function (error) {
                    errorCallback(error);
                }
            );
    };

    $scope.updatePost = function (callback, errorCallback) {
        $scope.post.$update().then(
                function (value) {
                    callback(value);
                },
                function (error) {
                    errorCallback(error);
                }
            );
    };

    $scope.deletePost = function (callback, errorCallback) {
        $scope.post.$delete().then(
                function (value) {
                    callback(value);
                },
                function (error) {
                    errorCallback(error);
                }
            );
    };

    $scope.visitBlog = function (post) {
        $location.path('/blogs/' + post.blogID);
    };

    $scope.visitPost = function (post) {
        $scope.visitPostId(post.id);
    };

    $scope.visitPostId = function (id) {
        $location.path('/posts/' + id);
    };

    $scope.handlePostError = function (error) {
        $scope.addError(error.data.text);
    };
});

app.controller('PostEditController', function ($scope, $sce, Post) {
    $scope.newTag = { "name": "" };

    $scope.deleteTag = function (tag) {
        $scope.post.tags.splice($scope.post.tags.indexOf(tag), 1);
    };

    $scope.newTagChanged = function () {
        if ($scope.newTag.name.slice(-1) === ' ') {
            var tag = { "name": $scope.newTag.name.trim() };

            if ($scope.post.tags.indexOf(tag) === -1 && tag.name !== '') {
                $scope.post.tags.push(tag);
            }

            $scope.newTag.name = '';
        }
    };

    $scope.keyDown = function (event) {
        if (event.keyCode === 8) {
            if ($scope.newTag.name === '') {
                if ($scope.post.tags.length > 0) {
                    $scope.newTag.name = $scope.post.tags.pop().name + ' ';
                }
            }
        }
    };
});

app.controller('PostListviewController', function ($scope, posts, listTitle) {
    $scope.posts = posts;
    $scope.listTitle = listTitle;
});

app.controller('PostDetailsController', function ($scope, $route, post) {
    $scope.post = post;
});

app.controller('PostAddController', function ($scope, Post, blog) {
    $scope.blog = blog;

    var newPost = new Post();
    newPost.blogID = blog.id;
    newPost.tags = [];

    $scope.post = newPost;
});