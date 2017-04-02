app.factory('Blog', function ($resource) {
    return $resource('/api/blogs/:id', { id: '@id' }, {
        update: {
            method: 'PUT'
        },
        getByUser: {
            method: 'GET',
            url: '/api/users/:username/blogs',
            isArray: true
        }
    });
});