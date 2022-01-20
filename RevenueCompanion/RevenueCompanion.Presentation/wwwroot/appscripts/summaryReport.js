$(function () {

    var baseUrl = $('#ApiBaseUrl').val();
    var userId = $('#userId').val();
    var merchantCode = $('#merchantCode').val();
    var token = $('#jwtToken').val();
    var siteLocation = $('#siteLocation').val();
    var bearerToken = 'Bearer ' + token;
    var currentYear = new Date().getFullYear();

    loadCreditNoteRequest(currentYear);

    function loadCreditNoteRequest(year) {
        var remoteDataLoader = window.DevExpress.data.AspNet.createStore({
            key: "payerUtin",
            loadUrl: baseUrl + "v1.0/SummaryReport/summary-report",
            onBeforeSend: function (operation,
                ajaxSettings) {
                ajaxSettings.beforeSend = function (xhr) {
                    xhr.setRequestHeader('year', year);
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
                        dataField: "taxAgent",
                        caption: "Tax Agent",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "payerUtin",
                        caption: "payerUtin",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "totalDaysDefualted",
                        caption: "Total Days Defualted",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                    },
                    {
                        dataField: "totalPaymentAmount",
                        caption: "Total Amount Defaulted",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {
                            container.text(formatMoney(options.data.totalPaymentAmount, "N"));
                        }

                    },
                    {
                        caption: "Action",
                        width: 180,
                        alignment: "center",
                        cellTemplate: function (container, options) {
                            console.log("options", options);

                            $('<div />').dxButton(
                                {
                                    icon: 'fa fa-eye',
                                    text: 'View',
                                    type: 'success',
                                    cssClass: 'btn btn-primary btn-sm',
                                    alignment: "center",
                                    onClick: function (e) {
                                        GotoDetails(options.data.payerUtin, options.data.taxAgent);
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

    function GotoDetails(payerUtin, taxAgent) {
        var url = siteLocation + "/PayeeDefaulter/SummaryDetails/?payerUtin=" + payerUtin + "&taxAgent=" + taxAgent;
        window.location.href = url;
    }

    for (var i = 0; i < 6; i++) {
        $("#yearField").append(`<option value='${currentYear - i}'>${currentYear - i}</option>`)
    }

    $("#yearField").change(function () {
        loadCreditNoteRequest($(this).val());
    })


});

$(document).on({
    ajaxStart: function () {
        $('#cover-spin').show(0);
    },
    ajaxStop: function () {
        $('#cover-spin').hide(0);
    }

});
