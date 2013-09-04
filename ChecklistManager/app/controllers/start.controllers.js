var StartCtrl = function ($scope, $location) {

};

function FilterCtrl($scope) {
    $scope.toRange = function (start, count) {
        return _.range(start, count + 1)
    };

    function getODataFilter(filters) {
        var odataFilter = {
            $top: filters.pageSize,
            $skip: (filters.page - 1) * filters.pageSize,
            $filter: filters.filter,
            $orderby: filters.orderby,
            $inlinecount: 'allpages'
        };
        _.extend(odataFilter, filters.args)
        return odataFilter;
    }

    function updateTotalPages(totalItemsCount) {
        $scope.filters.totalPages = totalItemsCount / $scope.filters.pageSize + 0.5;
    }

    $scope.filters = {
        page: 1,
        pageSize: 2,
        totalPages: 0,
        orderby: 'Title',
        args: {}
    };

    $scope.$watch('filters.query', function (query) {
        $scope.filters.filter = query ? "substringof('" + query + "', Title)" : undefined;
    });

    $scope.toPage = function (page) {
        $scope.filters.page = page;
        $scope.getData();
    };

    $scope.previousPage = function () {
        if ($scope.filters.page > 1) {
            $scope.filters.page--;
            $scope.getData();
        }
    }

    $scope.nextPage = function () {
        if ($scope.filters.page < $scope.filters.totalPages) {
            $scope.filters.page++;
            $scope.getData();
        }
    }

    $scope.sort_by = function (ord) {
        if ($scope.sort_order === ord) {
            $scope.sort_desc = !$scope.sort_desc;
        }
        else {
            $scope.sort_desc = false;
        }
        $scope.sort_order = ord;
        $scope.filters.orderby = ord + ($scope.sort_desc ? ' desc' : '');
        $scope.getData();
    };

    return {
        getODataFilter: getODataFilter,
        updateTotalPages: updateTotalPages
    };
}

function ChecklistBase($scope) {
    $scope.itemStateToggled = function (itemId) {
        $scope.completedItems = calculateCompletedCount($scope.checklistItems);
    };
    function calculateCompletedCount(items) {
        return _.filter(items, function (x) { return x.IsDone; }).length;
    };
    return {
        calculateCompletedCount: calculateCompletedCount
    };
}