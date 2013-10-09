function ChecklistDefinitionFactory($resource) {
    return $resource('/api/checklistDefinition/:id', { id: '@id' }, {
        update: { method: 'PUT' },
        odata: { method: 'GET' }
    });
}
function CheckItemDefinitionFactory($resource) {
    return $resource('/api/checkItemDefinition/:id', { id: '@id' }, { update: { method: 'PUT' } });
}
function ChecklistFactory($resource) {
    return $resource('/api/checklist/:id', { id: '@id' }, {
        update: { method: 'PUT' },
        create: { method: 'GET', params: { definitionId: '@definitionId' } },
        odata: { method: 'GET' }
    });
}
function CheckItemFactory($resource) {
    return $resource('/api/checkItem/:id', { id: '@id' }, { update: { method: 'PUT' } });
}
function UserFactory($resource) {
    return $resource('/api/user/:id', { id: '@id' }, { update: { method: 'PUT' } });
}

function UserService() {
    return {
        isLoggedIn: true,
        isAdmin: true,
        username: 'tajed@outlook.com',
        name: 'Tallis Jed',
        organisation: '23 Software'
    };
}