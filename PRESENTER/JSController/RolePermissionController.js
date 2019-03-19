var app = angular.module('RolePermissionApplication', []);
app.controller('RolePermissionApplicationController', function ($scope, $http, $compile, $timeout) {

    $scope.DEFAULT_SELECTED = {
        "APP_ID": 0,
        "APP_NAME": "Selected Application",
        "APP_URL": null,
        "APP_DESCRIPTION": null,
        "APP_STATUS": null,
        "APP_CODE": null
    }

    $scope.ROLE_DEFAULT_SELECTED = {
        ROLE_ID: 0,
        APP_ID: 0,
        ROLE_NAME: "Selected Role",
        //ROLE_DESCRIPTION: "Admin"
    }


    $scope.APP_ID
    $scope.APP_NAME
    $scope.LIST_COMBO_APPLICATION
    $scope.LIST_APPLICATION_ROLE
    $scope.APP_SELECTED_ID
    $scope.ROLE_ID
    $scope.LIST_ROLE_PERMISSION

    $scope._ROLE_NAME
    $scope._ROLE_DESCRIPTION

    $scope.list_permission = [];

    var permission = {
        ROLE_PERMISSION_ID: 0,
        ROLE_ID: 0,
        MENU_ID: 0,
        MENU_LEVEL: 0,
        PARENT_ID: 0,
        MENU_NAME: "",
        GRANT_PERMISSION: "",
        VIEW_PERMISSION: "",
        INSERT_PERMISSION: "",
        UPDATE_PERMISSION: "",
        DELETE_PERMISSION: "",
    };



    function BindingObj() {

        return new Promise(function (done) {

            if ($('#chkGrant').is(':checked')) {
                permission.GRANT_PERMISSION = "Y"
            }
            else {
                permission.GRANT_PERMISSION = "N"
            }

            if ($('#chkView').is(':checked')) {
                permission.VIEW_PERMISSION = "Y"
            }
            else {
                permission.VIEW_PERMISSION = "N"
            }

            if ($('#chkInsert').is(':checked')) {
                permission.INSERT_PERMISSION = "Y"
            }
            else {
                permission.INSERT_PERMISSION = "N"
            }
            if ($('#chkUpdate').is(':checked')) {
                permission.UPDATE_PERMISSION = "Y"
            }
            else {
                permission.UPDATE_PERMISSION = "N"
            }
            if ($('#chkDelete').is(':checked')) {
                permission.DELETE_PERMISSION = "Y"
            }
            else {
                permission.DELETE_PERMISSION = "N"
            }
           
            permission.ROLE_ID = $scope.MODAL_ROLE_ID
            permission.MENU_ID = $scope.MODAL_MENU_ID

            $scope.list_permission.push(permission);
            debugger;
            done();
        })
    }

    function UpdateRolePermissionConfirm() {

        $http({
            url: "/RolePermission/UpdateRolePermission",
            method: "GET",
            params: {
                menu_id: $scope.MODAL_MENU_ID,
                role_id: $scope.MODAL_ROLE_ID,
                list_permission: JSON.stringify($scope.list_permission)
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                //modalRolePermission
                $('#modalRolePermission').modal('hide');
                $scope.ComboRoleChange();
                list_permission.clear();
            }
        })
    }

    $scope.GetComboApplication = function () {
        debugger;
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
        $scope.GetComboApplicationRole()
    }

    $scope.GetComboApplicationRole = function () {

        $http({
            url: "/applicationrole/GetListApplicationRole",
            method: "GET",
            params: {
                application_id: $scope.APP_SELECTED_ID
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                debugger;
                $scope.LIST_COMBO_ROLE = response.data.OUTPUT_DATA.LIST_ROLE_MODEL;
                $scope.LIST_COMBO_ROLE.unshift($scope.ROLE_DEFAULT_SELECTED)
                $scope.ROLE = $scope.LIST_COMBO_ROLE[0]
                $scope.ROLE_ID = $scope.ROLE.ROLE_ID
            }
        })

    }

    $scope.ComboRoleChange = function () {
        $http({
            url: "/RolePermission/GetRolePermissionByApplication",
            method: "GET",
            params: {
                application_id: $scope.APP_SELECTED_ID,
                role_id: $scope.ROLE.ROLE_ID
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $scope.LIST_ROLE_PERMISSION = response.data.OUTPUT_DATA.LIST_ROLE_PERMISSION_ALLOWED_MODEL
            }
        }).then(function () {
            debugger;
            $scope.RenderTable();
        }).then(function () {
            $scope.RenderFinish();
        })
    }

    $scope.UpdateRolePermission = function (menu_id, role_id) {
        $scope.MODAL_MENU_ID = menu_id;
        $scope.MODAL_ROLE_ID = role_id;
        $http({
            url: "/RolePermission/GetRolePermission",
            method: "GET",
            params: {
                menu_id: menu_id,
                role_id: role_id
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                debugger;
                $scope.ROLE_PERMISSION = response.data.OUTPUT_DATA;
            }
        }).then(function () {
            $scope.RenderModal();
        }).then(function () {
            $('#modalRolePermission').modal('show');
        })
    }

    $scope.UpdateRolePermissionConfirm = function () {
        BindingObj().then(function () {

            debugger;
            UpdateRolePermissionConfirm();
        })
    }

    $scope.IsGrantSelected = function () {
        if ($('#chkGrant').is(':checked')) {

        }
        else {
            $('#chkView').prop('checked', false);
            $('#chkInsert').prop('checked', false);
            $('#chkUpdate').prop('checked', false);
            $('#chkDelete').prop('checked', false);
        }
    }

    $scope.RenderModal = function () {
        if ($scope.ROLE_PERMISSION.MENU_LEVEL == 0) {
            $('#frmView').hide();
            $('#frmInsert').hide();
            $('#frmUpdate').hide();
            $('#frmDelete').hide();
            if ($scope.ROLE_PERMISSION.GRANT_PERMISSION == 'Y') {
                $('#chkGrant').prop('checked', true);
            }
            else {
                $('#chkGrant').prop('checked', false);
            }
        }
        else {
            $('#frmView').show();
            $('#frmInsert').show();
            $('#frmUpdate').show();
            $('#frmDelete').show();
            if ($scope.ROLE_PERMISSION.GRANT_PERMISSION == 'Y') {
                debugger;
                $('#chkGrant').prop('checked', true);
                $('#chkView').prop('disable', false);
                $('#chkInsert').prop('disable', false);
                $('#chkUpdate').prop('disable', false);
                $('#chkDelete').prop('disable', false);
            }
            else {
                $('#chkView').prop('disable', true);
                $('#chkInsert').prop('disable', true);
                $('#chkUpdate').prop('disable', true);
                $('#chkDelete').prop('disable', true);
            }
            if ($scope.ROLE_PERMISSION.VIEW_PERMISSION == 'Y') {
                $('#chkView').prop('checked', true);
            }
            else {
                $('#chkView').prop('checked', false);
            }
            if ($scope.ROLE_PERMISSION.INSERT_PERMISSION == 'Y') {
                $('#chkInsert').prop('checked', true);
            }
            else {
                $('#chkInsert').prop('checked', false);
            }
            if ($scope.ROLE_PERMISSION.UPDATE_PERMISSION == 'Y') {
                $('#chkUpdate').prop('checked', true);
            }
            else {
                $('#chkUpdate').prop('checked', false);
            }
            if ($scope.ROLE_PERMISSION.DELETE_PERMISSION == 'Y') {
                $('#chkDelete').prop('checked', true);
            }
            else {
                $('#chkDelete').prop('checked', false);
            }
        }
    }

    $scope.RenderTable = function () {
        $('#tableRolePermission').empty();
        html = "  <thead>\
                    <tr>\
                        <th>Menu Name</th>\
                        <th>Grant</th>\
                        <th>View</th>\
                        <th>Insert</th>\
                        <th>Update</th>\
                        <th>Delete</th>\
                        <th>Manage</th>\
                    </tr>\
                </thead>\
                "
        var lenght = Object.keys($scope.LIST_ROLE_PERMISSION).length;

        for (var i = 0; i < lenght; i++) {
            debugger;
            html += "<tr>\
                    <td>" + $scope.LIST_ROLE_PERMISSION[i].MENU_NAME + "</td>\
                    "
            if ($scope.LIST_ROLE_PERMISSION[i].GRANT_PERMISSION == null) {
                html += "<td></td>"
            }
            else if ($scope.LIST_ROLE_PERMISSION[i].GRANT_PERMISSION == 'Y') {
                html += "<td class = 'text-center'><span class='badge badge-pill badge-success'>grant</span></td>"
            }
            else {
                html += "<td class = 'text-center'><span class='badge badge-pill badge-danger'>grant</span></td>"
            }

            if ($scope.LIST_ROLE_PERMISSION[i].VIEW_PERMISSION == null) {
                html += "<td></td>"
            }
            else if ($scope.LIST_ROLE_PERMISSION[i].VIEW_PERMISSION == 'Y') {
                html += "<td class = 'text-center' ><span class='badge badge-pill badge-success'>view</span></td>"
            }
            else {
                html += "<td class = 'text-center' ><span class='badge badge-pill badge-danger'>view</span></td>"
            }

            if ($scope.LIST_ROLE_PERMISSION[i].INSERT_PERMISSION == null) {
                html += "<td></td>"
            }
            else if ($scope.LIST_ROLE_PERMISSION[i].INSERT_PERMISSION == 'Y') {
                html += "<td class = 'text-center' ><span class='badge badge-pill badge-success'>Insert</span></td>"
            }
            else {
                html += "<td class = 'text-center'><span class='badge badge-pill badge-danger'>Insert</span></td>"
            }

            if ($scope.LIST_ROLE_PERMISSION[i].UPDATE_PERMISSION == null) {
                html += "<td></td>"
            }
            else if ($scope.LIST_ROLE_PERMISSION[i].UPDATE_PERMISSION == 'Y') {
                html += "<td class = 'text-center' ><span class='badge badge-pill badge-success'>update</span></td>"
            }
            else {
                html += "<td class = 'text-center'><span class='badge badge-pill badge-danger'>update</span></td>"
            }

            if ($scope.LIST_ROLE_PERMISSION[i].DELETE_PERMISSION == null) {
                html += "<td></td>"
            }
            else if ($scope.LIST_ROLE_PERMISSION[i].DELETE_PERMISSION == 'Y') {
                html += "<td class = 'text-center' ><span class='badge badge-pill badge-success'>delete</span></td>"
            }
            else {
                html += "<td class = 'text-center'><span class='badge badge-pill badge-danger'>delete</span></td>"
            }
            html += "<td><button class ='btn btn-primary btn-block' ng-click='UpdateRolePermission(" + $scope.LIST_ROLE_PERMISSION[i].MENU_ID + "," + $scope.ROLE.ROLE_ID + ")'>Edit</button></td>"
            html += "</tr>"

        }

        var $OnCompile = $(html).appendTo('#tableRolePermission');
        $compile($OnCompile)($scope);
    }

    $scope.RenderFinish = function () {

        $('#tableRolePermission').DataTable(
            {
                destroy: true,
                searching: false
            });


    }
});