using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsV1.Models
{

    #region Reservation Package Table

    public class PackageTable
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Meals { get; set; }
        public decimal Fuel { get; set; }
        public int Days { get; set; }
        public decimal Rate { get; set; }

        //calculation strings
        public string TextMeals { get; set; }
        public string TextFuel { get; set; }
        public string TextDays { get; set; }
        public string TextRate { get; set; }
    }

    #endregion
    
    public class CarReserve
    {
        JobDBContainer db = new JobDBContainer();
        
        #region PackageList
        //reservation table
        public List<PackageTable> getPackageList(int CarId, int days, string rentType, int meals, int fuel, int isAuthorize)
        {
            string RentType = rentType;
            int rsvRentType = RentType == "With Driver" ? 1 : 0;
            int rsvDays = days;
            int rsvIncludeMeals = meals;
            int rsvIncludeFuel = fuel;
            decimal TotalRate = 1;
            int adjDays = (rsvDays - 1);

            List<PackageTable> packages = new List<PackageTable>();
            List<CarRatePackage> packageList = db.CarRatePackages.Where(c => c.Status == "ACT" || c.Status == "SYS").ToList();

            //list of package
            foreach (var pkg in packageList)
            {
                //package rates of car unit
                var carRatePackage = pkg.CarRateUnitPackages.Where(c => c.CarUnitId == CarId && c.CarRatePackage.Id == pkg.Id).FirstOrDefault();

                //handle with driver

                //handle meals
                decimal MealsRate = getMealsCalc(rsvIncludeMeals, pkg.DailyMeals, pkg.DailyRoom, adjDays, rsvRentType);
                string MealsRateTxt = getMealsCalcTxt(rsvIncludeMeals, pkg.DailyMeals, pkg.DailyRoom, adjDays, rsvRentType, isAuthorize);
                //handle fuel
                decimal FuelRate = getFuelCalc(rsvIncludeFuel, carRatePackage.FuelDaily, carRatePackage.FuelLonghaul, rsvDays, rsvRentType, pkg.Id);
                string FuelRateTxt = getFuelCalcTxt(rsvIncludeFuel, carRatePackage.FuelDaily, carRatePackage.FuelLonghaul, rsvDays, rsvRentType, pkg.Id, isAuthorize);

                //handle unit Rate
                decimal carRate = getCarRateCalc(CarId, days, (decimal)carRatePackage.DailyAddon, carRatePackage.DailyRate);
                string carRatetxt = getCarRateCalcTxt(CarId, days, (decimal)carRatePackage.DailyAddon, carRatePackage.DailyRate, isAuthorize);
                //handle total Rate
                TotalRate = MealsRate + FuelRate + carRate;

                PackageTable newPkg = new PackageTable();
                newPkg.Id = pkg.Id;
                newPkg.Description = pkg.Description;
                newPkg.Fuel = FuelRate;
                newPkg.Meals = (MealsRate);
                newPkg.Days = (rsvDays);
                newPkg.Rate = TotalRate;
                //string
                newPkg.TextMeals = MealsRateTxt;
                newPkg.TextFuel = FuelRateTxt;
                newPkg.TextRate = carRatetxt;

                packages.Add(newPkg);
            }

            return packages;
        }


        public PackageTable getPackageSummary(int CarId, int days, string rentType, int meals, int fuel, int selectedPkgId, int isAuthorize)
        {
            string RentType = rentType;
            int rsvRentType = RentType == "With Driver" ? 1 : 0;
            int rsvDays = days;
            int rsvIncludeMeals = meals;
            int rsvIncludeFuel = fuel;
            decimal TotalRate = 1;
            int adjDays = (rsvDays - 1);

            PackageTable packages = new PackageTable();
            CarRatePackage pkg = db.CarRatePackages.Where(c => c.Id == selectedPkgId).FirstOrDefault();

            //package rates of car unit
            var carRatePackage = pkg.CarRateUnitPackages.Where(c => c.CarUnitId == CarId && c.CarRatePackage.Id == pkg.Id).FirstOrDefault();

            //handle with driver

            //handle meals
            decimal MealsRate = getMealsCalc(rsvIncludeMeals, pkg.DailyMeals, pkg.DailyRoom, adjDays, rsvRentType);
            string MealsRateTxt = getMealsCalcTxt(rsvIncludeMeals, pkg.DailyMeals, pkg.DailyRoom, adjDays, rsvRentType, isAuthorize);
            //handle fuel
            decimal FuelRate = getFuelCalc(rsvIncludeFuel, carRatePackage.FuelDaily, carRatePackage.FuelLonghaul, rsvDays, rsvRentType, pkg.Id);
            string FuelRateTxt = getFuelCalcTxt(rsvIncludeFuel, carRatePackage.FuelDaily, carRatePackage.FuelLonghaul, rsvDays, rsvRentType, pkg.Id, isAuthorize);

            //handle unit Rate
            decimal carRate = getCarRateCalc(CarId, days, (decimal)carRatePackage.DailyAddon, carRatePackage.DailyRate);
            string carRatetxt = getCarRateCalcTxt(CarId, days, (decimal)carRatePackage.DailyAddon, carRatePackage.DailyRate, isAuthorize);
            //handle total Rate
            TotalRate = MealsRate + FuelRate + carRate;

            PackageTable newPkg = new PackageTable();
            newPkg.Id = pkg.Id;
            newPkg.Description = pkg.Description;
            newPkg.Fuel = FuelRate;
            newPkg.Meals = (MealsRate);
            newPkg.Days = (rsvDays);
            newPkg.Rate = TotalRate;
            //string
            newPkg.TextMeals = MealsRateTxt;
            newPkg.TextFuel = FuelRateTxt;
            newPkg.TextRate = carRatetxt;

            packages = newPkg;

            return packages;
        }

        private decimal getMealsCalc(int rsvIncludeMeals, decimal pkgMeals, decimal pkgRooms, int days, int rentType)
        {
            //MealsAcc
            decimal mealsperDay = (pkgMeals + pkgRooms) * (days);
            decimal mealsRate = ((mealsperDay) + pkgMeals); //include meals on the day
            mealsRate *= rentType;    //self drive does not include meals
            mealsRate *= rsvIncludeMeals;    //check if user wants to include or not include driver meals

            return mealsRate;
        }


        private string getMealsCalcTxt(int rsvIncludeMeals, decimal pkgMeals, decimal pkgRooms, int days, int rentType, int isAuthorize)
        {
            // MealsAcc
            decimal mealsperDay = (pkgMeals + pkgRooms) * (days);
            decimal mealsRate = ((mealsperDay) + pkgMeals); //include meals on the day
            mealsRate *= rentType;    //self drive does not include meals
            mealsRate *= rsvIncludeMeals;    //check if user wants to include or not include driver meals

            //build string
            string mealsText = "";

            if (isAuthorize == 1)
            {
                mealsText = rsvIncludeMeals == 1 ? "Included - m : " + mealsRate : "by renter";
            }
            else
            {   //guest user
                mealsText = rsvIncludeMeals == 1 ? "Included in the package " : "by renter";
            }
        

            return mealsText;
        }



        private decimal getFuelCalc(int rsvIncludeFuel, decimal pkgFuel, decimal pkglonghaul, int days, int rentType, int tourid)
        {
            //MealsAcc
            decimal fuelRate = 0;

            //Fuel
            //fuel = (fuelrate * days ) + longhaul 
            fuelRate = (pkgFuel * days) + pkglonghaul;
            fuelRate *= rsvIncludeFuel;

            return fuelRate;
        }

        private string getFuelCalcTxt(int rsvIncludeFuel, decimal pkgFuel, decimal pkglonghaul, int days, int rentType, int tourid, int isAuthorize)
        {
            //MealsAcc
            decimal fuelRate = 0;

            //Fuel
            //fuel = (fuelrate * days ) + longhaul 
            fuelRate = (pkgFuel * days) + pkglonghaul;
            fuelRate *= rsvIncludeFuel;
            
            string fuelRateTxt = "";

            if (isAuthorize == 1)
            {
                fuelRateTxt = rsvIncludeFuel == 1 ? "Included - f: " + pkgFuel + "+ lh: " + pkglonghaul : "by renter";
            }
            else
            {   //guest user
                fuelRateTxt = rsvIncludeFuel == 1 ? "Included in the package" : "by renter";
            }

            return fuelRateTxt;
        }


        private decimal getCarRateCalc(int carId, int days, decimal packageDailyAddon, decimal carRateAdditional)
        {
            //MealsAcc
            decimal totalCarRate = 0;
            var carRate = db.CarRates.Where(c => c.Id == carId).FirstOrDefault();
            decimal addedRates = carRateAdditional + (packageDailyAddon * days);

            if (days > 21)
            {
                totalCarRate = (carRate.Monthly * days);
            }

            if (days > 6)
            {
                if (22 > days)
                {
                    totalCarRate = (carRate.Weekly * days);
                }
            }

            if (6 > days)
            {
                totalCarRate = (carRate.Daily * days);
            }

            totalCarRate = (carRate.Daily * days);
            totalCarRate += addedRates;
            return totalCarRate;
        }

        private string getCarRateCalcTxt(int carId, int days, decimal packageDailyAddon, decimal carRateAdditional, int isAuthorize)
        {
            //MealsAcc
            decimal totalCarRate = 0;
            var carRate = db.CarRates.Where(c => c.Id == carId).FirstOrDefault();
            decimal addedRates = carRateAdditional + (packageDailyAddon * days);

            string totalCarRateTxt = "";
            if (days > 21)
            {
                totalCarRate = (carRate.Monthly * days);
                totalCarRateTxt += "r: (" + carRate.Monthly + " / day ) = "+ totalCarRate + " +" ;
            }

            if (days > 6)
            {
                if (22 > days)
                {
                    totalCarRate = (carRate.Weekly * days);
                    totalCarRateTxt += "r: (" + carRate.Weekly + " / day ) = " + totalCarRate + " +";
                }
            }

            if (6 > days)
            {
                totalCarRate = (carRate.Daily * days);
                totalCarRateTxt += "r: (" + carRate.Daily + " / day ) = " + totalCarRate + " +";
            }
            
            totalCarRate = (carRate.Daily * days);
            totalCarRate += addedRates;

            totalCarRateTxt += " a: " + addedRates;
            

            if (isAuthorize == 0)
            {    //guest user
                totalCarRateTxt =  "" ;
            }


            return totalCarRateTxt;
        }


        #endregion
    }
}


/*
 * 
 * 
 * 
<script>
    displayPackages();
    function displayPackages(){

        var ratePackageCount = 1;
        var carRateAdditional = 1;
        var packageDailyAddon = 1;
        var mealFlag = 1;
        var fuelFlag = 1;

        RentalType = $('input:radio[name=options]:checked').val();

        var days =  $('#rsv-days').val();
        var fuelAuthText = "";
        var MealAuthText = "";
        var TotalAuthText = "";
        var isAuthorized = '@ViewBag.isAuthorize';

        @foreach (var package in ViewBag.Packages as IEnumerable<JobsV1.Models.CarRatePackage>)
        {
            <text>
            var carRate_longhaulrate = 0;
            var carRate_fuel = 0;
            </text>

            //carRatesPackages
            foreach (var carRatesPackages in ViewBag.carRatesPackages as IEnumerable<JobsV1.Models.CarRateUnitPackage>)
            {
                
                if (@carRatesPackages.CarRatePackageId == @package.Id)
                {
                    <text>
                    if(@(carRatesPackages.CarUnitId) == selectedCar )
                    {
                        ratePackageCount += 1;
                        carRate_longhaulrate = @carRatesPackages.FuelLonghaul;
                        carRate_fuel =  @carRatesPackages.FuelDaily;
                        carRateAdditional = @carRatesPackages.DailyRate;
                        packageDailyAddon = (@carRatesPackages.DailyAddon == null ? 0 : @carRatesPackages.DailyAddon );
                        packageDailyAddon *= days;

                        //handle self drive selected tour
                        if(selectedTour == 1){
                            packageDailyAddon = 0;
                            carRateAdditional = 0;
                        }

                        //fuel
                        if ($('#rsv-fuel').val() == '0') {

                            //hides the calculation if not login
                            if(isAuthorized == '1'){
                                fuelAuthText = ': f:'  + carRate_fuel.toLocaleString() +' lh:' + carRate_longhaulrate.toLocaleString() + ' dr:' + carRateAdditional.toLocaleString() + ' da:' + packageDailyAddon.toLocaleString();
                            }else{
                                fuelAuthText = "";
                            }

                            //display
                            $('#pkg-fuel@(package.Id)').text('by renter ' + fuelAuthText);

                            //reset values
                            carRate_longhaulrate = 0;
                            fuelFlag = 0;
                        } else {
                            //hides the calculation if not login
                            if(isAuthorized == '1'){
                                fuelAuthText = ' dr:' + carRateAdditional.toLocaleString() + ' da:' + packageDailyAddon.toLocaleString() ;
                            }else{
                                fuelAuthText = "";
                            }

                            $('#pkg-fuel@(package.Id)').text('Included in the Package ' + fuelAuthText);
                            fuelFlag = 1;
                        }
                    }
                </text>
                }
            }
            <text>
            $('#pkg-days@(package.Id)').text($('#rsv-days').val());

            //meals and accomodation
            if ($('#rsv-meal').val() == '0'){
                $('#pkg-meal@(package.Id)').text('by renter ')
                mealFlag = 0;
            } else {
                //hides the calculation if not login
                if(isAuthorized == '1'){
                    //check reservation date if one day or more
                    if($('#rsv-days').val() == 1){
                        MealAuthText =' - m:' + @package.DailyMeals;
                    }else{
                        MealAuthText =' - ( m:' + @package.DailyMeals +' + r:' + @package.DailyRoom +' per day ) + m:' + @package.DailyMeals;
                    }
                }else{
                    MealAuthText = "in the Package";
                }
                $('#pkg-meal@(package.Id)').text('Included ' + MealAuthText)
                mealFlag = 1;
            }
            </text>

            //rate
            foreach (var rates in ViewBag.CarRates as IEnumerable<JobsV1.Models.CarRate>)
            {
                <text>
                var rate = 0;
                var rateText = 0;
                //check if weekly, monthly or daily
                var days =  $('#rsv-days').val();
                if(days > 21){
                    rate = @rates.Monthly;
                    rate = rate * days;
                    rateText = '(@rates.Monthly / day) '+ rate.toLocaleString() +'';
                }

                if(days > 6 ){
                    if(22 > days){
                        rate = @rates.Weekly;
                        rate = rate * days;
                        rateText = '(@rates.Weekly / day) '+ rate.toLocaleString() +'';
                    }
                }

                if(6 > days){
                    rate = @rates.Daily;
                    rate = rate * days;
                    rateText = '(@rates.Daily / day ) = '+ rate.toLocaleString() +' ';
                }

                var rentalType;
                if(RentalType == "With Driver" ){
                    rentalType = 1;
                }else{
                    rentalType = 0
                }

                var days =  $('#rsv-days').val();
                var adjDays = (days-1);

                //Fuel
                var fuelRate = (Fuel * carRate_fuel * days ) + carRate_longhaulrate;
                fuelRate *= fuelFlag;

                //MealsAcc
                var mealsperDay = (@package.DailyMeals +  @package.DailyRoom) * (adjDays);
                var mealsRate = ((mealsperDay)  + @package.DailyMeals ) * rentalType;
                mealsRate *= mealFlag;

                var addedRates = carRateAdditional + packageDailyAddon;


                //handle self drive selected tour
                if(selectedTour == 1){
                    addedRates = 0;
                }

                //total rate
                var totalRate =rate + fuelRate + mealsRate + carRateAdditional + packageDailyAddon;

                if(selectedCar == '@rates.CarUnitId' ){

                    //hides the calculation if not login
                    if(isAuthorized == '1'){
                        TotalAuthText = 'r:' + rateText   +' + m:'+ mealsRate.toLocaleString() +' + f:'+ fuelRate.toLocaleString()  +' + a:'+ addedRates.toLocaleString()  +' = T:' ;
                    }else{
                        TotalAuthText = "";
                    }

                    $('#pkg-rate@(package.Id)').text( TotalAuthText + ' ' );

                    $('#pkg-total@(package.Id)').text( totalRate.toLocaleString());

                }
                </text>
            }
        }
      //  $("#table-packages > tbody > tr").hide().slice(0, ratePackageCount).show();
    }
</script>

 * 
 * 
 * 
 */
