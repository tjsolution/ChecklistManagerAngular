function ChecklistBase($scope) {
    $scope.getCompletedCount = function () {
        return _.filter($scope.checklistItems, function (x) { return x.IsDone; }).length;
    };
    $scope.calculateCompletedCount = function (items) {
        return _.filter(items, function (x) { return x.IsDone; }).length;
    };
    $scope.itemStateToggled = function (itemId) {
        $scope.completedItems = $scope.getCompletedCount();
    };
}

function ViewChecklistsCtrl($scope, $location, checklistResource, userService) {
    ViewChecklistCtrl.prototype = new ChecklistBase($scope);

    checklistResource.query({ organisation: userService.organisation }, function (lists) {
        _.each(lists, function (list) {
            list.completedItems = $scope.calculateCompletedCount(list.Items)
        });
        $scope.checklists = lists;
    });
    $scope.remove = function (id) {
        checklistResource.remove({ id: id }, function () {
            angular.element("#item_" + id).remove();
        });
    };
};

var CompleteChecklistCtrl = function ($scope, $location, checklistTemplates, userService) {
    $scope.templates = checklistTemplates.query({ organisation: userService.organisation });
    $scope.completeChecklist = function (templateId) {
        $location.path('/checklists/new/' + templateId);
    };
};

function CreateChecklistCtrl($scope, $routeParams, $location, checklistResource) {
    CreateChecklistCtrl.prototype = new ChecklistBase($scope);

    checklistResource.create({ templateId: $routeParams.templateId }, function (checklist) {
        $scope.checklist = checklist;
        $scope.checklistItems = checklist.Items;
        $scope.completedItems = $scope.getCompletedCount();
    });

    $scope.save = function () {
        checklistResource.save($scope.checklist, function (checklist) {
            $location.path('/checklists');
        });
    };
}

function ViewChecklistCtrl($scope, $routeParams, $location, checklistResource, checkItemResource) {
    ViewChecklistCtrl.prototype = new ChecklistBase($scope);

    checklistResource.get({ id: $routeParams.id }, function (checklist) {
        $scope.checklist = checklist;
        $scope.checklistItems = checkItemResource.query({ checklistId: checklist.Id }, function (items) {
            $scope.checklist.Items = items;
            $scope.completedItems = $scope.getCompletedCount();
        });   
    });    

    $scope.save = function () {
        checklistResource.update({ id: $scope.checklist.Id}, $scope.checklist, function (checklist) {
            $location.path('/checklists');
        });
    };
}