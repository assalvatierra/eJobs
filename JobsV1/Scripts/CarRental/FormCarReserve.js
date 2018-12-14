

/******** Car Reserve ************/

//public PartialViewResult FormPackages (int? carId, int? days, int? rentType, int? meals, int? fuel)
function CarReserve_FormPackages(controller,carId, noDays, rentalType, meals, fuel) {
    window.location.href = '/' + controller + '/FormPackages?carId=' + carId + '&days=' + noDays +
        '&rentType=' + rentalType + '&meals=' + meals + '&fuel=' + fuel;
}

//clear border highlight
function CarBorderReset() {
    $('#car1').css('border', '1px solid lightgray');
    $('#car2').css('border', '1px solid lightgray');
    $('#car3').css('border', '1px solid lightgray');
    $('#car4').css('border', '1px solid lightgray');
    $('#car5').css('border', '1px solid lightgray');
    $('#car6').css('border', '1px solid lightgray');
    $('#car' + $('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue');
}


function selfDrive(id){
   window.location.href = '/CarRental/Reservation?unitid='+id ;
}

function CarReserve_Default(carid, days, meals, fuelId) {

    $('#car' + $('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue');
    $('#withdriver').attr('checked', true);

    var carid = carid;
    $("#" + carid + "").attr('checked', true);
    CarBorderReset();

    $('#rsv-days').val(days);

    //meals and accomodation
    if (meals == 1) {
        $('#rsv-meal').val("1");
    } else {
        $('#rsv-meal').val("0");
    }

    //fuel inclusion
    if (fuelId == 1) {
        $('#rsv-fuelId').val("1");
    } else {
        $('#rsv-fuel').val("0");
    }

    updateTransaction();

}


function rentalTypeChange() {

    if ($('input:radio[name=options]:checked').val() == "Self Drive") {
        if ($('input:radio[name=cars]:checked').attr("id") == 1 || $('input:radio[name=cars]:checked').attr("id") == 2) {
            radiobtn = document.getElementById("3");
            radiobtn.checked = true;
            //
        }

        $('#car' + $('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue');

        $('.isSelfDrive1').hide();
    } else {

        $('#1').attr('checked', true);
        $('#car' + $('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue');

        $('.isSelfDrive1').show();
    }

    updateTransaction();
}

function selfDrive(id) {
    window.location.href = '/CarRental/Reservation?unitid=' + id;
}

$('#btn-rentalType').click(function () {
    updateTransaction();
    rentalReset();
    $('#3').attr('checked', true);

    if ($('input:radio[name=options]:checked').val() == "Self Drive") {
        if ($('input:radio[name=cars]:checked').attr("id") == 1) {
            $('#3').attr('checked', true);
        }

        $('#rental-type-sdrive').addClass('active').siblings().removeClass('active');

        $('#car' + $('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue');

        updateTransaction();
        $('.isSelfDrive1').hide();

    } else {

        $('#rental-type-wdriver').addClass('active').siblings().removeClass('active');

        $('#1').attr('checked', true);

        $('#car' + $('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue');
        updateTransaction();
        $('.isSelfDrive1').show();
    }
});

//meal selection on click
$('#rsv-meal').click(function () {
    updateTransaction();
});
//fuel selection on click
$('#rsv-fuel').click(function () {
    updateTransaction();
});

//carSelection on click
$('#btn-rentalUnit').click(function () {
    updateTransaction();

    //$('#car' + $('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue');
    $('#car' + +$('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue').siblings().css('border', '1px solid lightgray');
});

function updateTransaction() {
    var mealPackage = $('#rsv-meal').val() == '1' ? 'Driver Meals/Accomodation Included ' : 'Driver Meals/Accomodation by renter';
    var fuelPackage = $('#rsv-fuel').val() == '1' ? 'Driver Fuel Included ' : 'Driver Fuel by renter';
    $('#modal-text-foot').text($('input:radio[name=options]:checked').val() + ' - ' + $('input:radio[name=cars]:checked').val() +
    ' - ' + $('#rsv-days').val() + ' Days - ' + mealPackage + ' - ' + fuelPackage);

}


function rentalReset() {
    $('#modal-text-foot').text($('input:radio[name=options]:checked').val() + ' - Please select a vehicle');

    var selectedCar = "";
}


//handle the count of number of days 
$('#days-add').click(function () {
    $('#rsv-days').val(+$('#rsv-days').val() + 1);
    updateTransaction();
});
$('#days-sub').click(function () {
    if ($('#rsv-days').val() > 1) {
        $('#rsv-days').val(+$('#rsv-days').val() - 1);
        updateTransaction();
    }
});

function dayschange() {

    if ($('#rsv-days').val() < 1) {
        $('#rsv-days').val(1);
    }
    updateTransaction();

}