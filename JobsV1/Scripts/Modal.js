/***************
* Modal Scripts
*
****************/

    /**
    *   Handles footer. Show Footer only when device is
    *   mobile or screen width less than 600 pixels.
    */
    function toggleScreen() {
        var f = document.getElementById("mobile-footer");
        var screenwidth = document.documentElement.clientWidth;

        if (screenwidth < 600) {
            //alert("MOBILE DEVICE!!");
            f.style.display = "block";
        }
        else {
            f.style.display = "none";
        }
    }



