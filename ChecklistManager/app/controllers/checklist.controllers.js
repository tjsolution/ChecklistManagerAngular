function ViewChecklistsCtrl($scope, $location, checklistResource, userService) {
    var ext = new ChecklistBase($scope);
    var filterExt = new FilterCtrl($scope);

    $scope.filters.args.organisation = userService.organisation;

    $scope.getData = function () {
        checklistResource.odata(filterExt.getODataFilter($scope.filters), function (pageResult) {
            filterExt.updateTotalPages(pageResult.Count);
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

    $scope.getData();
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