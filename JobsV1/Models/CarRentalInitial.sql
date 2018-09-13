
insert into CarCategories (Description, Remarks)
values ('Rental','');

insert into CarUnits ( Description, Remarks, CarCategoryId ) 
values ('Van (10 seater)','',1),
	   ('Van (14 seater)','',1),
	   ('SUV','Sports Utility Vehicle',1),
	   ('MPV/AUV/MiniVan','Multi-Purpose Vehicle',1),
	   ('Sedan','Van',1),
	   ('Pickup','',1);

insert into CarImages ( CarUnitId, ImgUrl, Remarks, SysCode)
values (1,'toyotagrandia_primary.jpg','','MAIN'),
       (2,'nissan_premium_primary.png','','MAIN'),
       (3,'ToyotaFortuner_primary.png','','MAIN'),
       (4,'toyotainnova_primary.jpg','','MAIN'),
       (5,'hondacity_primary.jpg','','MAIN'),
       (6,'strada_primary.jpg','','MAIN');
	   
insert into CarViewPages (CarUnitId, Viewname)
values (1,'CarDetail_van'),
	   (2,'CarDetail_van'),
	   (3,'CarDetail_mpv'),
	   (4,'CarDetail_suv'),
	   (5,'CarDetail_pickup'),
	   (6,'CarDetail_sedan');

insert into CarRates (Daily,Weekly,Monthly,KmFree,KmRate,CarUnitId,OtRate)
values (3500,3000,2250,120,5,1,250),
	   (3500,3000,2250,120,5,2,250),
	   (3500,2500,2000,100,5,3,250),
	   (3500,3500,3000,100,5,4,250),
	   (2500,2000,1500,100,5,5,250),
	   (3500,3000,2500,100,5,6,250);