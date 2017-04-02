app.factory('User', function ($resource) {
    return $resource('/api/users/:username', { username: '@username' }, {
        update: {
            method: 'PUT'
        }
    });
});