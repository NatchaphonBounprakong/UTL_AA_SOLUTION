var app = angular.module('Application', []);
app.controller('ApplicationController', function ($scope, $http, $compile, $timeout) {

    $scope.LIST_APPLICATION_MODEL = [];
    $scope.APP_ID
    $scope.APP_NAME
    $scope.APP_URL
    $scope.APP_DESCRIPTION
    $scope.APP_CODE

    $scope.FILTER_APP_NAME

    $scope._APP_ID = 0
    $scope._APP_NAME
    $scope._APP_URL
    $scope._APP_DESCRIPTION
    $scope._APP_CODE
    $scope._APP_STATUS

   
    $scope.GetListApplication = function () {
        debugger;
        $http({
            url: "/application/getlistapplication",
            method: "POST",
            params: {
                APP_NAME: $scope.FILTER_APP_NAME
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                var OUTPUT_DATA = response.data.OUTPUT_DATA;
                $scope.LIST_APPLICATION_MODEL = OUTPUT_DATA.LIST_APPLICATION_MODEL;
            }
        }).then(function (response) {
            $scope.RenderTable();
        })
        .then(function (response) {
            $scope.RenderFinish();
        })

    }

    $scope.RenderTable = function () {
        $('#tableApplication').empty();
        html = "  <thead>\
                    <tr>\
                        <th>Application Code</th>\
                        <th>Applicatoin Name</th>\
                        <th>Desscription</th>\
                        <th>URL</th>\
                        <th>Status</th>\
                        <th>Edit</th>\
                    </tr>\
                </thead>\
                "
        var lenght = Object.keys($scope.LIST_APPLICATION_MODEL).length;

        for (var i = 0; i < lenght; i++) {
            html += "<tr>\
                        <td>" + $scope.LIST_APPLICATION_MODEL[i].APP_CODE + "</td>\
                        <td>" + $scope.LIST_APPLICATION_MODEL[i].APP_NAME + "</td>\
                        <td>" + $scope.LIST_APPLICATION_MODEL[i].APP_DESCRIPTION + "</td>\
                        <td>" + $scope.LIST_APPLICATION_MODEL[i].APP_URL + "</td>\
                        <td>" + $scope.LIST_APPLICATION_MODEL[i].APP_STATUS + "</td>\
                        <th><button class ='btn btn-primary btn-block' ng-click='GetApplication(" + $scope.LIST_APPLICATION_MODEL[i].APP_ID + ")'>Edit</button></th>\
                    </tr>\
                    "
        }

        var $OnCompile = $(html).appendTo('#tableApplication');
        $compile($OnCompile)($scope);
    }

    $scope.AddApplication = function () {
        var status = $("input[name='APP_STATUS']:checked").val();
        
        $http({
            url: "/application/AddApplication",
            method: "GET",
            params: {
                APP_NAME: $scope.APP_NAME,
                APP_URL: $scope.APP_URL,
                APP_DESCRIPTION: $scope.APP_DESCRIPTION,
                APP_STATUS: status,
                APP_CODE: $scope.APP_CODE,
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $('#modalAddApp').modal('hide');
                $scope.APP_NAME = ""
                $scope.APP_URL = ""
                $scope.APP_DESCRIPTION = ""
                $scope.APP_CODE = ""
                $('#modalSaveSuccess').modal('show');
            }
        }).then(function (response) {

            $scope.GetListApplication();
        })
    }

    $scope.RenderFinish = function () {

        $('#tableApplication').DataTable(
            {
                destroy: true,
                searching: false
            });

        
    }

    $scope.GetApplication = function (application_id) {
        $scope._APP_ID = application_id
        $http({
            url: "/application/GetApplication",
            method: "GET",
            params: {
                application_id: application_id
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                
                $scope._APP_ID = response.data.OUTPUT_DATA.APP_ID
                $scope._APP_NAME = response.data.OUTPUT_DATA.APP_NAME
                $scope._APP_URL = response.data.OUTPUT_DATA.APP_URL
                $scope._APP_DESCRIPTION = response.data.OUTPUT_DATA.APP_DESCRIPTION
                $scope._APP_CODE = response.data.OUTPUT_DATA.APP_CODE
                if (response.data.OUTPUT_DATA.APP_STATUS == "1") {
                    $('#_APP_STATUS_1').attr('checked', 'checked');
                }
                else {
                    $('#_APP_STATUS_0').attr('checked', 'checked');
                }
               
            }
        }).then(function (response) {
            
            $("#modalUpdateApp").modal("show")
           
        });
    }

    $scope.UpdateApplication = function () {
        
        var status = $("input[name='_APP_STATUS']:checked").val();
        $http({
            url: "/application/UpdateApplication",
            method: "POST",
            params: {
                
                APP_ID: $scope._APP_ID,
                APP_NAME: $scope._APP_NAME,
                APP_DESCRIPTION: $scope._APP_DESCRIPTION,
                APP_URL: $scope._APP_URL,
                APP_CODE: $scope._APP_CODE,
                APP_DESCRIPTION: $scope._APP_DESCRIPTION,
                APP_STATUS: status,
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $('#modalUpdateApp').modal('hide');
                $scope.GetListApplication();
            }
        }).then(function (response) {

            $('#modalSaveSuccess').modal('show');

        });
    }

});