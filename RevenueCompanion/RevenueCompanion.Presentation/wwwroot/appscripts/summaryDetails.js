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
            key: "payerUtin",
            loadUrl: baseUrl + "v1.0/SummaryReport/summary-details",
            onBeforeSend: function (operation,
                ajaxSettings) {
                ajaxSettings.beforeSend = function (xhr) {
                    xhr.setRequestHeader('payerUtin', $("#payerUtin").val());
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
                        dataField: "dueDate",
                        caption: "Due Date",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {
                            container.text(formatDate(options.data.dueDate));
                        }
                    },
                    {
                        dataField: "actualDateOfPayment",
                        caption: "Actual Date Of Payment",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {
                            container.text(formatDate(options.data.actualDateOfPayment));
                        }
                    },
                    {
                        dataField: "noOfDaysDefaulted",
                        caption: "Days Defualted",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                    },
                    {
                        dataField: "initialAmountDue",
                        caption: "Initial Amount Due",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {
                            container.text(formatMoney(options.data.initialAmountDue));
                        }

                    },
                    {
                        dataField: "accruedIntreset",
                        caption: "Accrued Intreset",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {
                            container.text(formatMoney(options.data.accruedIntreset));
                        }
                    },
                    {
                        dataField: "accruedPenalty",
                        caption: "Accrued Penalty",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {
                            container.text(formatMoney(options.data.accruedPenalty));
                        }
                    },
                    {
                        dataField: "totalLiability",
                        caption: "Total Liability",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {
                            container.text(formatMoney(options.data.totalLiability));
                        }
                    },
                    {
                        dataField: "liabilityPaid",
                        caption: "Liability Paid",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {
                            container.text(formatMoney(options.data.liabilityPaid));
                        }
                    },
                    {
                        dataField: "balance",
                        caption: "Balance",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {
                            container.text(formatMoney(options.data.balance));
                        }
                    },
                    {
                        dataField: "assessmentRefNo",
                        caption: "Assessment Ref No",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                    },
                    {
                        dataField: "payerUtin",
                        caption: "Payer Utin",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                    },
//                    {
//                        caption: "Action",
//                        width: 180,
//                        alignment: "center",
//                        cellTemplate: function (container, options) {
//                            console.log("options", options);
//                            $('<div />').dxButton(
//                                {
//                                    icon: 'fa fa-eye',
//                                    text: 'View',
//                                    type: 'success',
//                                    cssClass: 'btn btn-primary btn-sm',
//                                    alignment: "center",
//                                    onClick: function (e) {
///*                                        GotoDetails(options.data.payerUtin);*/
//                                    }
//                                }
//                            ).appendTo(container);

//                        },
//                        visible: true
//                    }
                ],

            };

        dataGrid = window.$("#requestContainer").dxDataGrid(gridOptions).dxDataGrid("instance");
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
