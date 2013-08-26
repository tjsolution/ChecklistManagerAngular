var ChecklistTemplatesCtrl = function ($scope, $location, checklistTemplates, userService) {
    $scope.template = {};

    $scope.reset = function () {
        $scope.offset = 0;
        $scope.items = [];
        if ($scope.template) {
            $scope.template.query = null;
        }
        $scope.search();
    };

    $scope.search = function () {
        checklistTemplates.query(
            {
                q: $scope.template.query,
                sort: $scope.sort_order,
                desc: $scope.sort_desc,
                limit: $scope.limit,
                offset: $scope.offset
            },
            function (items) {
                var cnt = items.length;
                $scope.no_more = cnt < $scope.limit;
                $scope.items = items;
            }
        );
    };

    $scope.sort_by = function (ord) {
        if ($scope.sort_order === ord) {
            $scope.sort_desc = !$scope.sort_desc;
        }
        else {
            $scope.sort_desc = false;
        }
        $scope.sort_order = ord;
        $scope.reset();
    };

    $scope.show_more = function () {
        return !$scope.no_more;
    };

    $scope.newTemplate = function () {
        var checklist = {
            Title: $scope.template.newTemplateName,
            ManagerUsername: userService.username
        };
        checklistTemplates.save(checklist, function (savedItem) {
            $scope.items.splice(0, 0, savedItem);
        });
    };

    $scope.delete = function (itemId) {
        checklistTemplates.delete({ id: itemId }, function () {
            $("#item_" + itemId).fadeOut();
        });
    };

    $scope.limit = 20;

    $scope.sort_order = 'ManagerUsername';
    $scope.sort_desc = true;

    $scope.reset();
};

function EditChecklistCtrl($scope, $routeParams, $location, checklistTemplates, checklistTemplateItems, checkItemTemplates, userResource, userService) {
    $scope.template = checklistTemplates.get({ id: $routeParams.itemId }, function (item) {
        userResource.get({ username: item.ManagerUsername }, function (manager) {
            $scope.managerName = manager ? manager.Name : 'None';
        });
    });
    
    $scope.templateItems = checklistTemplateItems.query({ templateId: $routeParams.itemId });
    $scope.managers = userResource.query({ organisation: userService.organisation });

    $scope.save = function () {
        checklistTemplates.update({ id: $scope.template.Id }, $scope.item, function () {
            $location.path('/checklist-templates');
        });
    };
    $scope.newItem = function () {
        var checkItem = {
            Title: $scope.item.newItemName,
            ChecklistTemplateId: $scope.template.Id
        };
        checkItemTemplates.save(checkItem, function (savedItem) {
            $scope.item.newItemName = null;
            $scope.templateItems.splice(0, 0, savedItem);
        });
    };
    $scope.delete = function (itemId) {
        checkItemTemplates.delete({ id: itemId }, function () {
            $("#item_" + itemId).fadeOut();
        });
    };
    $scope.select = function (text, value) {
        $scope.template.ManagerUsername = value;
        $scope.managerName = text;
    };
}

function EditCheckItemCtrl($scope, $routeParams, $location, checkItemTemplates) {
    $scope.templateItem = checkItemTemplates.get({ id: $routeParams.itemId });

    $scope.save = function () {
        checkItemTemplates.update({ id: $scope.templateItem.Id }, $scope.templateItem, function () {
            $location.path('/checklist-templates/edit/' + $scope.templateItem.ChecklistTemplateId);
        });
    };
}