
$(document).ready(function () {
    var token = $('#jwtToken').val();
    var bearerToken = 'Bearer ' + token;
    var baseUrl = $('#ApiBaseUrl').val();
    var menuSetupId = $('#menuSetupId').val();
    var merchantCode = $('#merchantCode').val();

    $('#actionLoader').hide();
    var that = this;
    //domainName = document.querySelector("#domainNameId").value;




    $('#btn_AddCreditNoteRequest').on('click', function () {
        $('#btn_AddCreditNoteRequest').attr('disabled', 'disabled');
        //let reason = document.getElementById("Reason").value;
        let proposedAmount = document.getElementById("ProposedAmount").value;

        //if (reason == "" || reason == null) {
        //    $('#Reason').css('border-color', 'Red');
        //    $('#Reason').focus();
        //    $('#btn_AddCreditNoteRequest').removeAttr('disabled');
        //    ShowMessagePopup("Oops!", "Please enter a reason for this request", "warning");

        //    return false;
        //}
        if (proposedAmount == "" || proposedAmount == null) {
            $('#ProposedAmount').css('border-color', 'Red');
            $('#ProposedAmount').focus();
            $('#btn_AddCreditNoteRequest').removeAttr('disabled');
            ShowMessagePopup("Oops!", "Please enter a proposed amount", "warning");

            return false;
        }
        else {
            AddCreditNoteRequest();
        }

    });



  
    $('#searchAssessmentRefButton').on('click', function () {
        $('#searchAssessmentRefButton').attr('disabled', 'disabled');
        let assessmentNo = document.getElementById("assessmentNo").value;

        if (assessmentNo == "" || assessmentNo == null) {
            $('#assessmentNo').css('border-color', 'Red');
            $('#assessmentNo').focus();
            $('#searchAssessmentRefButton').removeAttr('disabled');
            ShowMessagePopup("Oops!", "Assessment Reference Number Cannot be Empty", "warning");

            return false;
        }
        SearchAssessmentRef(assessmentNo);
    });


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
                if (response.succeeded) {
                    document.getElementById('PayerName').innerHTML = response.data.payerName;
                    document.getElementById('PlatformName').innerHTML = response.data.platformcode;
                    document.getElementById('Utin').innerHTML = response.data.agentUtin;


                    document.getElementById('AmountAccessed').innerHTML = response.data.amountAccessed;
                    document.getElementById('RevenueName').innerHTML = response.data.revenueName;
                    document.getElementById('TotalAmount').innerHTML = response.data.totalAmount;
                    document.getElementById('AmountPaid').innerHTML = response.data.amountPaid;
                    document.getElementById('RevenueCode').innerHTML = response.data.revenueCode;
                    document.getElementById('AgencyName').innerHTML = response.data.agencyName;
                    document.getElementById('TaxYear').innerHTML = response.data.taxYear;
                    document.getElementById('DateCreated').innerHTML = response.data.dateCreated;
                    document.getElementById('AssessmentCreatedBy').innerHTML = response.data.assessmentCreatedBy;

                    $("#AssessmentInfoDiv").show();
                    $("#ProposedAmountDiv").show();
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
                            $("#AssessmentInfoDiv").hide();
                            $("#ProposedAmountDiv").hide();
                            $('#searchAssessmentRefBtn').removeAttr('disabled');
                        });
                    $("#AssessmentInfoDiv").hide();
                    $("#ProposedAmountDiv").hide();
                    $('#searchAssessmentRefButton').removeAttr('disabled');
                }
            }
        });
    }

    function AddCreditNoteRequest() {

        var AddCreditNoteRequest = {
            MenuSetupId: parseInt(menuSetupId),
            PaymentReferenceNumber: "",
            AssessmentReferenceNumber: $('#assessmentNo').val(),
            ActualAmount: parseFloat(document.getElementById("TotalAmount").innerHTML),
            AmountUsed: parseFloat($('#ProposedAmount').val()),
            Balance: 0,
            MerchantCode: merchantCode,
            TransType: "2"
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
                            document.getElementById("addProposedAmountForm").reset();
                            document.getElementById("searchByAssessmentForm").reset();
                            window.location.reload();
                        });
                }

            },
            error: function (xhr) {
                window.location.reload();
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
        $('#cover-spin').hide();
    }
});