////$(document).on({
////    ajaxStart: function () {
////        $("#overlay").fadeIn(300);
////    },
////    ajaxStop: function () {
////        $("#overlay").fadeOut(300);
////    }
////});

function getAndSetProfilePic() {
    const type = 1 //EmployeeProfileImage
    $.ajax({
        url: `${urls.getImage}/${type}`,
        method: "GET",
        dataType: "json",
        contentType: "application/json",
        headers: {
            "Access-Control-Allow-Origin": "*",
            "Authorization": bearerToken,
        },
        success: function (data) {
            if (data.statusCode == 200) {
                if (data.data) {
                    if (data.data.employeeImage != null) {
                        const userId = $("#userId").val();
                        setLocalStorage(`profilePic_${userId}`, data.data.employeeImage)
                        $("#profilePic").attr("src", data.data.employeeImage);
                        $("#bigProfilePic").attr("src", data.data.employeeImage);
                    }
                }
            }
        },
        error: function (err) {
            apiErrorHandler(err);
        }
    });
}

function getAndSetCompanyLogo() {
    const type = 2 //CompanyLogoId
    $.ajax({
        url: `${urls.getImage}/${type}`,
        method: "GET",
        dataType: "json",
        contentType: "application/json",
        headers: {
            "Access-Control-Allow-Origin": "*",
            "Authorization": bearerToken,
        },
        success: function (data) {
            if (data.statusCode == 200) {
                if (data.data) {
                    if (data.data.companyLogoImage != null) {
                        const companyId = $("#companyId").val();
                        setLocalStorage(`companyLogo_${companyId}`, data.data.companyLogoImage)
                        $("#companyLogo").attr("src", data.data.companyLogoImage);
                    }
                }
            }
        },
        error: function (err) {
            apiErrorHandler(err);
        }
    });
}

function setProfilePic() {
    const userId = $("#userId").val();
    const profilePic = getLocalStorage(`profilePic_${userId}`)
    if (profilePic != null || profilePic != undefined) {
        $("#profilePic").attr("src", profilePic);
        $("#bigProfilePic").attr("src", profilePic);
    }
}

function setCompanyLogo() {
    const companyId = $("#companyId").val();
    const pic = getLocalStorage(`companyLogo_${companyId}`)
    if (pic != null || pic != undefined) {
        $("#companyLogo").attr("src", pic);
    }
}

function saveImage(type, img) {
    try {
        img = img[0];
        if (img.files && img.files[0]) {
            if (img.files[0].size > 200177) {
                toastNotify("File size can not be more than 200kb", "warning")
                return false
            }

            var FR = new FileReader();

            FR.addEventListener("load", function (e) {
                var base64Pic = e.target.result;
                const model = {
                    imageFile: base64Pic,
                    tableType: type
                }
                swal(confirmSwalText, function (res) {
                    if (res) {
                        $.ajax({
                            url: urls.addImage,
                            method: "POST",
                            data: JSON.stringify(model),
                            dataType: "json",
                            contentType: "application/json",
                            headers: {
                                "Access-Control-Allow-Origin": "*",
                                "Authorization": bearerToken,
                            },
                            success: function (data) {
                                if (data.succeeded) {
                                    swal("Success!", "Picture update successfully.", "success");
                                    type == 2 ? getAndSetCompanyLogo() : getAndSetProfilePic();
                                } else {
                                    toastNotify(data.message, "error");
                                }
                            },
                            error: function (err) {
                                apiErrorHandler(err);
                            }
                        });
                    }
                });

            });
            FR.readAsDataURL(img.files[0]);
        } else {
            toastNotify("No File Selected", "warning")
        }

    } catch (ex) {
        errorHandler(ex)
    }
}

function toastNotify(msg = "No Message", type = "info") {
    let bgColor = ""
    switch (type) {
        case "success":
            bgColor = "alert-success"
            break
        case "error":
            bgColor = "alert-danger"
            break
        case "warning":
            bgColor = "alert-warning"
            break
        case "info":
            bgColor = "alert-info"
            break

        default:
            bgColor = "alert-info"
    }
    showNotification(bgColor, msg, "top", "center");
}

function errorHandler(error) {
    console.error(error)
    toastNotify("Error Dey Faa", "danger");
}

function apiErrorHandler(error) {
    console.log("error", error);
    $("#overlay").fadeOut(300);
    switch (error.status) {
        case 400:
            toastNotify(error.responseJSON.title ? error.responseJSON.title : error.responseJSON.message, "error");
            break

        case 500:
            toastNotify(errorMessage.error500, "error");
            break

        case 404:
            toastNotify(errorMessage.error404, "error");
            break

        case 401:
            toastNotify(errorMessage.error401, "error");
            window.location = baseUrl + "/Auth/Login"
            break

        case 409:
            toastNotify(error.responseJSON.title ? error.responseJSON.title : error.responseJSON.message, "error");
            break

        case 403:
            toastNotify(error.responseJSON.title ? error.responseJSON.title : error.responseJSON.Message, "error");
            break


        default:
            toastNotify(errorMessage.default, "error");
            break
    }
}

function validatePassword(password) {
    let pattern = /^(?=.{5,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])/;
    let checkval = pattern.test(password);
    return checkval;
}

function IsEmail(email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test(email);
}

function ExcelDateToJSDate(serial) {
    var utc_days = Math.floor(serial - 25569);
    var utc_value = utc_days * 86400;
    var date_info = new Date(utc_value * 1000);

    var fractional_day = serial - Math.floor(serial) + 0.0000001;

    var total_seconds = Math.floor(86400 * fractional_day);

    var seconds = total_seconds % 60;

    total_seconds -= seconds;

    var hours = Math.floor(total_seconds / (60 * 60));
    var minutes = Math.floor(total_seconds / 60) % 60;
    let y = date_info.getFullYear();
    let m = date_info.getMonth() + 1;
    m = m < 10 ? `0${m}` : m ;
    let d = date_info.getDate() < 10 ? `0${date_info.getDate()}` : date_info.getDate();

    return `${y}-${m}-${d}`;

/*    return new Date(date_info.getFullYear(), date_info.getMonth(), date_info.getDate(), hours, minutes, seconds);*/
}

function formatDate(date) {
    return (new Date(date)).toDateString();
}

function formatDateForTextBox(date) {
    return date.split("T")[0]
}

function readAndPreviewImg(input, containerId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#' + containerId).css('background-image', 'url(' + e.target.result + ')');
            $('#' + containerId).hide();
            $('#' + containerId).fadeIn(650);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function setLocalStorage(key, value) {
    localStorage.setItem(key, JSON.stringify(value));
}

function getLocalStorage(key) {
    return JSON.parse(localStorage.getItem(key));
}

function deleteLocalStorage(key) {
    localStorage.removeItem(key);
}

function getFormattedDate(date) {
    var year = date.getFullYear();
    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;
    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;
    return day + '/' + month + '/' + year;
}

function getMonthYearDate(date) {
    var year = date.getFullYear();
    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;
    //var day = date.getDate().toString();
    //day = day.length > 1 ? day : '0' + day;
    return month + '/' + year;
}

function getFormattedLongDate(date) {
    var monthNames = [
        "January", "February", "March",
        "April", "May", "June", "July",
        "August", "September", "October",
        "November", "December"
    ];

    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    return day + ' ' + monthNames[monthIndex] + ' ' + year;
}

function getMonthNameYearDate(date) {
    var monthNames = [
        "January", "February", "March",
        "April", "May", "June", "July",
        "August", "September", "October",
        "November", "December"
    ];
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    return monthNames[monthIndex] + ' ' + year;
}

function getFormattedDateTime(date) {
    //var hours = date.getHours();
    //var minutes = date.getMinutes();
    //var ampm = hours >= 12 ? 'pm' : 'am';
    //hours = hours % 12;
    //hours = hours ? hours : 12; // the hour '0' should be '12'
    //minutes = minutes < 10 ? '0' + minutes : minutes;
    //var strTime = hours + ':' + minutes + ' ' + ampm;

    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = date.getHours() >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return getFormattedDate(date) + " " + strTime;
}

function amountRoundToTwo(num) {
    return +(Math.round(num + "e+2") + "e-2");
}

function amountRoundUp(num, places) {
    return +(Math.round(num + "e+" + places) + "e-" + places);
}

function formatMoney(n, currency = "₦") {
    return currency + "" + (amountRoundToTwo(n) + '').replace(/./g, function (c, i, a) {
        return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
    });
}

function browserBack() {
    // broswer hack to disable back button
    history.pushState(null, null, location.href);
    window.addEventListener("popstate", function () {
        history.pushState(null, null, location.href);
    });
}

function removeFormat(str) {
    if (!str) return str;
    return str.replace(/[^0-9-.]/g, '');
}

function getFloatFromValue(inputValue) {
    return removeFormat(inputValue) === ''
        ? 0
        : parseFloat(removeFormat(inputValue));
}

function amountRoundToTwo(num) {
    return +(Math.round(num + "e+2") + "e-2");
}

var Base64 = (function () {
    "use strict";

    var _keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

    var _utf8_encode = function (string) {

        var utftext = "", c, n;

        string = string.replace(/\r\n/g, "\n");

        for (n = 0; n < string.length; n++) {

            c = string.charCodeAt(n);

            if (c < 128) {

                utftext += String.fromCharCode(c);

            } else if ((c > 127) && (c < 2048)) {

                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);

            } else {

                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);

            }

        }

        return utftext;
    };

    var _utf8_decode = function (utftext) {
        var string = "", i = 0, c = 0, c1 = 0, c2 = 0;

        while (i < utftext.length) {

            c = utftext.charCodeAt(i);

            if (c < 128) {

                string += String.fromCharCode(c);
                i++;

            } else if ((c > 191) && (c < 224)) {

                c1 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c1 & 63));
                i += 2;

            } else {

                c1 = utftext.charCodeAt(i + 1);
                c2 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c1 & 63) << 6) | (c2 & 63));
                i += 3;

            }

        }

        return string;
    };

    var _hexEncode = function (input) {
        var output = '', i;

        for (i = 0; i < input.length; i++) {
            output += input.charCodeAt(i).toString(16);
        }

        return output;
    };

    var _hexDecode = function (input) {
        var output = '', i;

        if (input.length % 2 > 0) {
            input = '0' + input;
        }

        for (i = 0; i < input.length; i = i + 2) {
            output += String.fromCharCode(parseInt(input.charAt(i) + input.charAt(i + 1), 16));
        }

        return output;
    };

    var encode = function (input) {
        var output = "", chr1, chr2, chr3, enc1, enc2, enc3, enc4, i = 0;

        input = _utf8_encode(input);

        while (i < input.length) {

            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }

            output += _keyStr.charAt(enc1);
            output += _keyStr.charAt(enc2);
            output += _keyStr.charAt(enc3);
            output += _keyStr.charAt(enc4);

        }

        return output;
    };

    var decode = function (input) {
        var output = "", chr1, chr2, chr3, enc1, enc2, enc3, enc4, i = 0;

        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < input.length) {

            enc1 = _keyStr.indexOf(input.charAt(i++));
            enc2 = _keyStr.indexOf(input.charAt(i++));
            enc3 = _keyStr.indexOf(input.charAt(i++));
            enc4 = _keyStr.indexOf(input.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output += String.fromCharCode(chr1);

            if (enc3 !== 64) {
                output += String.fromCharCode(chr2);
            }
            if (enc4 !== 64) {
                output += String.fromCharCode(chr3);
            }

        }

        return _utf8_decode(output);
    };

    var decodeToHex = function (input) {
        return _hexEncode(decode(input));
    };

    var encodeFromHex = function (input) {
        return encode(_hexDecode(input));
    };

    return {
        'encode': encode,
        'decode': decode,
        'decodeToHex': decodeToHex,
        'encodeFromHex': encodeFromHex
    };
}());
//Ishola code above

Date.prototype.formatDate = function (format) {
    var date = this;
    if (!format)
        format = "MM/dd/yyyy";

    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    format = format.replace("MM", month.toString().padL(2, "0"));

    if (format.indexOf("yyyy") > -1)
        format = format.replace("yyyy", year.toString());
    else if (format.indexOf("yy") > -1)
        format = format.replace("yy", year.toString().substr(2, 2));

    format = format.replace("dd", date.getDate().toString().padL(2, "0"));

    var hours = date.getHours();
    if (format.indexOf("t") > -1) {
        if (hours > 11)
            format = format.replace("t", "pm")
        else
            format = format.replace("t", "am")
    }
    if (format.indexOf("HH") > -1)
        format = format.replace("HH", hours.toString().padL(2, "0"));
    if (format.indexOf("hh") > -1) {
        if (hours > 12) hours - 12;
        if (hours === 0) hours = 12;
        format = format.replace("hh", hours.toString().padL(2, "0"));
    }
    if (format.indexOf("mm") > -1)
        format = format.replace("mm", date.getMinutes().toString().padL(2, "0"));
    if (format.indexOf("ss") > -1)
        format = format.replace("ss", date.getSeconds().toString().padL(2, "0"));
    return format;
};

// Read a page's GET URL variables and return them as an associative array.
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}