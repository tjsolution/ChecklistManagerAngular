var checklistApp = angular.module("checklistApp", ["ngResource"]).
    config(function ($routeProvider) {
        $routeProvider
            .when('/', { controller: ListCtrl, templateUrl: 'htmlView/checklist.html' })
            .when('/new', { controller: CreateCtrl, templateUrl: 'htmlView/checklistDetails.html' })
            .when('/edit/:itemId', { controller: EditCtrl, templateUrl: 'htmlView/checklistDetails.html' })
            .otherwise({ redirectTo: '/' });
    });

checklistApp.factory('checklistTemplates', function ($resource) {
    return $resource('/api/checklistTemplate/:id', { id: '@id' }, { update: { method: 'PUT' } });
});

checklistApp.directive('sorted', function () {
    return {
        scope: true,
        transclude: true,
        template: '<a ng-click="do_sort()" ng-transclude></a>' +
                '<span ng-show="do_show(true)"><i class="icon-circle-arrow-down"></i></span>' +
                '<span ng-show="do_show(false)"><i class="icon-circle-arrow-up"></i></span>',
        controller: function ($scope, $element, $attrs) {
            $scope.sort = $attrs.sorted;

            $scope.do_sort = function () { $scope.sort_by($scope.sort); };

            $scope.do_show = function (asc) {
                return (asc != $scope.sort_desc) && ($scope.sort_order == $scope.sort);
            };
        }
    }
});

checklistApp.directive('inlineEdit', function () {

    return function (scope, element, attrs) {
        element.bind('click', function () {
            element.toggleClass('inactive');
            if (element.hasClass('inactive')) {
                $(element).blur();
            }
        });
    };

});

var ListCtrl = function ($scope, $location, checklistTemplates) {
    $scope.reset = function () {
        $scope.offset = 0;
        $scope.items = [];
        $scope.search();
    };

    $scope.search = function () {
        checklistTemplates.query({ q: $scope.query, sort: $scope.sort_order, desc: $scope.sort_desc, limit: $scope.limit, offset: $scope.offset },
            function (items) {
                var cnt = items.length;
                $scope.no_more = cnt < $scope.limit;
                $scope.items = $scope.items.concat(items);
            }
        );
    };

    $scope.sort_by = function (ord) {
        if ($scope.sort_order == ord) { $scope.sort_desc = !$scope.sort_desc; }
        else { $scope.sort_desc = false; }
        $scope.sort_order = ord;
        $scope.reset();
    };

    $scope.show_more = function () { return !$scope.no_more; };

    $scope.limit = 20;

    $scope.sort_order = 'UserId';
    $scope.sort_desc = true;

    $scope.reset();
};

var CreateCtrl = function ($scope, $routeParams, $location, checklistTemplates) {

    $scope.save = function () {
        checklistTemplates.save($scope.item, function () {
            $location.path('/');
        });
    };
};

var EditCtrl = function ($scope, $routeParams, $location, checklistTemplates) {
    $scope.item = checklistTemplates.get({ id: $routeParams.itemId });

    $scope.save = function () {
        checklistTemplates.update({ id: $scope.item.TodoItemId }, $scope.item, function () {
            $location.path('/');
        });
    };
};