var app = angular.module('ApplicationMenu', []);
app.controller('ApplicationMenuController', function ($scope, $http, $compile, $timeout) {

    $scope.LIST_TREE

    $scope.APP_SELECTED_ID
    $scope.APP_SELECTED_NAME

    $scope.LIST_COMBO_APPLICATION

    $scope._PARENT_ID
    $scope._PARENT_MENU_NAME
    $scope._APP_ID
    $scope._MENU_ID
    $scope._MENU_NAME
    $scope._MENU_STATUS
    $scope._MENU_LEVEL
    $scope._MENU_SEQ

    $scope.PARENT_MENU_NAME
    $scope.MENU_ID
    $scope.MENU_NAME
    $scope.MENU_STATUS
    $scope.MENU_LEVEL
    $scope.MENU_SEQ

    $scope.DEFAULT_SELECTED = {
        "APP_ID": 0,
        "APP_NAME": "Selected Application",
        "APP_URL": null,
        "APP_DESCRIPTION": null,
        "APP_STATUS": null,
        "APP_CODE": null
    }

    $scope.GetApplicationMenuTreeview = function () {

        $http({
            url: "/applicationmenu/GetApplicationMenuTreeview",
            method: "GET",
            params: {
                application_id: $scope.APP_SELECTED_ID
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true && response.data.OUTPUT_DATA.length > 0) {
                RenderMenu(response.data.OUTPUT_DATA)
            }
            else {
                $('#jstree_demo_div').jstree("destroy")
            }
        })
    }

    function RenderMenu(data) {

        $('#jstree_demo_div').jstree("destroy")

        $('#jstree_demo_div').jstree({
            'check_callback': true,
            'core': {
                'data': data
            }
        });

        $('#jstree_demo_div').on('changed.jstree', function (e, data) {

            var parent = 0

            for (i = 0, j = data.selected.length; i < j; i++) {

                parent = data.instance.get_node(data.selected[i]).parent

                $scope._MENU_ID = data.instance.get_node(data.selected[i]).id
                $scope._PARENT_MENU_NAME = data.instance.get_node(data.selected[i]).text.trim()

                var currentNode = $("#jstree_demo_div").jstree("get_selected");
                var childrens = $("#jstree_demo_div").jstree("get_children_dom", currentNode);

                if (childrens.length > 0 || $scope.PARENT_MENU_NAME == "Home") {
                    $('#btnDelete').hide();
                }
                else {
                    $('#btnDelete').show();

                }
            }

        });
    }

    $scope.ComboApplicationChange = function () {

        $scope.APP_SELECTED_ID = $scope.APPLICATION.APP_ID
        $scope.APP_SELECTED_NAME = $scope.APPLICATION.APP_NAME
        $scope.GetApplicationMenuTreeview()
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

    $scope.AddSubMenu = function () {
        $('#modalAddMenu').modal('toggle')
    }

    $scope.AddSubMenuConfirm = function () {
        debugger;
        var status = $("input[name='MENU_STATUS']:checked").val();
        $http({
            url: "/ApplicationMenu/AddApplicationMenu",
            method: "GET",
            params: {

                PARENT_ID: $scope._MENU_ID,
                APP_ID: $scope.APP_SELECTED_ID,
                MENU_NAME: $scope._MENU_NAME,
                MENU_STATUS: status,
                MENU_LEVEL: $scope._MENU_LEVEL,
                MENU_SEQ: $scope._MENU_SEQ,

            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            if (response.data.STATUS == true) {
                $('#modalAddMenu').modal('hide')
                $scope.GetApplicationMenuTreeview()
                $scope._MENU_ID = ""
                $scope._MENU_NAME = ""
                $scope._MENU_LEVEL = ""
                $scope._MENU_SEQ = ""
                $('#modalSaveSuccess').modal('show')
            }
        })
    }

    $scope.UpdateMenu = function () {        
        $('#modalEditMenu').modal('toggle')

        $http({
            url: "/applicationmenu/GetApplicationMenu",
            method: "GET",
            params: {
                menu_id: $scope._MENU_ID
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            debugger;
            if (response.data.STATUS) {
                var data = response.data.OUTPUT_DATA;
                $scope.PARENT_MENU_NAME = data.PARENT_NAME
                $scope.MENU_ID=data.MENU_ID
                $scope.MENU_NAME=data.MENU_NAME
                $scope.MENU_STATUS=data.MENU_STATUS
                $scope.MENU_LEVEL=data.MENU_LEVEL
                $scope.MENU_SEQ = data.MENU_SEQ

                if ($scope.MENU_STATUS == 1) {
                    $('#MENU_STATUS_1').attr('checked', 'checked');
                }
                else {
                    $('#MENU_STATUS_0').attr('checked', 'checked');
                }
            }
            
        })
      
    }

    $scope.UpdateMenuConfirm = function () {
        var status = $("input[name='MENU_STATUS']:checked").val();

        $http({
            url: "/applicationmenu/UpdateApplicationMenu",
            method: "GET",
            params: {
                MENU_ID: $scope.MENU_ID,
                APP_ID: $scope.APP_ID,
                MENU_NAME: $scope.MENU_NAME,
                MENU_STATUS: status,
                MENU_LEVEL: $scope.MENU_LEVEL,
                MENU_SEQ: $scope.MENU_SEQ,
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            debugger;
            if (response.data.STATUS) {
                $('#modalEditMenu').modal('hide')
                $scope.GetApplicationMenuTreeview()
                $('#modalSaveSuccess').modal('show')
            }

        })

    }

    $scope.DeleteMenu = function () {
        $('#modalDeleteMenu').modal('toggle')
    }

    $scope.DeleteMenuConfirm = function () {
        $http({
            url: "/applicationmenu/DeleteApplicationMenu",
            method: "GET",
            params: {
                menu_id: $scope._MENU_ID
            }
        }).then(function (response) {
            console.log(response.data.OUTPUT_DATA)
            debugger;
            if (response.data.STATUS) {
                $('#modalDeleteMenu').modal('hide')
                $scope.GetApplicationMenuTreeview()
                $('#modalSaveSuccess').modal('show')
            }

        })
    }



});

