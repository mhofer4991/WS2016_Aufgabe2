app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
    .when('/', {
        templateUrl: '/app/views/overview.html',
        controller: 'OverviewController',
        resolve: {
            posts: function (Post) {
                return Post.getLatest().$promise.then(function (posts) {
                    return posts;
                });
            }
        }
    })
    .when('/posts/:id', {
        templateUrl: '/app/views/postview.html',
        controller: function ($route, $location) {
            $location.path('/posts/' + $route.current.params.id + '/redirectToFriendlyUrl').replace();
        }
    })
    .when('/posts/:id/edit', {
        templateUrl: '/app/views/posteditview.html',
        controller: 'PostDetailsController',
        resolve: {
            post: function ($route, Post) {
                return Post.get({ id: $route.current.params.id }).$promise.then(function (post) {
                    return post;
                });
            }
        }
    })
    .when('/posts/:id/:friendlyUrl', {
        templateUrl: '/app/views/postview.html',
        controller: 'PostDetailsController',
        resolve: {
            post: function ($route, $location, Post) {
                return Post.get({ id: $route.current.params.id }).$promise.then(function (post) {
                    if ($route.current.params.friendlyUrl !== post.friendlyUrl) {
                        $location.path('/posts/' + post.id + '/' + post.friendlyUrl).replace();
                    }

                    return post;
                });
            }
        }
    })
    .when('/search/:term', {
        templateUrl: '/app/views/postlistview.html',
        controller: 'PostListviewController',
        resolve: {
            posts: function ($route, Post) {
                return Post.search({ term: $route.current.params.term }).$promise.then(function (posts) {
                    return posts;
                });
            },
            listTitle : function($route)
            {
                return 'Posts found by search term \'' + $route.current.params.term + '\'';
            }
        }
    })
    .when('/tag/:tag', {
        templateUrl: '/app/views/postlistview.html',
        controller: 'PostListviewController',
        resolve: {
            posts: function ($route, Post) {
                return Post.getByTag({ tag: $route.current.params.tag }).$promise.then(function (posts) {
                    return posts;
                });
            },
            listTitle: function ($route) {
                return 'Posts found by tag \'' + $route.current.params.tag + '\'';
            }
        }
    })
    .when('/archive', {
        templateUrl: '/app/views/archiveview.html',
        redirectTo: function () {
            return '/archive/' + new Date().getFullYear() + '/' + (new Date().getMonth() + 1);
        }
    })
    .when('/archive/:year/:month', {
        templateUrl: '/app/views/archiveview.html',
        controller: 'ArchiveController',
        resolve: {
            posts: function ($route, Post) {
                return Post.getArchive({ year: $route.current.params.year, month: $route.current.params.month }).$promise.then(function (posts) {
                    return posts;
                });
            }
        }
    })
    .when('/blogs/:id/posts', {
        templateUrl: '/app/views/postlistview.html',
        controller: 'PostListviewController',
        resolve: {
            posts: function ($route, Post) {
                return Post.getByBlog({ blogID: $route.current.params.id }).$promise.then(function (posts) {
                    return posts;
                });
            },
            listTitle: function()
            {
                return 'Posts';
            }
        }
    })
    .when('/blogs/:id/posts/add', {
        templateUrl: '/app/views/postaddview.html',
        controller: 'PostAddController',
        resolve: {
            blog: function ($route, Blog) {
                return Blog.get({ id: $route.current.params.id }).$promise.then(function (blog) {
                    return blog;
                });
            }
        }
    })
    .when('/blogs/add', {
        templateUrl: '/app/views/blogaddview.html',
        controller: 'BlogAddController',
        resolve: {
        }
    })
    .when('/blogs/:id/', {
        templateUrl: '/app/views/blogview.html',
        controller: 'BlogDetailsController',
        resolve: {
            blog: function ($route, Blog) {
                return Blog.get({ id: $route.current.params.id }).$promise.then(function (blog) {
                    return blog;
                });
            },
            posts: function ($route, Post) {
                return Post.getByBlog({ blogID: $route.current.params.id }).$promise.then(function (posts) {
                    return posts;
                });
            }
        }
    })
    .when('/blogs/:id/edit', {
        templateUrl: '/app/views/blogeditview.html',
        controller: 'BlogDetailsController',
        resolve: {
            blog: function ($route, Blog) {
                return Blog.get({ id: $route.current.params.id }).$promise.then(function (blog) {
                    return blog;
                });
            },
            posts: function()
            {
                return {};
            }
        }
    })
    .when('/users/:username', {
        templateUrl: '/app/views/userview.html',
        controller: 'UserDetailsController',
        resolve: {
            user: function ($route, User) {
                return User.get({ username: $route.current.params.username }).$promise.then(function (user) {
                    return user;
                });
            },
            posts: function ($route, Post) {
                return Post.getByUser({ username: $route.current.params.username }).$promise.then(function (posts) {
                    return posts;
                });
            }
        }
    })
    .when('/users/:username/blogs', {
        templateUrl: '/app/views/bloglistview.html',
        controller: 'BlogListviewController',
        resolve: {
            blogs: function ($route, Blog) {
                return Blog.getByUser({ username: $route.current.params.username }).$promise.then(function (blogs) {
                    return blogs;
                });
            },
            listTitle: function($route)
            {
                return 'Blogs by ' + $route.current.params.username;
            }
        }
    });
});