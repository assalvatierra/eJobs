
insert into CarCategories (Description, Remarks)
values ('Rental','');

insert into CarUnits ( Description, Remarks, CarCategoryId , SelfDrive) 
values ('Van (10 seater)','Van',1,1),
	   ('Van (14 seater)','Van',1,1),
	   ('SUV','Sports Utility Vehicle',1,0),
	   ('MPV/AUV/MiniVan','Multi-Purpose Vehicle',1,0),
	   ('Sedan','',1,0),
	   ('Pickup','',1,0);

insert into CarImages ( CarUnitId, ImgUrl, Remarks, SysCode)
values (1,'grandiaPrimary.png','','MAIN'),
       (2,'NissanPremiumPrimary.png','','MAIN'),
       (3,'FortunerPrimary.png','','MAIN'),
       (4,'InnovaPrimary.png','','MAIN'),
       (5,'HondaCityPrimary.png','','MAIN'),
       (6,'PickupPrimary.png','','MAIN');
	   
insert into CarViewPages (CarUnitId, Viewname)
values (1,'CarDetail_van'),
	   (2,'CarDetail_van'),
	   (3,'CarDetail_suv'),
	   (4,'CarDetail_mpv'),
	   (5,'CarDetail_sedan'),
	   (6,'CarDetail_pickup');

insert into CarRates (Daily,Weekly,Monthly,KmFree,KmRate,CarUnitId,OtRate)
values (3500,3000,2250,120,5,1,250),
	   (3500,3000,2250,120,5,2,250),
	   (3500,2500,2000,100,5,3,250),
	   (2500,3500,3000,100,5,4,250),
	   (2500,2000,1500,100,5,5,250),
	   (3500,3000,2500,100,5,6,250);

insert into CarRatePackages (Description, Remarks, DailyMeals, DailyRoom, DaysMin)
values ('City Tour', 'With Driver', 200,300, 1),
	   ('Countryside Tour', 'With Driver', 200,300, 1),
	   ('Samal Tour', 'With Driver', 200,300, 1);

insert into CarUnitMetas(carUnitId,PageTitle,MetaDesc)
values
-- Van -- 
(1, 'Vehicle (Toyota Grandia GL) for Rent in Davao City, Reliable car rental company in Davao',
'Toyota Grandia GL is 10-12 seater van for business, tour and family travel needs. Car Rental offers affordable and flexible rental rates'),

-- Nissan Premium -- 
(2, 'Vehicle (Nissan Urvan Premium) for Rent in Davao City, Reliable van rental company in Davao City',
'Nissan Urvan Premium is comfortable 10-14 seater van for business, tour and family travel needs. Car Rental offers affordable and flexible rental rates'

);

