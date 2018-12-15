

/******** Car Reserve ************/
//redirect to Packages selection form
function CarReserve_FormPackages(controller,carId, noDays, rentalType, meals, fuel) {
    window.location.href = '/' + controller + '/FormPackages?carId=' + carId + '&days=' + noDays +
        '&rentType=' + rentalType + '&meals=' + meals + '&fuel=' + fuel;
}

//clear border highlight on car selection
function CarBorderReset() {
    $('#car1').css('border', '1px solid lightgray');
    $('#car2').css('border', '1px solid lightgray');
    $('#car3').css('border', '1px solid lightgray');
    $('#car4').css('border', '1px solid lightgray');
    $('#car5').css('border', '1px solid lightgray');
    $('#car6').css('border', '1px solid lightgray');
    $('#car' + $('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue');
}

//selfdrive: redirect to self drive reservation form
function selfDrive(id){
   window.location.href = '/CarRental/Reservation?unitid='+id ;
}

//set default reservation
function CarReserve_Default(carid, days, meals, fuelId) {

    $('#car' + $('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue');
    $('#withdriver').attr('checked', true);
    $("#" + carid + "").attr('checked', true);
    $('#rsv-days').val(days);

    //clear car border highlight
    CarBorderReset();

    //meals and accomodation
    // 1 = included in the package
    // 0 = by renter
    if (meals == 1) {
        $('#rsv-meal').val("1");
    } else {
        $('#rsv-meal').val("0");
    }

    //fuel inclusion
    // 1 = included in the package
    // 0 = by renter
    if (meals == 1) {
        if (fuelId == 1) {
            $('#rsv-fuelId').val("1");
        } else {
            $('#rsv-fuel').val("0");
        }

        //update reservation text summary
        updateTransaction();

    }
}

    //listener on rental type click
    //values: with driver, self drive, longterm
    $('#btn-rentalType').click(function () {

        //update reservation text summary
        updateTransaction();
        rentalReset();
        rentalTypeSelection();

        //initial value
        $('#3').attr('checked', true);

    });

    //handle changes when selecting rental types
    function rentalTypeSelection() {

        if ($('input:radio[name=options]:checked').val() == "Self Drive") {
            //Long Term rental Type
            var selectedCar = $('input:radio[name=cars]:checked').attr("id");
            //change selection when van is selectedCar
            if (selectedCar == 1 || selectedCar == 2) {
                $('#3').attr('checked', true);
            }
            //highlight rental type button
            $('#rental-type-sdrive').addClass('active').siblings().removeClass('active');

            CarBorderReset();
            updateTransaction();
            $('.isSelfDrive1').hide();

        } else if ($('input:radio[name=options]:checked').val() == "longterm") {
            //Long Term rental Type
            //set car3 as default
            $('#3').attr('checked', true);

            //highlight rental type button
            $('#rental-type-longterm').addClass('active').siblings().removeClass('active');

            CarBorderReset();
            updateTransaction();
            $('.isSelfDrive1').hide();

        } else {
            //With Driver rental Type
            $('#1').attr('checked', true);

            //highlight rental type button
            $('#rental-type-wdriver').addClass('active').siblings().removeClass('active');

            CarBorderReset();
            updateTransaction();
            $('.isSelfDrive1').show();
        }
    }

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

        $('#car' + +$('input:radio[name=cars]:checked').attr("id")).css('border', '2px solid dodgerblue').siblings().css('border', '1px solid lightgray');
    });

    //update reservation text details
    function updateTransaction() {
        var mealPackage = $('#rsv-meal').val() == '1' ? 'Driver Meals/Accomodation Included ' : 'Driver Meals/Accomodation by renter';
        var fuelPackage = $('#rsv-fuel').val() == '1' ? 'Driver Fuel Included ' : 'Driver Fuel by renter';

        //display text on reservation summary
        $('#modal-text-foot').text($('input:radio[name=options]:checked').val() + ' - ' + $('input:radio[name=cars]:checked').val() +
        ' - ' + $('#rsv-days').val() + ' Days - ' + mealPackage + ' - ' + fuelPackage);

    }

    //reset text reservation summary
    function rentalReset() {
        $('#modal-text-foot').text($('input:radio[name=options]:checked').val() + ' - Please select a vehicle');
    }

    //handle change of days
    function dayschange() {
        if ($('#rsv-days').val() < 1) {
            $('#rsv-days').val(1);
        }

        updateTransaction();
    }

    //handle the count of number of days  on addition
    $('#days-add').click(function () {
        $('#rsv-days').val(+$('#rsv-days').val() + 1);
        updateTransaction();
    });

    //handle no of days limit and update text on subtraction
    $('#days-sub').click(function () {
        if ($('#rsv-days').val() > 1) {
            $('#rsv-days').val(+$('#rsv-days').val() - 1);
            updateTransaction();
        }
    });
