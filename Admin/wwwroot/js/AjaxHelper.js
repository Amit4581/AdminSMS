
function Post(url, data) {
    var result;
    $("#Loder").show();
    $.ajax({
        url: url,
        dataType: "json",
        data: JSON.stringify(data),
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",

        success: function (response) {
            result = response
            $("#Loder").hide();
        },
        error: function (errormessage) {
            result = errormessage;
            $("#Loder").hide();
        }
    });
    // debugger;
    return result;
}



function ModelPost(url, data) {
    $("#Loder").show();
    var result;
    $.ajax({
        url: url,
        dataType: "json",
        data: data,
        type: "POST",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',

        success: function (response) {
            result = response
            $("#Loder").hide();
        },
        error: function (errormessage) {
            result = errormessage;
            $("#Loder").hide();
        }
    });
    // debugger;
    return result;
}
function Get(url) {
    $("#Loder").show();
    var result;
    $.ajax({
        url: url,
        dataType: "json",
        type: "Get",
        contentType: "application/json; charset=utf-8",
        async: false,

        success: function (response) {
            result = response
            $("#Loder").hide();
        },
        error: function (errormessage) {
            result = errormessage;
            $("#Loder").hide();
        }
    });
    return result;
}
function ValidateResponse(response) {
    return response != "" && response != undefined && response.statusCode == 200;
}





