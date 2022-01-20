$(function () {
   
    var baseUrl = $('#ApiBaseUrl').val();
    var merchantCode = $('#merchantCode').val();
    var token = $('#jwtToken').val();
    var siteLocation = $('#siteLocation').val();
    var userId = $('#userId').val();
    var bearerToken = 'Bearer ' + token;

    
    loadUser();

    function loadUser() { 
        let url = baseUrl + "Account/get-users";
        var remoteDataLoader = window.DevExpress.data.AspNet.createStore({
            key: "userId",
            loadUrl: url,
            onBeforeSend: function (operation,
                ajaxSettings) {
                ajaxSettings.beforeSend = function (xhr) {

                    xhr.setRequestHeader('Authorization', bearerToken);
                    xhr.setRequestHeader('RecentlyCreated', 5);
                },
                    ajaxSettings.global = false;
            }
        });




        var dataGrid,
            gridOptions = {
                dataSource: remoteDataLoader,
                columnHidingEnabled: true,
                showBorders: true,
                remoteOperations: {
                    paging: true,
                    filtering: true,
                    sorting: true,
                    grouping: true,
                    summary: true,
                    groupPaging: true
                },
                searchPanel: {
                    visible: true,
                    placeholder: "Search...",
                    width: 250
                },
                paging: {
                    pageSize: 10
                },
                pager: {
                    showNavigationButtons: true,
                    showPageSizeSelector: true,
                    allowedPageSizes: [10, 20, 100, 250],
                    showInfo: true
                },
                selection: {
                    mode: "single",
                    mode: "multiple",
                    selectAllMode: 'page',
                    showCheckBoxesMode: 'no'
                },
                "export": {
                    enabled: false,
                    fileName: ""
                },
                hoverStateEnabled: true,
                showRowLines: true,
                rowAlternationEnabled: true,
                columnAutoWidth: true,
                columns: [
                    {
                        caption: 'S/N',
                        width: "auto",
                        allowSorting: false,
                        allowFiltering: false,
                        allowReordering: false,
                        allowHeaderFiltering: false,
                        allowGrouping: false,
                        cellTemplate: function (container, options) {
                            container.text(dataGrid.pageIndex() * dataGrid.pageSize() + (options.rowIndex + 1));

                        }
                    },
                    {
                        dataField: "lastName",
                        caption: "User Name",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "firstName",
                        caption: "User Name",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "role",
                        caption: "Role",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "email",
                        caption: "User Email",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },

                    {
                        dataField: "status",
                        caption: "Status",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    }
                ],

            };

        dataGrid = window.$("#recentlyCreatedUserGrid").dxDataGrid(gridOptions).dxDataGrid("instance");
    }

    GetTotalUserCount(userId);
    GetTotalApprovalSetting();


    function GetTotalUserCount(userId) {
        var applicationUrl = baseUrl + 'v1.0/Account/GetUserCount/' + userId;
        $.ajax({
            headers: {
                'Authorization': bearerToken
            },
            url: applicationUrl,
            type: 'POST',
            data: userId,
            contentType: 'application/json',
            success: function (data) {
                document.getElementById("totalUser").innerHTML = data;
            },
            error: function (xhr) {
            }
        });
    }

});

$(document).on({
    ajaxStart: function () {
        $('#cover-spin').show(0);
    },
    ajaxStop: function () {
        $('#cover-spin').hide(0);
    }
});
