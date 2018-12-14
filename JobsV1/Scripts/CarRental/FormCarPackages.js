
/******** Form Packages ************/

var selectedTour;
$(document).ready(function () {
    $('[data-toggle="popover"]').popover({
        placement: 'right',
        html: 'true',
        content: '<button class="btn btn-primary" onclick="FormPackages_NextForm();">Proceed</button>'
    });

    $('[data-toggle="popover"]').on('click', function (e) {
        $('[data-toggle="popover"]').not(this).popover('hide');
    });

});

function formPackages_TransText(carId, days, meals, fuel, RentTypeTxt, carDesc) {

    var carId = carId;
    var noDays = days;
    var mealsAcc = meals;
    var fuel = fuel;

    var mealPackage = mealsAcc == '1' ? 'Driver Meals/Accomodation Included ' : 'Driver Meals/Accomodation by renter';
    var fuelPackage = fuel == '1' ? 'Driver Fuel Included ' : 'Driver Fuel by renter';
    $('#modal-text-foot').text(RentTypeTxt + ' - ' + carDesc + ' - ' + noDays + ' Days - ' + mealPackage + ' - ' + fuelPackage + '');
}


//public PartialViewResult FormPackages (int? carId, int? days, int? rentType, int? meals, int? fuel)
function FormPackages_Next(controller,carId, noDays, rentalType, meals, fuel, pkg) {
    window.location.href = '/' + controller + '/FormSummary?carId=' + carId + '&days=' + noDays +
        '&rentType=' + rentalType + '&meals=' + meals + '&fuel=' + fuel + '&pkg=' + pkg;
}

//public PartialViewResult FormPackages (int? carId, int? days, int? rentType, int? meals, int? fuel)
function FormPackages_Prev(carId, noDays, rentalType, meals, fuel, pkg) {
    window.location.href = '/' + controller + '/CarReserve?id=' + carId + '&days=' + noDays +
        '&rentType=' + rentalType + '&meals=' + meals + '&fuel=' + fuel + '&pkg=' + pkg;
}


function updateTransaction() {
    var mealPackage = $('#rsv-meal').val() == '1' ? 'Driver Meals/Accomodation Included ' : 'Driver Meals/Accomodation by renter';
    var fuelPackage = $('#rsv-fuel').val() == '1' ? 'Driver Fuel Included ' : 'Driver Fuel by renter';
    $('#modal-text-foot').text($('input:radio[name=options]:checked').val() + ' - ' + $('input:radio[name=cars]:checked').val() +
    ' - ' + $('#rsv-days').val() + ' Days - ' + mealPackage + ' - ' + fuelPackage);

}

//packages selection on click
$('#pkg-table').on('click', '.clickable-row', function (event) {
    $(this).addClass('active').siblings().removeClass('active');
    selectedTour = $(this).attr('id').substring(4, 6);
    //console.log(selectedTour);
});
