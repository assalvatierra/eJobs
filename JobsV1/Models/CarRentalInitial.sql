
insert into CarCategories (Description, Remarks)
values ('Rental','');

insert into CarUnits ( Description, Remarks, CarCategoryId ) 
values ('Van (10 seater)','',1),
	   ('Van (14 seater)','',1),
	   ('MPV/AUV/MiniVan','Multi-Purpose Vehicle',1),
	   ('SUV','Sports Utility Vehicle',1),
	   ('Pickup','',1),
	   ('Sedan','Van',1);

insert into CarImages ( CarUnitId, ImgUrl, Remarks, SysCode)
values (1,'Grandia.jpg','','MAIN'),
       (2,'Grandia.jpg','','MAIN'),
       (3,'ToyotaInnova.jpg','','MAIN'),
       (4,'FordEverest.jpg','','MAIN'),
       (5,'ToyotaHilux.jpg','','MAIN'),
       (6,'HondaCity.jpg','','MAIN');
	   
insert into CarViewPages (CarUnitId, Viewname)
values (1,'CarDetail_van'),
	   (2,'CarDetail_van'),
	   (3,'CarDetail_mpv'),
	   (4,'CarDetail_suv'),
	   (5,'CarDetail_pickup'),
	   (6,'CarDetail_sedan');

insert into CarRates (Daily,Weekly,Monthly,KmFree,KmRate,CarUnitId)
values (3500,3000,2250,120,5,1),
	   (3500,3000,2250,120,5,2),
	   (3500,3500,3000,100,5,3),
	   (3500,2500,2000,100,5,4),
	   (3500,3000,2500,100,5,5),
	   (2500,2000,1500,100,5,6);