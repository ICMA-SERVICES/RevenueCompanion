$(function () {

    var baseUrl = $('#ApiBaseUrl').val();
    var userId = $('#userId').val();
    var merchantCode = $('#merchantCode').val();
    var token = $('#jwtToken').val();
    var siteLocation = $('#siteLocation').val();
    var bearerToken = 'Bearer ' + token;

    loadCreditNoteRequest();

    function loadCreditNoteRequest() {
        var remoteDataLoader = window.DevExpress.data.AspNet.createStore({
            key: "creditNoteRequestId",
            loadUrl: baseUrl + "v1.0/CreditNote/get-all-by-userId",
            onBeforeSend: function (operation,
                ajaxSettings) {
                ajaxSettings.beforeSend = function (xhr) {
                    xhr.setRequestHeader('userId', userId);
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
                        dataField: "paymentReferenceNumber",
                        caption: "Payment Ref Number",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "assessmentReferenceNumber",
                        caption: "Assessment Ref Number",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "actualAmount",
                        caption: "Actual Amount",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "amountUsed",
                        caption: "Amount Used",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "noOfRequiredApproval",
                        caption: "No Of Required Approval",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "approvalCount",
                        caption: "Approval So Far",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "dateRequested",
                        caption: "Date Requested",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        caption: "Status",
                        width: 200,
                        alignment: "center",
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {

                            if (options.data.approvalCount == 0) {
                                $('<div />').dxButton(
                                    {
                                        text: 'Pending Approval',
                                        type: 'warning',
                                        cssClass: 'badge badge-success',
                                        alignment: "center",

                                    }
                                ).appendTo(container);
                            }
                            else if (options.data.approvalCount > 0 && options.data.approvalCount < options.data.noOfRequiredApproval) {
                                $('<div />').dxButton(
                                    {
                                        text: 'In Progress',
                                        type: 'success',
                                        cssClass: 'badge badge-success',
                                        alignment: "center",

                                    }
                                ).appendTo(container);
                            }
                            else if (options.data.isApproved === true) {
                                $('<div />').dxButton(
                                    {
                                        text: 'Approved',
                                        type: 'success',
                                        cssClass: 'badge badge-success',
                                        alignment: "center",

                                    }
                                ).appendTo(container);
                            }
                            else {
                                $('<div />').dxButton(
                                    {
                                        text: 'Dis-Approved',
                                        type: 'warning',
                                        cssClass: 'badge badge-success',
                                        alignment: "center",

                                    }
                                ).appendTo(container);
                            }

                        },
                    },
                    {
                        caption: "Action",
                        width: 180,
                        alignment: "center",
                        cellTemplate: function (container, options) {

                            $('<div />').dxButton(
                                {
                                    icon: 'fa fa-eye',
                                    text: 'View',
                                    type: 'success',
                                    cssClass: 'btn btn-primary btn-sm',
                                    alignment: "center",
                                    onClick: function (e) {
                                        GotoDetails(options.data.creditNoteRequestId);
                                    }
                                }
                            ).appendTo(container);

                        },
                        visible: true
                    }
                ],

            };

        dataGrid = window.$("#creditNoteRequestContainer").dxDataGrid(gridOptions).dxDataGrid("instance");
    }

    function GotoDetails(creditNoteRequestId) {
        var url = siteLocation + "/CreditNote/RequestDetails/?requestId=" + creditNoteRequestId;
        window.location.href = url;
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
