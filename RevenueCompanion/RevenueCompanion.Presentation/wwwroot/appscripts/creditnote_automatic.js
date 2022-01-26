////const { swal } = require("../Scripts/OtherScript/SweetAlert");

$(document).ready(function () {
    var token = $('#jwtToken').val();
    var bearerToken = 'Bearer ' + token;
    var baseUrl = $('#ApiBaseUrl').val();
    var menuSetupId = $('#menuSetupId').val();
    var merchantCode = $('#merchantCode').val();
    var amountAvailable, revenueName;

    $('#ProposedAmount').blur(function () {
/*        alert($(this).val());*/
        if (parseFloat($(this).val()) > parseFloat(amountAvailable)) {
            swal("Proposed amount can not be greater than available amount")
            $(this).val(0);
        }
    })

    $('#actionLoader').hide();
    var that = this;
    //domainName = document.querySelector("#domainNameId").value;

    $('#SearchPaymentRefBtn').on('click', function () {
        $('#SearchPaymentRefBtn').attr('disabled', 'disabled');
        var isValid = true;
        let assessmentNo = document.getElementById("paymentReferenceNumber").value;

        if (assessmentNo == "" || assessmentNo == null) {
            $('#paymentReferenceNumber').css('border-color', 'Red');
            $('#paymentReferenceNumber').focus();
            $('#SearchPaymentRefBtn').removeAttr('disabled');
            ShowMessagePopup("Oops!", "Payment Reference Number Cannot be Empty", "warning");

            return false;
        }
        SearchPaymentRef(assessmentNo);
    });



    $('#btn_AddCreditNoteRequest').on('click', function () {
        $('#btn_AddCreditNoteRequest').attr('disabled', 'disabled');
        let reason = document.getElementById("Reason").value;
        let proposedAmount = document.getElementById("ProposedAmount").value;
        let Balance = document.getElementById("Balance").value;

        if (reason == "" || reason == null) {
            $('#Reason').css('border-color', 'Red');
            $('#Reason').focus();
            $('#btn_AddCreditNoteRequest').removeAttr('disabled');
            ShowMessagePopup("Oops!", "Please enter a reason for this request", "warning");

            return false;
        }
        if (proposedAmount == "" || proposedAmount == null) {
            $('#ProposedAmount').css('border-color', 'Red');
            $('#ProposedAmount').focus();
            $('#btn_AddCreditNoteRequest').removeAttr('disabled');
            ShowMessagePopup("Oops!", "Please enter a proposed amount", "warning");

            return false;
        }
        if (parseInt(proposedAmount) > parseInt(Balance)) {
            ShowMessagePopup("Oops!", "Amoung proposed cannot be greater than the balance remaining.", "warning");
            return false;
        }
        else {
            AddCreditNoteRequest();
        }

    });



    $('#moreDetails').on('click', function () {
        $('#myModal').modal('show');
    });
    $('#searchAssessmentRefBtn').on('click', function () {
        $('#searchAssessmentRefBtn').attr('disabled', 'disabled');
        let assessmentNo = document.getElementById("assessmentNo").value;

        if (assessmentNo == "" || assessmentNo == null) {
            $('#assessmentNo').css('border-color', 'Red');
            $('#assessmentNo').focus();
            $('#searchAssessmentRefBtn').removeAttr('disabled');
            ShowMessagePopup("Oops!", "Assessment Reference Number Cannot be Empty", "warning");

            return false;
        }
        SearchAssessmentRef(assessmentNo);
    });

    function SearchPaymentRef(paymentRef) {

        var applicationUrl = baseUrl + 'v1.0/CreditNote/search-by-paymentref';
        $.ajax({
            headers: {
                'Authorization': bearerToken,
                'paymentRef': paymentRef
            },
            url: applicationUrl,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                console.log(response);
                if (response.data != null) {
                    //ShowSearchByPaymentRefDevGrid(response);
                    //document.getElementById('Name').innerHTML = response.data.payername;
                    //document.getElementById('Bank').innerHTML = response.data.bankName;
                    //document.getElementById('Branch').innerHTML = response.data.branchname;
                    //document.getElementById('Amount').innerHTML = response.data.amount;
                    let balance = response.data[0].balance;
                    document.getElementById('Balance').value = formatMoney(balance, "N");
                    amountAvailable = balance;
                    revenueName = response.data[0].revenueName;
                    //document.getElementById('RevenueCode').innerHTML = response.data.revenueCode;
                    //document.getElementById('RevenueName').innerHTML = response.data.revenueName;
                    //document.getElementById('AgencyCode').innerHTML = response.data.agencyCode;
                    //document.getElementById('AgencyName').innerHTML = response.data.agencyName;
                    //document.getElementById('AgencyName').innerHTML = response.data.agencyName;
                    //document.getElementById('PaymentDate').innerHTML = response.data.paymentDateString;
                    //var $active = $('.wizard .nav-tabs li.active');
                    //$active.next().removeClass('disabled');
                    //nextTab($active);
                    ShowSearchByPaymentRefDevGrid(paymentRef);

                }
                else {
                    response = response.searchResult
                    swal({
                        title: "An error occured!",
                        text: response.message,
                        icon: "warning",
                        closeOnClickOutside: false,
                        closeOnEsc: false
                    })
                        .then(() => {
                            $('#SearchPaymentRefBtn').removeAttr('disabled');
                            return false;
                        });
                    $('#SearchPaymentRefBtn').removeAttr('disabled');
                    return false;
                }
            }
        });

       
    }

    function SearchAssessmentRef(paymentRef) {

        var applicationUrl = baseUrl + 'v1.0/CreditNote/get-assessment';
        $.ajax({
            headers: {
                'Authorization': bearerToken,
                'assessmentRef': paymentRef
            },
            url: applicationUrl,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
/*                console.log("response", response);*/
                if (response.succeeded) {
                    if (response.data.revenueName != revenueName) {
                        swal({
                            title: "This Payment cannot be used!",
                            text: "Revenue Name for Payment does not match Revenue name for Assessment",
                            icon: "warning",
                            closeOnClickOutside: false,
                            closeOnEsc: false
                        })
                            .then(() => {
                                $('#searchAssessmentRefBtn').removeAttr('disabled');
                            });
                        $('#searchAssessmentRefBtn').removeAttr('disabled');
                        return false
                    }

                    document.getElementById('PayerName').innerHTML = response.data.payerName;
                    document.getElementById('PlatformName').innerHTML = response.data.platformcode;
                    document.getElementById('Utin').innerHTML = response.data.agentUtin;
                    document.getElementById('TotalAmount').innerHTML = formatMoney(response.data.totalAmount, "N");
                    document.getElementById('AvailableAmount').innerHTML = formatMoney(amountAvailable, "N");
                    document.getElementById('RevenueNameAssessment').innerHTML = response.data.revenueName;
                    document.getElementById('ProposedAmount').value = amountAvailable;
                    /* document.getElementById('TotalAmount').innerHTML = response.data.totalAmount;*/


/*                    document.getElementById('AmountAccessedModal').innerHTML = response.data.amountAccessed;*/
                    document.getElementById('RevenueNameModal').innerHTML = response.data.revenueName;
                    document.getElementById('TotalAmountModal').innerHTML = formatMoney(response.data.totalAmount, "N");
                    document.getElementById('AmountPaidModal').innerHTML = formatMoney(response.data.amountPaid, "N");
                    document.getElementById('RevenueCode').innerHTML = response.data.revenueCode;
                    document.getElementById('AgencyName').innerHTML = response.data.agencyName;
                    /*document.getElementById('AgencyCode').innerHTML = response.data.agencyCode;*/
                    document.getElementById('TaxYear').innerHTML = response.data.taxYear;
                    document.getElementById('DateCreated').innerHTML = response.data.dateCreated;
                    document.getElementById('AssessmentCreatedBy').innerHTML = response.data.assessmentCreatedBy;



                    var $active = $('.wizard .nav-tabs li.active');
                    $active.next().removeClass('disabled');
                    nextTab($active);
                }
                else {
                    swal({
                        title: "An error occured!",
                        text: response.message,
                        icon: "warning",
                        closeOnClickOutside: false,
                        closeOnEsc: false
                    })
                        .then(() => {
                            $('#searchAssessmentRefBtn').removeAttr('disabled');
                        });
                    $('#searchAssessmentRefBtn').removeAttr('disabled');
                }
            }
        });
    }

    function AddCreditNoteRequest() {
        debugger;

        var AddCreditNoteRequest = {
            MenuSetupId: parseInt(menuSetupId),
            PaymentReferenceNumber: $('#paymentReferenceNumber').val(),
            AssessmentReferenceNumber: $('#assessmentNo').val(),
            ActualAmount: parseFloat(document.getElementById("TotalAmountModal").innerHTML),
            AmountUsed: parseFloat($('#ProposedAmount').val()),
            Balance: parseFloat(amountAvailable),
            MerchantCode: merchantCode,
            TransType: "1"
        };



        var applicationUrl = baseUrl + 'v1.0/CreditNote/add-new';
        $.ajax({
            headers: {
                'Authorization': bearerToken
            },
            url: applicationUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(AddCreditNoteRequest),
            success: function (data) {
                console.log("data", data);
                if (data.responseCode == "00") {
                    swal({
                        title: "Successful!",
                        text: "Submitted for Approval",
                        icon: "success",
                        closeOnClickOutside: false,
                        closeOnEsc: false
                    })
                        .then(() => {
                            window.location.reload();
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
                            window.location.reload();
                        });
                }

            },
            error: function (xhr) {
                console.log("xhr", xhr)
                //window.location.reload();
                //alert('Woow something went wrong');
            }
        });
    }
    function nextTab(elem) {
        $(elem).next().find('a[data-toggle="tab"]').click();
    }

    function ShowSearchByPaymentRefDevGrid(paymentRef) {
        var $active = $('.wizard .nav-tabs li.active');
        $active.next().removeClass('disabled');
        nextTab($active);
        var remoteDataLoader = window.DevExpress.data.AspNet.createStore({
            key: "paymentRefNumber",
            loadUrl: baseUrl + 'v1.0/CreditNote/search-by-paymentref',
            onBeforeSend: function (operation,
                ajaxSettings) {
                ajaxSettings.beforeSend = function (xhr) {
                    xhr.setRequestHeader('paymentRef', paymentRef);
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
                        dataField: "payername",
                        caption: "Name",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "bankName",
                        caption: "Bank",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "branchname",
                        caption: "Branch",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "amount",
                        caption: "Amount",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "revenueCode",
                        caption: "Revenue Code",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "revenueName",
                        caption: "Revenue Name",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    },
                    {
                        dataField: "agencyName",
                        caption: "Agency Name",
                        sortIndex: 0,
                        sortOrder: 'asc',
                        //fixed: true,
                        cssClass: 'font-bold'

                    }
                ],

            };

        dataGrid = window.$("#seacrhByPaymentRefDiv").dxDataGrid(gridOptions).dxDataGrid("instance");
    }
});

$(document).on({
    ajaxStart: function () {
        $('#cover-spin').show(0);
    },
    ajaxStop: function () {
        $('#cover-spin').hide();
    }
});