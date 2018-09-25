﻿
//globals
var currentForm = "Modal";
var selectedCar = "";
var RentalType = "With Driver";
var NoDays = 0;
var MealsAcc = 0;
var Fuel = 0;
var selectedTour = 0;
var reservationNum = 0;

//initial-default
$('#withdriver').attr('checked', true);
$('#car' + $('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue');
$('#modal-text-foot').text($('input:radio[name=options]:checked').val() + ' - ' + $('input:radio[name=cars]:checked').val());
$('#pkg-1').attr('checked', true);
$('#pkg-1').addClass('active').siblings().removeClass('active');
selectedTour =  $('#pkg-1').attr('id');

$('#btn-rentalType').click(function () {
    $('#modal-text-foot').text($('input:radio[name=options]:checked').val() + ' - ' + $('input:radio[name=cars]:checked').val());

    if ($('input:radio[name=options]:checked').val() == "Self Drive") {
        $('.isSelfDrive1').hide();
    } else {
        $('.isSelfDrive1').show();
    }
});

//
$('#btn-rentalUnit').click(function () {
    $('#modal-text-foot').text($('input:radio[name=options]:checked').val() + ' - ' + $('input:radio[name=cars]:checked').val());

    $('#car1').css('border', '1px solid lightgray');
    $('#car2').css('border', '1px solid lightgray');
    $('#car3').css('border', '1px solid lightgray');
    $('#car4').css('border', '1px solid lightgray');
    $('#car5').css('border', '1px solid lightgray');
    $('#car6').css('border', '1px solid lightgray');

    $('#car' + $('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue');

});

//controls the flow of reservation forms
//display the next form
function showForm() {

    RentalType = $('input:radio[name=options]:checked').val();
    selectedCar = $('input:radio[name=cars]:checked').attr("id");
    NoDays = $('#rsv-days').val();
    MealsAcc = $('#rsv-meal').val();
    Fuel = $('#rsv-fuel').val();

    //self drive
    if ($('input:radio[name=options]:checked').val() == "Self Drive") {
        selfDrive($('input:radio[name=cars]:checked').attr("id"));
    }

    switch (currentForm) {
        case "Modal": //next FormReservation
            currentForm = "FormReserve";
            $('#modal2-content').css('display', 'none');
            $('#formReserve').css('display', 'block');
            $('#formPackages').css('display', 'none');
            $('#formSummary').css('display', 'none');
            $('#formRenter').css('display', 'none');
            $('#formFinish').css('display', 'none');
            $('#rsv-footer').css('display','block');
            break;
        case "FormReserve": //next FormPackages
            currentForm = "FormPackages";
            $('#modal2-content').css('display', 'none');
            $('#formReserve').css('display', 'none');
            $('#formPackages').css('display', 'block');
            $('#formSummary').css('display', 'none');
            $('#formRenter').css('display', 'none');
            $('#formFinish').css('display', 'none');
            $('#rsv-footer').css('display','block');
            displayPackages();
            break;
        case "FormPackages": //next formSummary
            currentForm = "formSummary";
            $('#modal2-content').css('display', 'none');
            $('#formPackages').css('display', 'none');
            $('#formReserve').css('display', 'none');
            $('#formSummary').css('display', 'block');
            $('#formRenter').css('display', 'none');
            $('#formFinish').css('display', 'none');
            $('#rsv-footer').css('display','block');
            displaySummary();
            break;
        case "formSummary":  //next formRenter
            currentForm = "formRenter";
            $('#modal2-content').css('display', 'none');
            $('#formPackages').css('display', 'none');
            $('#formReserve').css('display', 'none');
            $('#formSummary').css('display', 'none');
            $('#formRenter').css('display', 'block');
            $('#formFinish').css('display', 'none');
            $('#rsv-footer').css('display', 'none');
            setReferenceIds(selectedTour.substring(4, 5), MealsAcc, Fuel);
            break;
        case "formRenter":  //next formThankyou
            currentForm = "formThankyou";
            $('#modal2-content').css('display', 'none');
            $('#formPackages').css('display', 'none');
            $('#formReserve').css('display', 'none');
            $('#formSummary').css('display', 'none');
            $('#formRenter').css('display', 'none');
            $('#formFinish').css('display', 'block');
            $('#rsv-footer').css('display','none');
            break;
    }

    $('#FormPackages_carid').val($('input:radio[name=cars]:checked').attr("id"));
}

//displays the vehicle selection form
function backtoMain() {
    currentForm = "Modal";
    $('#modal2-content').css('display', 'block');
    $('#formReserve').css('display', 'none');
    $('#formPackages').css('display', 'none');
    $('#formSummary').css('display', 'none');
    $('#formRenter').css('display', 'none');
    $('#formFinish').css('display', 'none');
    $('#rsv-footer').css('display','block');

}

//display the previous form
function backtoPrev() {

    switch (currentForm) {
        case "Modal": //prev none
            currentForm = "Modal";
            $('#modal2-content').css('display', 'block');
            $('#formReserve').css('display', 'none');
            $('#formPackages').css('display', 'none');
            $('#formSummary').css('display', 'none');
            $('#formRenter').css('display', 'none');
            break;
        case "FormReserve": //prev Modal
            currentForm = "Modal";
            $('#modal2-content').css('display', 'block');
            $('#formReserve').css('display', 'none');
            $('#formPackages').css('display', 'none');
            $('#formSummary').css('display', 'none');
            $('#formRenter').css('display', 'none');
            break;
        case "FormPackages":  //next form summary
            currentForm = "FormReserve";
            $('#modal2-content').css('display', 'none');
            $('#formReserve').css('display', 'block');
            $('#formPackages').css('display', 'none');
            $('#formSummary').css('display', 'none');
            $('#formRenter').css('display', 'none');
            break;
        case "formSummary":  //next back to main
            currentForm = "FormPackages";
            $('#modal2-content').css('display', 'none');
            $('#formReserve').css('display', 'none');
            $('#formPackages').css('display', 'block');
            $('#formSummary').css('display', 'none');
            $('#formRenter').css('display', 'none');
            break;
        case "formRenter":  //next summary
            currentForm = "formSummary";
            $('#modal2-content').css('display', 'none');
            $('#formPackages').css('display', 'none');
            $('#formReserve').css('display', 'none');
            $('#formSummary').css('display', 'block');
            $('#formRenter').css('display', 'none');
            break;
    }
}


function submitRenter(){

    alert('submit');
    data = {
        Id : 0,
        DtTrx : '9/22/2018',
        CarUnitId : '1',
        DtStart : '9/22/2018',
        LocStart : 'somewhere',
        DtEnd : '9/22/2018',
        LocEnd : 'somewhere',
        BaseRate : '2000',
        Destinations : 'Samal Tour',
        UseFor : 'somewhere',
        RenterName : 'somewhere',
        RenterCompany : 'somewhere',
        RenterEmail : 'somewhere@gmail.com',
        RenterMobile : '09123456',
        RenterAddress : '',
        RenterFbAccnt : '',
        RenterLinkedInAccnt : '',
        EstHrPerDay : '',
        EstKmTravel : ''
    }


    var serviceURL = '/CarRental/FormRenterPOST';

    $.ajax({
        type: "POST",
        url: serviceURL,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc(),
        error: errorFunc()
    });
}

function ajaxSubmitData() {
    var data = {
        reservationid: reservationNum,
        pacakgeId: selectedTour,
        mealAcc: MealsAcc,
        fuel: Fuel
    }
    var serviceURL = '/SMSWebService.asmx/reservationPkg';

    $.ajax({
        type: "POST",
        url: serviceURL,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(){
            console.log('success');
        },
        error: function(jqXHR, textStatus, errorThrown) {
            console.log('An error occurred... Look at the console (F12 or Ctrl+Shift+I, Console tab) for more information!');

            $('#result').html('<p>status code: '+jqXHR.status+'</p><p>errorThrown: ' + errorThrown + '</p><p>jqXHR.responseText:</p><div>'+jqXHR.responseText + '</div>');
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function selfDrive(id){
    window.location.href = '/CarRental/Reservation?unitid='+id ;
}

function setReferenceIds(RntrSelectedTour, RntrMealsAcc, RntrFuel) {
    $('#dtls-packageId').val(RntrSelectedTour);
    $('#dtls-mealsAcc').val(RntrMealsAcc);
    $('#dtls-fuel').val(RntrFuel);
}


$('#pkg-table').on('click', '.clickable-row', function(event) {
    $(this).addClass('active').siblings().removeClass('active');
    selectedTour =  $(this).attr('id');
});