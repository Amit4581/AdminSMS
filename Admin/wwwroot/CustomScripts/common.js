"use strict";
$(document).ready(function () {
    (function () {
        $.fn.serializeObject = function () {
            var o = {};
            var a = this.serializeArray();
            $.each(a, function () {
                if (o[this.name]) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
            return o;
        };
    })(jQuery)
     
});
function populateDropdown(id, data) {
    var idval = "";
    $('#' + id).empty();
    for (var i of data) {
        if (i.Selected==true) {
            idval = i.Value;
        }
        if (i.Selected == true) {
            $('#' + id).append('<option value="' + i.Value + '" selected="' + i.Selected + '">' + i.Text + '</option>');
        }
        else {
            $('#' + id).append('<option value="' + i.Value + '">' + i.Text + '</option>');
        }
    }
    if (idval!="") {
        $('#' + id).val(idval);
        $('#' + id).val($('#' + id).val()).trigger('change');
    }
}
function validateDropdown(id, name) {
    if (! $("#" + id).val()) {
        swal("Please Select " + name).then(() => {
            $("#" + id).focus();
        });
        event.preventdefault();
        return ;
    }
    else if ($("#" + id).val().trim() == "") {
        swal("Please Select " + name).then(() => {
            $("#" + id).focus();
        });
        event.preventdefault();
        return ;
    }
    else if ($("#" + id).val().trim() == "-1") {
        swal("Please Select " + name).then(() => {
            $("#" + id).focus();
        });
        event.preventdefault();
        return ;
    }    
}
function validateTextbox(id, name) {
    if (! $("#" + id).val()) {
        swal("Please Enter " + name).then(() => {
            $("#" + id).focus();
        });
        event.preventdefault();
        return;
    }
    else if ($("#" + id).val().trim() == "") {
        swal("Please Enter " + name).then(() => {
            $("#" + id).focus();
        });
        event.preventdefault();
        return;
    }
}
var specialKeys = new Array();
specialKeys.push(47); // for Forward Slash(/)
specialKeys.push(8); //Backspace
specialKeys.push(9); // Tab
//specialKeys.push(46); // for .
specialKeys.push(37); // for left Arrow
specialKeys.push(39); // for Right Arrow
function IsNumeric(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1); // For Decimal To All Number
    if (ret == false) {
        alert("Only numaric value allwed.");
    }
    return ret;
}
$(document).ready(function () {
    $('.select2').select2({
        closeOnSelect: true
    });
});
$('.datepicker').datepicker({
    weekStart: 1,
    format: 'dd/mm/yyyy',
    daysOfWeekHighlighted: "6,0",
    autoclose: true,
    todayHighlight: true,
    minDate: 0,
    startDate: new Date(2020,0,1),
    endDate: new Date(2100,0,1),
});
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})