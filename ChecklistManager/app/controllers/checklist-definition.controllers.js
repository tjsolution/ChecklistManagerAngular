var ChecklistDefinitionsCtrl = function ($scope, $location, checklistDefinitions, userService) {
    var filterExt = new FilterCtrl($scope);
    $scope.definitionList = {};
    $scope.filters.args.organisation = userService.organisation;

    $scope.getData = function () {
        checklistDefinitions.odata(filterExt.getODataFilter($scope.filters),
            function (pageResult) {
                filterExt.updateTotalPages(pageResult.Count);
                $scope.definitions = pageResult.Items;
            }
        );
    };

    $scope.createDefinition = function () {
        var checklist = {
            Title: $scope.definitionList.name,
            ManagerUsername: userService.username
        };
        checklistDefinitions.save(checklist, function (savedDefinition) {
            $scope.definitions.splice(0, 0, savedDefinition);
        });
    };

    $scope.remove = function (itemId) {
        checklistDefinitions.remove({ id: itemId }, function () {
            $("#item_" + itemId).fadeOut();
        });
    };

    $scope.getData();
};

function EditDefinitionCtrl($scope, $routeParams, $location,
                            checklistDefinitions, checkItemDefinitions, userResource, userService) {

    $scope.definition = checklistDefinitions.get({ id: $routeParams.id }, function (item) {
        userResource.get({ username: item.ManagerUsername }, function (manager) {
            $scope.managerName = manager ? manager.Name : 'None';
        });
    });
    
    $scope.definitionItems = checkItemDefinitions.query({ definitionId: $routeParams.id });
    $scope.managers = userResource.query({ organisation: userService.organisation });

    $scope.save = function () {
        checklistDefinitions.update({ id: $scope.definition.Id }, $scope.definition, function () {
            $location.path('/checklist-definitions');
        });
    };
    $scope.newItem = function () {
        var definitionItem = {
            Title: $scope.item.newItemName,
            ChecklistDefinitionId: $scope.definition.Id
        };
        checkItemDefinitions.save(definitionItem, function (savedItem) {
            $scope.item.newItemName = null;
            $scope.definitionItems.splice(0, 0, savedItem);
        });
    };
    $scope.remove = function (itemId) {
        checkItemDefinitions.remove({ id: itemId }, function () {
            $("#item_" + itemId).fadeOut();
        });
    };
    $scope.select = function (text, value) {
        $scope.definition.ManagerUsername = value;
        $scope.managerName = text;
    };
}

function EditCheckItemCtrl($scope, $routeParams, $location, checkItemDefinitions) {
    $scope.definitionItem = checkItemDefinitions.get({ id: $routeParams.itemId });

    $scope.save = function () {
        checkItemDefinitions.update({ id: $scope.definitionItem.Id }, $scope.definitionItem, function () {
            $location.path('/checklist-definitions/edit/' + $scope.definitionItem.ChecklistDefinitionId);
        });
    };
}

var SelectDefinitionCtrl = function ($scope, $location, checklistDefinitions, userService) {
    var filterExt = new FilterCtrl($scope);
    $scope.filters.args.organisation = userService.organisation;

    $scope.getData = function () {
        checklistDefinitions.odata(filterExt.getODataFilter($scope.filters),
            function (pageResult) {
                filterExt.updateTotalPages(pageResult.Count);
                $scope.definitions = pageResult.Items;
            }
        );
    };

    $scope.selectDefinition = function (definitionId) {
        $location.path('/checklists/new/' + definitionId);
    };

    $scope.getData();
};