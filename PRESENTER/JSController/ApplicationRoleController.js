var app = angular.module('ApplicationRole', []);
app.controller('ApplicationRoleController', function ($scope, $http, $compile, $timeout) {

    $scope.DEFAULT_SELECTED = {
        "APP_ID": 0,
        "APP_NAME": "Selected Application",
        "APP_URL": null,
        "APP_DESCRIPTION": null,
        "APP_STATUS": null,
        "APP_CODE": null
    }

    $scope.APP_ID
    $scope.APP_NAME
    $scope.LIST_COMBO_APPLICATION
    $scope.LIST_APPLICATION_ROLE
    $scope.APP_SELECTED_ID
    $scope.ROLE_ID


    $scope._ROLE_NAME
    $scope._ROLE_DESCRIPTION

    $scope.GetComboApplication = function () {
        $http({
            url: "/applicationmenu/GetComboApplication",
            method: "GET",
            params: {
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $scope.LIST_COMBO_APPLICATION = response.data.OUTPUT_DATA
                $scope.LIST_COMBO_APPLICATION.unshift($scope.DEFAULT_SELECTED)
                $scope.APPLICATION = $scope.LIST_COMBO_APPLICATION[0]
                $scope.APP_ID = $scope.APPLICATION.APP_ID
            }
        });
    }

    $scope.ComboApplicationChange = function () {
        $scope.APP_SELECTED_ID = $scope.APPLICATION.APP_ID
        $scope.APP_NAME = $scope.APPLICATION.APP_NAME
        $scope.GetListApplicationRole()
    }

    $scope.GetListApplicationRole = function () {

        $http({
            url: "/ApplicationRole/GetListApplicationRole",
            method: "GET",
            params: {
                application_id: $scope.APP_SELECTED_ID
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                var OUTPUT_DATA = response.data.OUTPUT_DATA;
                $scope.LIST_APPLICATION_ROLE = OUTPUT_DATA.LIST_ROLE_MODEL;
            }
        }).then(function (response) {
            $scope.RenderTable();
        })
        .then(function (response) {
            $('#tableApplicationRole').DataTable(
          {
              destroy: true,
              searching: false
          });
        })

    }

    $scope.RenderTable = function () {
        $('#tableApplicationRole').empty();
        html = "  <thead>\
                    <tr>\
                        <th>Role Name</th>\
                        <th>Role Description</th>\
                        <th>Manage</th>\
                    </tr>\
                </thead>\
                "
        var lenght = Object.keys($scope.LIST_APPLICATION_ROLE).length;

        for (var i = 0; i < lenght; i++) {
            html += "<tr>\
                        <td>" + $scope.LIST_APPLICATION_ROLE[i].ROLE_NAME + "</td>\
                        <td>" + $scope.LIST_APPLICATION_ROLE[i].ROLE_DESCRIPTION + "</td>\
                        <th>\
                        <button class ='btn btn-primary' ng-click='ViewApplicationRole(" + $scope.LIST_APPLICATION_ROLE[i].ROLE_ID + ")'>View</button>\
                        <button class ='btn btn-warning' ng-click='UpdateApplicationRole(" + $scope.LIST_APPLICATION_ROLE[i].ROLE_ID + ")'>Edit</button>\
                        <button class ='btn btn-danger'ng-click='DeleteApplicationRole(" + $scope.LIST_APPLICATION_ROLE[i].ROLE_ID + ")'>Delete</button>\
                        </th>\
                    </tr>\
                    "
        }

        var $OnCompile = $(html).appendTo('#tableApplicationRole');
        $compile($OnCompile)($scope);
    }

    $scope.AddApplicationRole = function () {
        $('#modalAddRole').modal('toggle')
    }

    $scope.AddApplicationRoleConfirm = function () {
        $http({
            url: "/ApplicationRole/AddApplicationRole",
            method: "GET",
            params: {

                ROLE_NAME: $scope._ROLE_NAME,
                ROLE_DESCRIPTION: $scope._ROLE_DESCRIPTION,
                APP_ID: $scope.APP_SELECTED_ID,

            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $('#modalAddRole').modal('hide')
                $scope.GetListApplicationRole()
                $scope._ROLE_NAME = ""
                $scope._ROLE_DESCRIPTION = ""
                $('#modalSaveSuccess').modal('show')
            }
        })
    }

    $scope.GetApplicationRole = function (role_id) {

        $http({
            url: "/ApplicationRole/GetApplicationRole",
            method: "GET",
            params: {
                role_id: role_id
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {

                $scope._ROLE_NAME = response.data.OUTPUT_DATA.ROLE_NAME
                $scope._ROLE_DESCRIPTION = response.data.OUTPUT_DATA.ROLE_DESCRIPTION
                $scope.ROLE_ID = role_id

            }
        })




    }

    $scope.ViewApplicationRole = function (role_id) {

        $scope.GetApplicationRole(role_id)
        $("#modalViewRole").modal("show")
    }

    $scope.UpdateApplicationRole = function (role_id) {
        $scope.GetApplicationRole(role_id)
        $('#modalUpdateRole').modal('toggle')
    }

    $scope.UpdateApplicationRoleConfirm = function () {
        $http({
            url: "/ApplicationRole/UpdateApplicaionRole",
            method: "GET",
            params: {

                ROLE_NAME: $scope._ROLE_NAME,
                ROLE_DESCRIPTION: $scope._ROLE_DESCRIPTION,
                APP_ID: $scope.APP_SELECTED_ID,
                ROLE_ID: $scope.ROLE_ID
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $('#modalUpdateRole').modal('hide')
                $scope.GetListApplicationRole()
                $('#modalSaveSuccess').modal('show')
            }
        })
    }

    $scope.DeleteApplicationRole = function (role_id) {

        $scope.GetApplicationRole(role_id)
        $('#modalDeleteRole').modal('toggle')

    }

    $scope.DeleteApplicationRoleConfirm = function () {
        debugger;

        $http({
            url: "/ApplicationRole/DeleteApplicationRole",
            method: "GET",
            params: {
                role_id: $scope.ROLE_ID
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $('#modalDeleteRole').modal('hide')
                $scope.GetListApplicationRole()
                $('#modalSaveSuccess').modal('show')
            }
        })
    }

});