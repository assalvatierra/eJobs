


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
values (1,'/Images/CarRental/Grandia.jpg','','MAIN'),
       (2,'/Images/CarRental/Grandia.jpg','','MAIN'),
       (3,'/Images/CarRental/ToyotaInnova.jpg','','MAIN'),
       (4,'/Images/CarRental/FordEverest.jpg','','MAIN'),
       (5,'/Images/CarRental/ToyotaHilux.jpg','','MAIN'),
       (6,'/Images/CarRental/HondaCity.jpg','','MAIN');

insert into CarViewPage (CarUnitId, Viewname)
values (1,'CarDetail_van'),
	   (2,'CarDetail_van'),
	   (2,'CarDetail_mpv'),
	   (2,'CarDetail_suv'),
	   (2,'CarDetail_pickup'),
	   (2,'CarDetail_sedan');

