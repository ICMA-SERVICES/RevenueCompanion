$(function () {

    var baseUrl = $('#ApiBaseUrl').val();
    var token = $('#jwtToken').val();
    var bearerToken = 'Bearer ' + token;
    var currentYear = new Date().getFullYear();

    loadCreditNoteRequest(currentYear);
    loadPayeeList()

    function loadCreditNoteRequest(year, payerUtin = null) {
        var remoteDataLoader = window.DevExpress.data.AspNet.createStore({
            key: "payerUtin",
            loadUrl: baseUrl + "v1.0/SummaryReport/defaulter-history",
            onBeforeSend: function (operation,
                ajaxSettings) {
                ajaxSettings.beforeSend = function (xhr) {
                    xhr.setRequestHeader('year', year);
                    xhr.setRequestHeader('payerUtin', payerUtin);
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
                        dataField: "assessmentRefNo",
                        caption: "Assessment Ref No",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

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
                        dataField: "noOfDaysDefaulted",
                        caption: "Days Defualted",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                    },
                    {
                        dataField: "totalLiability",
                        caption: "Default Amount",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {
                            container.text(formatMoney(options.data.totalPaymentAmount, "N"));
                        }
                    },
                    {
                        dataField: "paymentAmount",
                        caption: "Amount Paid",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold',
                        cellTemplate: function (container, options) {
                            container.text(formatMoney(options.data.totalPaymentAmount, "N"));
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
                                    }
                                }
                            ).appendTo(container);

                        },
                        visible: true
                    }
                ],

            };

        dataGrid = window.$("#RequestContainer").dxDataGrid(gridOptions).dxDataGrid("instance");
    }

    for (var i = 0; i < 6; i++) {
        $("#yearField").append(`<option value='${currentYear - i}'>${currentYear - i}</option>`)
    }

    $("#filterTable").click(function () {
        let year = $("#yearField").val();
        let payerUtin = $("#payerUtin").val();
        loadCreditNoteRequest(year, payerUtin)
    })

    function loadPayeeList() {
        $.ajax({
            url: baseUrl + "v1.0/SummaryReport/payer-list",
            method: "GET",
            dataType: "json",
            contentType: "application/json",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Authorization": bearerToken,
            },
            success: function (data) {
                if (data) {
                    if (data.length > 0) {
                        let d = data;
                        $("#payerUtin").html('<option value="">Select</option>');
                        $.each(d, function (i, val) {
                            $("#payerUtin").append(
                                "<option value='" + val.payerUtin + "'>" + val.taxAgent + "</option>"
                            );
                        });
                    }
                }
            },
            error: function (err) {
                apiErrorHandler(err);
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
