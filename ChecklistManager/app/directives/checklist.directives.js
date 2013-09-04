function sortedDirective() {
    return {
        scope: true,
        transclude: true,
        template: '<a ng-click="do_sort()" ng-transclude></a>' +
                '<span ng-show="do_show(true)"><i class="icon-circle-arrow-down"></i></span>' +
                '<span ng-show="do_show(false)"><i class="icon-circle-arrow-up"></i></span>',
        controller: function ($scope, $element, $attrs) {
            $scope.sort = $attrs.sorted;

            $scope.do_sort = function () {
                $scope.sort_by($scope.sort);
            };

            $scope.do_show = function (asc) {
                return (asc !== $scope.sort_desc) && ($scope.sort_order === $scope.sort);
            };
        }
    };
};

function focusDirective($parse) {
    return function (scope, element, attr) {
        var fn = $parse(attr['cngFocus']);
        element.bind('focus', function (event) {
            scope.$apply(function () {
                fn(scope, { $event: event });
            });
        });
    }
};

function blurDirective($parse) {
    return function (scope, element, attr) {
        var fn = $parse(attr['cngBlur']);
        element.bind('blur', function (event) {
            scope.$apply(function () {
                fn(scope, { $event: event });
            });
        });
    }
};