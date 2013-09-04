var ChecklistTemplatesCtrl = function ($scope, $location, checklistTemplates, userService) {
    var filterExt = new FilterCtrl($scope);
    $scope.templateList = {};
    $scope.filters.args.organisation = userService.organisation;

    $scope.getData = function () {
        checklistTemplates.odata(filterExt.getODataFilter($scope.filters),
            function (pageResult) {
                filterExt.updateTotalPages(pageResult.Count);
                $scope.templates = pageResult.Items;
            }
        );
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

    $scope.getData();
};

function EditTemplateCtrl($scope, $routeParams, $location,
                            checklistTemplates, checkItemTemplates, userResource, userService) {

    $scope.template = checklistTemplates.get({ id: $routeParams.id }, function (item) {
        userResource.get({ username: item.ManagerUsername }, function (manager) {
            $scope.managerName = manager ? manager.Name : 'None';
        });
    });
    
    $scope.templateItems = checkItemTemplates.query({ templateId: $routeParams.id });
    $scope.managers = userResource.query({ organisation: userService.organisation });

    $scope.save = function () {
        checklistTemplates.update({ id: $scope.template.Id }, $scope.template, function () {
            $location.path('/checklist-templates');
        });
    };
    $scope.newItem = function () {
        var templateItem = {
            Title: $scope.item.newItemName,
            ChecklistTemplateId: $scope.template.Id
        };
        checkItemTemplates.save(templateItem, function (savedItem) {
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

var SelectTemplateCtrl = function ($scope, $location, checklistTemplates, userService) {
    var filterExt = new FilterCtrl($scope);
    $scope.filters.args.organisation = userService.organisation;

    $scope.getData = function () {
        checklistTemplates.odata(filterExt.getODataFilter($scope.filters),
            function (pageResult) {
                filterExt.updateTotalPages(pageResult.Count);
                $scope.templates = pageResult.Items;
            }
        );
    };

    $scope.selectTemplate = function (templateId) {
        $location.path('/checklists/new/' + templateId);
    };

    $scope.getData();
};