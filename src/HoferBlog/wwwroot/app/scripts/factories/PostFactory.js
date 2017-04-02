app.factory('Post', function ($resource) {
    return $resource('/api/posts/:id', { id: '@id' }, {
        update: {
            method: 'PUT'
        },
        search: {
            method: 'GET',
            url: '/api/search/:term',
            isArray: true
        },
        getArchive: {
            method: 'GET',
            url: '/api/archive/:year/:month',
            isArray: true
        },
        getByTag: {
            method: 'GET',
            url: '/api/tag/:tag',
            isArray: true
        },
        getByBlog: {
            method: 'GET',
            url: '/api/blogs/:blogID/posts',
            isArray: true
        },
        getByUser: {
            method: 'GET',
            url: '/api/users/:username/posts',
            isArray: true
        },
        getLatest: {
            method: 'GET',
            url: '/api/posts/latest',
            isArray: true
        }
    });
});