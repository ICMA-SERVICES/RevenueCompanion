$(function () {

    var baseUrl = $('#ApiBaseUrl').val();
    var merchantCode = $('#merchantCode').val();
    var token = $('#jwtToken').val();
    var siteLocation = $('#siteLocation').val();
    var bearerToken = 'Bearer ' + token;

    $('#btn-AddUser').off().click(function () {
        var menuId = document.getElementById('MenuSetupId');
        var approvalCount = document.getElementById('NoOfRequiredApproval');
        //var phone = document.getElementById('PhoneNumber');

        var ismenuIdValid = menuId.checkValidity();
        var isapprovalCountValid = approvalCount.checkValidity();
        //var isPhoneValid = phone.checkValidity();
        var isValid = true;
        if (!ismenuIdValid) {
            $('#MenuSetupId').css('border-color', 'Red');
            ($('#MenuSetupId').focus());
            isValid = false;
        }
        if (isapprovalCountValid == false) {
            $('#NoOfRequiredApproval').css('border-color', 'Red');
            ($('#NoOfRequiredApproval').focus());
            isValid = false;
        }
        
        if (isValid == false) {
            return false;
        }
        AddApprovalSettings();
    });
    $('#btn-EditUser').off().click(function () {
        EditSetting();
    });

    loadSettings();

    function loadSettings() {
        let url = baseUrl + "v1.0/ApprovalSetting/get-all";
        var remoteDataLoader = window.DevExpress.data.AspNet.createStore({
            key: "approvalSettingId",
            loadUrl: url,
            onBeforeSend: function (operation,
                ajaxSettings) {
                ajaxSettings.beforeSend = function (xhr) {

                    xhr.setRequestHeader('Authorization', bearerToken);
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
                        dataField: "menuName",
                        caption: "Menu Name",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    }, 
                    {
                        dataField: "noOfRequiredApproval",
                        caption: "Count of Approval",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "createdOn",
                        caption: "Created On",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        caption: "Action",
                        width: 150,
                        alignment: "center",
                        cellTemplate: function (container, options) {

                            $('<div />').dxButton(
                                {
                                    icon: 'fa fa-edit',
                                    text: 'Edit',
                                    type: 'success',
                                    cssClass: 'btn btn-primary btn-sm',
                                    alignment: "center",
                                    onClick: function (e) {
                                        $('#ApprovalSettingId').val(options.data.approvalSettingId);
                                        $('#EditNoOfRequiredApproval').val(options.data.noOfRequiredApproval);
                                        
                                        $('#editModal').modal('show');
                                    }
                                }
                            ).appendTo(container);

                           
                        },
                        visible: true
                    }
                ],

            };

        dataGrid = window.$("#approvalSettingsContainer").dxDataGrid(gridOptions).dxDataGrid("instance");
    }


    function EditSetting() {
        var EditSettingRequest = {
            ApprovalSettingId: parseInt($('#ApprovalSettingId').val()),
            NoOfRequiredApproval: parseInt($('#EditNoOfRequiredApproval').val()),
           
        };

        var token = $('#jwtToken').val();
        var bearerToken = 'Bearer ' + token;

        var applicationUrl = baseUrl + 'v1.0/ApprovalSetting/update';
        $.ajax({
            headers: {
                'Authorization': bearerToken
            },
            url: applicationUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(EditSettingRequest),
            success: function (data) {
                if (data.succeeded) {
                    if (data.responseCode == "00") {
                        swal({
                            title: "Successful!",
                            text: "setting updated successfully",
                            icon: "success",
                            closeOnClickOutside: false,
                            closeOnEsc: false
                        })
                            .then(() => {
                                $('#editModal').modal('hide');
                                loadSettings();
                            });
                    }
                    else if (data.responseCode === "-1") {
                        swal({
                            title: "Already Exists!",
                            text: data.message,
                            icon: "warning",
                            closeOnClickOutside: false,
                            closeOnEsc: false
                        })
                            .then(() => {

                            });
                    }
                    else {
                        swal({
                            title: "Failed!",
                            text: data.message,
                            icon: "danger",
                            closeOnClickOutside: false,
                            closeOnEsc: false
                        })
                            .then(() => {

                            });
                    }

                }
            },
            error: function (xhr) {
                //alert('Woow something went wrong');
            }
        });
    }

    function AddApprovalSettings() {
        var AddApprovalSettingsRequest = {
            MenuSetupId: parseInt($('#MenuSetupId').val()),
            NoOfRequiredApproval: parseInt($('#NoOfRequiredApproval').val()),
        };



        var applicationUrl = baseUrl + 'v1.0/ApprovalSetting/create-new';
        $.ajax({
            headers: {
                'Authorization': bearerToken
            },
            url: applicationUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(AddApprovalSettingsRequest),
            success: function (data) {
                if (data.responseCode == "00") {
                    swal({
                        title: "Successful!",
                        text: "Settings added successfully",
                        icon: "success",
                        closeOnClickOutside: false,
                        closeOnEsc: false
                    })
                        .then(() => {
                            $('#myModal').modal('hide');
                            loadSettings();
                        });
                }
                else if (data.responseCode === "-1") {
                    swal({
                        title: "Already Exists!",
                        text: data.message,
                        icon: "warning",
                        closeOnClickOutside: false,
                        closeOnEsc: false
                    })
                        .then(() => {
                            $('#myModal').modal('hide');
                            loadSettings();
                        });
                }
                else {
                    swal({
                        title: "Failed!",
                        text: data.message,
                        icon: "warning",
                        closeOnClickOutside: false,
                        closeOnEsc: false
                    })
                        .then(() => {
                            loadSettings()
                        });
                }

            },
            error: function (xhr) {
                //alert('Woow something went wrong');
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
