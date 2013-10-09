/*global */

var checklistApp = angular.module('checklistApp', ['ngResource', 'ngRoute' /*, 'ui.bootstrap'*/]);

checklistApp.factory('myHttpInterceptor', MyHttpInterceptor);

checklistApp.config(function ($routeProvider, $httpProvider) {
    $httpProvider.interceptors.push('myHttpInterceptor');

    $routeProvider
        .when('/', { controller: StartCtrl, templateUrl: 'app/views/start.html' })
        .when('/login', { controller: LoginCtrl, templateUrl: 'app/views/login.html' })
        .when('/users', { controller: UserListCtrl, templateUrl: 'app/views/users.html' })
        .when('/users/new', { controller: CreateUserCtrl, templateUrl: 'app/views/userDetails.html' })
        .when('/users/edit/:username', { controller: EditUserCtrl, templateUrl: 'app/views/userDetails.html' })
        .when('/checklist-definitions', { controller: ChecklistDefinitionsCtrl, templateUrl: 'app/views/checklistDefinitions.html' })
        .when('/checklist-definitions/edit/:id', { controller: EditDefinitionCtrl, templateUrl: 'app/views/checklistDefinitionDetails.html' })
        .when('/checklist-definitions/item/edit/:itemId', { controller: EditCheckItemCtrl, templateUrl: 'app/views/itemDefinitionDetails.html' })
        .when('/checklists', { controller: ViewChecklistsCtrl, templateUrl: 'app/views/checklists.html' })
        .when('/checklist-definitions/select', { controller: SelectDefinitionCtrl, templateUrl: 'app/views/selectDefinition.html' })
        .when('/checklists/new/:definitionId', { controller: CreateChecklistCtrl, templateUrl: 'app/views/checklistDetails.html' })
        .when('/checklists/edit/:id', { controller: ViewChecklistCtrl, templateUrl: 'app/views/checklistDetails.html' })
        .otherwise({ redirectTo: '/' });
});

checklistApp.factory('checklistDefinitions', ChecklistDefinitionFactory);
checklistApp.factory('checkItemDefinitions', CheckItemDefinitionFactory);
checklistApp.factory('checklistResource', ChecklistFactory);
checklistApp.factory('checkItemResource', CheckItemFactory);
checklistApp.factory('userResource', UserFactory);
checklistApp.factory('userService', UserService);

checklistApp.directive('sorted', sortedDirective);
checklistApp.directive('cngFocus', ['$parse', focusDirective]);
checklistApp.directive('cngBlur', ['$parse', blurDirective]);
checklistApp.directive('cngEnter', enterDirective);