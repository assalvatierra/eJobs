

function submitRenter(){

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
        RenterEmail : 'jahdielsvillosa@gmail.com',
        RenterMobile : '09123456',
        RenterAddress : '',
        RenterFbAccnt : '',
        RenterLinkedInAccnt : '',
        EstHrPerDay : '',
        EstKmTravel : '',
        host: getUrl()
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


//Check renter name, email and number if not null
//check rent dates if valid
function checkRenterDetails() {
    var name, email, mobile, startdate, enddate;
    name = $('#rnt-name').val();
    email = $('#rnt-email').val();
    mobile = $('#rnt-mobile').val();
    startdate = $('#rnt-startdate').val();
    enddate = $('#rnt-enddate').val();
    var flag = true;

    //validate inputs if null or empty
    if (name == '' || name == null || email == '' || email == null || mobile == '' || mobile == null) {
        flag = false;
        $('#dtls-warning').text("Please enter your Name, Email and Mobile.");
    }

    //check if phone input is valid
    if (validateInputPhone()) {
    } else {
        flag = false;
        $('#dtls-warning').text("Phone number is not valid.");
    }

    //check if email input is valid
    if (validateInputEmail()) {
    } else {
        flag = false;
        $('#dtls-warning').text("Email is not valid.");
    }


    //validate inputs if null or empty
    if (name != null && email != null && mobile != null) {
    } else {
        flag = false;
        $('#dtls-warning').text("Please enter your Name, Email and Mobile.");

    }

    if (flag == true) {
        $('#submit-btn').removeClass('disabled');
        $('#dtls-warning').css('display', 'none');
    } else {
        $('#submit-btn').addClass('disabled');
        $('#dtls-warning').css('display', 'block');
    }

}


//validate inputs if contains the correct format
function  validateInputEmail() {
    var fieldValue = document.getElementById("rnt-email").value;

    var mailValidation = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (fieldValue.match(mailValidation)) {
        // correct mail format
        return true;
    } else {
        // incorrect structure
        return false;
    }
}

//validate inputs if containes number
function validateInputPhone() {
    var fieldValue = document.getElementById("rnt-mobile").value;

    var phoneValidation = /^([\s\(\)\-]*\d[\s\(\)\-]*){11}$/;
    if (fieldValue.match(phoneValidation)) {
        // correct phone structure
        return true;
    } else {
        // incorrect structure
        return false;
    }
}



function LoadOverlay() {
    $("#overlay").css("display", "flex");
}