﻿/* ********************************************************
* By Abel S. Salvatierra
* @2017 - Real Breeze Travel & Tours
* 
*********************************************************** */

$(document).ready(function () {
    InitDatePicker();
})


function InitDatePicker()
{
    var ddd1 = $('input[name="dtDone"]').val();
    var ddd2 = $('input[name="NextSched"]').val();

    $('input[name="dtDone"]').daterangepicker(
    {
        timePicker: false,
        timePickerIncrement: 30,
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'MM/DD/YYYY'
        }
    },
    function (start, end, label) {
        //alert(start.format('YYYY-MM-DD h:mm A'));
        
    }
    );


    $('input[name="NextSched"]').daterangepicker(
    {
        timePicker: false,
        timePickerIncrement: 30,
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'MM/DD/YYYY'
        }
    },
    function (start, end, label) {
        //alert(start.format('YYYY-MM-DD h:mm A'));
        
    }
    );

    $('input[name="dtDone"]').val(ddd1.substr(0, ddd1.indexOf(" ")));
    $('input[name="NextSched"]').val(ddd2.substr(0, ddd1.indexOf(" ")));




}
