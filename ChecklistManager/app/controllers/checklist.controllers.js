function ChecklistBase($scope) {
    $scope.itemStateToggled = function (itemId) {
        $scope.completedItems = calculateCompletedCount($scope.checklistItems);
    };
    $scope.toRange = function (start, count) {
        return _.range(start, count + 1)
    };

    function calculateCompletedCount (items) {
        return _.filter(items, function (x) { return x.IsDone; }).length;
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
    return {
        getODataFilter: getODataFilter,
        calculateCompletedCount: calculateCompletedCount
    };
}

function ViewChecklistsCtrl($scope, $location, checklistResource, userService) {
    var ext = new ChecklistBase($scope);

    $scope.filters = {
        page: 1,
        pageSize: 2,
        totalPages: 0,
        orderby: 'RecordCreated',
        args: {organisation: userService.organisation}
    };

    $scope.$watch('filters.query', function (query) {
        $scope.filters.filter = query ? "substringof('" + query + "', Title)" : undefined;
    })


    $scope.getData = function () {
        checklistResource.odata(ext.getODataFilter($scope.filters), function (pageResult) {
            $scope.filters.totalPages = pageResult.Count / $scope.filters.pageSize + 0.5;
            _.each(pageResult.Items, function (list) {
                list.completedItems = ext.calculateCompletedCount(list.Items)
                list.allItemsChecked = (list.completedItems == list.Items.length);
            });
            $scope.checklists = pageResult.Items;
        });
    };

    $scope.remove = function (id) {
        checklistResource.remove({ id: id }, function () {
            angular.element("#list_" + id).remove();
        });
    };

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

    $scope.getData();
};

var CompleteChecklistCtrl = function ($scope, $location, checklistTemplates, userService) {
    $scope.templates = checklistTemplates.query({ organisation: userService.organisation });
    $scope.completeChecklist = function (templateId) {
        $location.path('/checklists/new/' + templateId);
    };
};

function CreateChecklistCtrl($scope, $routeParams, $location, checklistResource) {
    var ext = new ChecklistBase($scope);

    checklistResource.create({ templateId: $routeParams.templateId }, function (checklist) {
        $scope.checklist = checklist;
        $scope.checklistItems = checklist.Items;
        $scope.completedItems = ext.getCompletedCount(checklist.Items);
    });

    $scope.save = function () {
        checklistResource.save($scope.checklist, function (checklist) {
            $location.path('/checklists');
        });
    };
}

function ViewChecklistCtrl($scope, $routeParams, $location, checklistResource, checkItemResource) {
    var ext = new ChecklistBase($scope);

    checklistResource.get({ id: $routeParams.id }, function (checklist) {
        $scope.checklist = checklist;
        $scope.checklistItems = checkItemResource.query({ checklistId: checklist.Id }, function (items) {
            $scope.checklist.Items = items;
            $scope.completedItems = ext.getCompletedCount(items);
            $scope.allItemsChecked = ($scope.completedItems == items.length);
        });   
    });    

    $scope.save = function () {
        checklistResource.update({ id: $scope.checklist.Id}, $scope.checklist, function (checklist) {
            $location.path('/checklists');
        });
    };
}