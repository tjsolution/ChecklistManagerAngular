/*global */

var checklistApp = angular.module("checklistApp", ["ngResource", 'ui.bootstrap']);

checklistApp.factory('myHttpInterceptor', MyHttpInterceptor);

checklistApp.config(function ($routeProvider, $httpProvider) {
    $httpProvider.interceptors.push('myHttpInterceptor');

    $routeProvider
        .when('/', { controller: StartCtrl, templateUrl: 'views/start.html' })

        .when('/users', { controller: UserListCtrl, templateUrl: 'views/users.html' })
        .when('/users/new', { controller: CreateUserCtrl, templateUrl: 'views/userDetails.html' })
        .when('/users/edit/:username', { controller: EditUserCtrl, templateUrl: 'views/userDetails.html' })

        .when('/checklist-templates', { controller: ChecklistTemplatesCtrl, templateUrl: 'views/checklistTemplates.html' })
        .when('/checklist-templates/edit/:itemId', { controller: EditChecklistCtrl, templateUrl: 'views/checklistTemplateDetails.html' })
        .when('/checklist-templates/item/edit/:itemId', { controller: EditCheckItemCtrl, templateUrl: 'views/checkItemTemplateDetails.html' })

        .when('/checklists/complete', { controller: CompleteChecklistCtrl, templateUrl: 'views/checklistItemDetails.html' })

        .otherwise({ redirectTo: '/' });
});

checklistApp.factory('checklistTemplates', ChecklistTemplateFactory);
checklistApp.factory('checklistTemplateItems', ChecklistTemplateItemsFactory);
checklistApp.factory('checkItemTemplates', CheckItemTemplateFactory);
checklistApp.factory('userResource', UserFactory);
checklistApp.factory('userService', UserService);

checklistApp.directive('sorted', sortedDirective);
checklistApp.directive('cngFocus', ['$parse', focusDirective]);
checklistApp.directive('cngBlur', ['$parse', blurDirective]);