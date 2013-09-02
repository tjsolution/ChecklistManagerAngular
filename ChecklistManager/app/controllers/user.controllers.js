function UserListCtrl($scope, $location, userResource, userService) {

    $scope.items = userResource.query({ organisation: userService.organisation });

    $scope.sort_by = function (ord) {
        if ($scope.sort_order === ord) {
            $scope.sort_desc = !$scope.sort_desc;
        }
        else {
            $scope.sort_desc = false;
        }
        $scope.sort_order = ord;
    };

    $scope.sort_order = 'OrganisationName';
    $scope.sort_desc = true;
}

function CreateUserCtrl($scope, $routeParams, $location, userResource, userService) {
    userResource.query({ organisation: userService.organisation }, function (users) {
        $scope.managers = users;
    });
    
    $scope.selectedManager = userService.username;
    $scope.select = function (text, value) {
        $scope.selectedManager = value;
        angular.element('#managerText').html(text);
    };

    $scope.save = function () {
        $scope.User = {
            ManagerUsername: $scope.selectedManager,
            OrganisationName: userService.organisation
        };
        userResource.save($scope.User, function () {
            $location.path('/users');
        });
    };
}

function EditUserCtrl($scope, $routeParams, $location, userResource, userService) {
    $scope.isEdit = true;
    $scope.User = userResource.get({ username: $routeParams.username });

    userResource.query({ organisation: userService.organisation }, function (users) {
        users.splice(0, 0, { Name: 'None', Username: null });
        $scope.managers = users;
    });
    if ($scope.User.Manager) {
        angular.element('#managerText').html($scope.User.Manager.Name);
    }
    else {
        angular.element('#managerText').html('None');
    }

    $scope.save = function () {
        userResource.update({ username: $scope.username }, $scope.item, function () {
            $location.path('/users');
        });
    };

    $scope.select = function (text, value) {
        $scope.User.ManagerUsername = value;
        angular.element('#managerText').html(text);
    };
}
