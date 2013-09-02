var ChecklistTemplatesCtrl = function ($scope, $location, checklistTemplates, userService) {
    $scope.templateList = {};

    $scope.reset = function () {
        $scope.offset = 0;
        $scope.templates = [];
        if ($scope.templateList) {
            $scope.templateList.query = null;
        }
        $scope.search();
    };

    $scope.search = function () {
        checklistTemplates.query(
            {
                q: $scope.templateList.query,
                sort: $scope.sort_order,
                desc: $scope.sort_desc,
                limit: $scope.limit,
                offset: $scope.offset
            },
            function (templates) {
                var cnt = templates.length;
                $scope.no_more = cnt < $scope.limit;
                $scope.templates = templates;
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

    $scope.createTemplate = function () {
        var checklist = {
            Title: $scope.templateList.name,
            ManagerUsername: userService.username
        };
        checklistTemplates.save(checklist, function (savedTemplate) {
            $scope.templates.splice(0, 0, savedTemplate);
        });
    };

    $scope.remove = function (itemId) {
        checklistTemplates.remove({ id: itemId }, function () {
            $("#item_" + itemId).fadeOut();
        });
    };

    $scope.limit = 20;

    $scope.sort_order = 'ManagerUsername';
    $scope.sort_desc = true;

    $scope.reset();
};

function EditChecklistCtrl($scope, $routeParams, $location,
                            checklistTemplates, checkItemTemplates, userResource, userService) {
    $scope.template = checklistTemplates.get({ id: $routeParams.itemId }, function (item) {
        userResource.get({ username: item.ManagerUsername }, function (manager) {
            $scope.managerName = manager ? manager.Name : 'None';
        });
    });
    
    $scope.templateItems = checkItemTemplates.query({ templateId: $routeParams.itemId });
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
    $scope.remove = function (itemId) {
        checkItemTemplates.remove({ id: itemId }, function () {
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