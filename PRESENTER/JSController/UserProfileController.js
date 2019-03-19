var app = angular.module('UserProfileApplication', []);
app.controller('UserProfileController', function ($scope, $http, $compile, $timeout, $window) {

    $scope.LIST_USER_PROFILE = []

    $scope.EMPLOYEE_DEPARTMENT
    $scope.EMPLOYEE_DISPLAY_NAME
    $scope.EMPLOYEE_FIRST_NAME
    $scope.EMPLOYEE_LAST_NAME
    $scope.EMPLOYEE_STATUS
    $scope.EMPLOYEE_ID
    $scope.LIST_USER_ALLOWED_APP = []
    $scope.LIST_USER_ALLOWED_ROLE = []

    $scope.DEFAULT_SELECTED = {
        "APP_ID": 0,
        "APP_NAME": "Selected Application",
        "APP_URL": null,
        "APP_DESCRIPTION": null,
        "APP_STATUS": null,
        "APP_CODE": null
    }

    $scope.GetListUserProfile = function () {
        $http({
            url: "/userprofile/GetListUserProfile",
            method: "GET",
            params: {
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $scope.LIST_USER_PROFILE = response.data.OUTPUT_DATA
            }
        }).then(function () {
            $scope.RenderTable()
        }).then(function () {
            $('#tableUser').DataTable(
           {
               destroy: true,
               searching: false
           });
        });
    }

    $scope.RenderTable = function () {
        $('#tableUser').empty();
        html = "  <thead>\
                    <tr>\
                        <th>Display Name</th>\
                        <th>Status</th>\
                        <th>Department</th>\
                        <th>Manage</th>\
                    </tr>\
                </thead>\
                "
        var lenght = Object.keys($scope.LIST_USER_PROFILE).length;

        for (var i = 0; i < lenght; i++) {
            html += "<tr>\
                        <td>" + $scope.LIST_USER_PROFILE[i].EMPLOYEE_DISPLAY_NAME + "</td>\
                        <td>" + $scope.LIST_USER_PROFILE[i].EMPLOYEE_STATUS + "</td>\
                        <td>" + $scope.LIST_USER_PROFILE[i].EMPLOYEE_DEPARTMENT + "</td>\
                        <td>\
                        <button class ='btn btn-primary' ng-click='ViewUser(" + $scope.LIST_USER_PROFILE[i].EMPLOYEE_ID + ")'>View</button>\
                        <button class ='btn btn-warning' ng-click='UpdateUser(" + $scope.LIST_USER_PROFILE[i].EMPLOYEE_ID + ")'>Edit</button>\
                        <button class ='btn btn-success'ng-click='ShowModalUserRole(" + $scope.LIST_USER_PROFILE[i].EMPLOYEE_ID + ")'>Role</button>\
                        <button class ='btn btn-success'ng-click='GetUserApplication(" + $scope.LIST_USER_PROFILE[i].EMPLOYEE_ID + ")'>Application</button>\
                        <button class ='btn btn-danger'ng-click='EditDenied(" + $scope.LIST_USER_PROFILE[i].EMPLOYEE_ID + ")'>Denied</button>\
                       </td>\
                    </tr>\
                    "
        }

        var $OnCompile = $(html).appendTo('#tableUser');
        $compile($OnCompile)($scope);
    }

    $scope.ViewUser = function (id) {
        $scope.GetUserProfile(id)
        $('#modalViewUser').modal('show');

    }

    $scope.UpdateUser = function (id) {
        $scope.GetUserProfile(id)
        $('#modalUpdateUser').modal('show');
    }

    $scope.UpdateUserConfirm = function () {

    }

    $scope.GetUserProfile = function (id) {
        $http({
            url: "/userprofile/GetUserProfile",
            method: "GET",
            params: {
                employee_id: id
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $scope.EMPLOYEE_DEPARTMENT = response.data.OUTPUT_DATA.EMPLOYEE_DEPARTMENT
                $scope.EMPLOYEE_DISPLAY_NAME = response.data.OUTPUT_DATA.EMPLOYEE_DISPLAY_NAME
                $scope.EMPLOYEE_FIRST_NAME = response.data.OUTPUT_DATA.EMPLOYEE_FIRST_NAME
                $scope.EMPLOYEE_LAST_NAME = response.data.OUTPUT_DATA.EMPLOYEE_LAST_NAME
                $scope.EMPLOYEE_STATUS = response.data.OUTPUT_DATA.EMPLOYEE_STATUS
                $scope.EMPLOYEE_ID = response.data.OUTPUT_DATA.EMPLOYEE_ID
            }
        }).then(function (response) {
            if ($scope.EMPLOYEE_STATUS == "1") {

            }
            else {

            }
        });
    }

    $scope.GetUserApplication = function (id) {
        $scope.EMPLOYEE_ID = id;
        $http({
            url: "/userprofile/GetUserApplication",
            method: "GET",
            params: {
                employee_id: id
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $scope.LIST_USER_PROFILE_APP = response.data.OUTPUT_DATA;
            }
        }).then(function () {
            $scope.RenderUserApp()
        }).then(function () {
            $('#modalUserApp').modal('show');
        }).then(function () {
            $('#userAppContent').DataTable(
           {
               destroy: true,
               searching: false
           });
        });
    }

    $scope.ShowModalUserRole = function (id) {
        $scope.EMPLOYEE_ID = id;
        $('#modalUserRole').modal('show');
    }

    $scope.RenderUserApp = function () {
        debugger;
        $('#userAppContent').empty();
        html = "  <thead>\
                    <tr>\
                        <th>App Name</th>\
                        <th>Allowed</th>\
                    </tr>\
                </thead>\
                "
        var lenght = Object.keys($scope.LIST_USER_PROFILE_APP).length;

        for (var i = 0; i < lenght; i++) {
            html += "<tr>\
                        <td>" + $scope.LIST_USER_PROFILE_APP[i].APP_NAME + "</td>\
                    "
            if ($scope.LIST_USER_PROFILE_APP[i].ALLOWED == true) {
                html += "<td><input type='checkbox'  value = " + $scope.LIST_USER_PROFILE_APP[i].APP_ID + " ng-click='UserAppChange($event);' checked/>"
                $scope.LIST_USER_ALLOWED_APP.push($scope.LIST_USER_PROFILE_APP[i].APP_ID)
            }
            else {
                html += "<td><input type='checkbox'   value = " + $scope.LIST_USER_PROFILE_APP[i].APP_ID + " ng-click='UserAppChange($event);' />"
            }
            html += "</tr>\
                    "
        }

        var $OnCompile = $(html).appendTo('#userAppContent');
        $compile($OnCompile)($scope);
    }

    $scope.UserAppChange = function ($event) {
        debugger;
        var val = parseInt($event.target.value)
        if ($event.target.checked) {
            var index = $scope.LIST_USER_ALLOWED_APP.indexOf(val);
            if (index > -1) {

            } else {
                $scope.LIST_USER_ALLOWED_APP.push(val)
            }
        }
        else {
            var index = $scope.LIST_USER_ALLOWED_APP.indexOf(val);
            if (index > -1) {
                $scope.LIST_USER_ALLOWED_APP.splice(index, 1)
            } else {

            }

        }
    }

    $scope.UserRoleChange = function ($event) {
        debugger;
        var val = parseInt($event.target.value)
        if ($event.target.checked) {
            var index = $scope.LIST_USER_ALLOWED_ROLE.indexOf(val);
            if (index > -1) {

            } else {
                $scope.LIST_USER_ALLOWED_ROLE.push(val)
            }
        }
        else {
            var index = $scope.LIST_USER_ALLOWED_ROLE.indexOf(val);
            if (index > -1) {
                $scope.LIST_USER_ALLOWED_ROLE.splice(index, 1)
            } else {

            }

        }
    }

    $scope.UpdateUserProfileApplication = function () {
        debugger;
        if ($scope.LIST_USER_ALLOWED_APP.length === 0) {
            $scope.LIST_USER_ALLOWED_APP = [];
        }
        $http({
            url: "/userprofile/UpdateUserApplication",
            method: "GET",
            params: {
                employee_id: $scope.EMPLOYEE_ID,
                application_id: $scope.LIST_USER_ALLOWED_APP

            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $('#modalSaveSuccess').modal('show');
                $('#modalUserApp').modal('hide');
            }
            else {
                $('#modalSaveFail').modal('show');
                $('#modalUserApp').modal('hide');
            }
        })
    }

    $scope.UpdateUserProfileRole = function () {
        debugger;
        if ($scope.LIST_USER_ALLOWED_ROLE.length === 0) {
            $scope.LIST_USER_ALLOWED_ROLE = [];
        }
        $http({
            url: "/userprofile/UpdateUserRole",
            method: "GET",
            params: {
                employee_id: $scope.EMPLOYEE_ID,
                application_id: $scope.APP_SELECTED_ID,
                role_id: $scope.LIST_USER_ALLOWED_ROLE

            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $('#modalSaveSuccess').modal('show');
                $('#modalUserRole').modal('hide');
            }
            else {
                $('#modalSaveFail').modal('show');
                $('#modalUserRole').modal('hide');
            }
        })
    }

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
            }
        });
    }

    $scope.ComboApplicationChange = function () {

        $scope.APP_SELECTED_ID = $scope.APPLICATION.APP_ID
        $scope.GetUserRole()
    }

    $scope.GetUserRole = function () {
        $http({
            url: "/userprofile/GetUserRole",
            method: "GET",
            params: {
                employee_id: $scope.EMPLOYEE_ID,
                application_id: $scope.APP_SELECTED_ID
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $scope.LIST_USER_PROFILE_ROLE = response.data.OUTPUT_DATA;
            }
        }).then(function () {
            $scope.RenderUserRole()
        }).then(function () {
            $('#userRoleContent').DataTable(
           {
               destroy: true,
               searching: false
           });
        });
    }

    $scope.RenderUserRole = function () {
        debugger;

        $('#userRoleContent').empty();
        html = "  <thead>\
                    <tr>\
                        <th>Role Name</th>\
                        <th>Allowed</th>\
                    </tr>\
                </thead>\
                "
        var lenght = Object.keys($scope.LIST_USER_PROFILE_ROLE).length;

        for (var i = 0; i < lenght; i++) {
            html += "<tr>\
                        <td>" + $scope.LIST_USER_PROFILE_ROLE[i].ROLE_NAME + "</td>\
                    "
            if ($scope.LIST_USER_PROFILE_ROLE[i].ALLOWED == true) {
                html += "<td><input type='checkbox'  value = " + $scope.LIST_USER_PROFILE_ROLE[i].ROLE_ID + " ng-click='UserRoleChange($event);' checked/>"
                $scope.LIST_USER_ALLOWED_APP.push($scope.LIST_USER_PROFILE_ROLE[i].ROLE_ID)
            }
            else {
                html += "<td><input type='checkbox'   value = " + $scope.LIST_USER_PROFILE_ROLE[i].ROLE_ID + " ng-click='UserRoleChange($event);' />"
            }
            html += "</tr>\
                    "
        }

        var $OnCompile = $(html).appendTo('#userRoleContent');
        $compile($OnCompile)($scope);
    }

});