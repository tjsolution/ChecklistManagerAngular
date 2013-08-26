function ChecklistTemplateFactory($resource) {
    return $resource('/api/checklistTemplate/:id', { id: '@id' }, { update: { method: 'PUT' } });
}
function ChecklistTemplateItemsFactory($resource) {
    return $resource('/api/checklistTemplateItems');
}
function CheckItemTemplateFactory($resource) {
    return $resource('/api/checkItemTemplate/:id', { id: '@id' }, { update: { method: 'PUT' } });
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