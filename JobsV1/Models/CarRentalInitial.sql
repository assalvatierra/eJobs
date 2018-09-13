


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
values (1,'/Images/CarRental/Grandia.jpg','',0),
       (2,'/Images/CarRental/Grandia.jpg','',0),
       (3,'/Images/CarRental/ToyotaInnova.jpg','',0),
       (4,'/Images/CarRental/FordEverest.jpg','',0),
       (5,'/Images/CarRental/ToyotaHilux.jpg','',0),
       (6,'/Images/CarRental/HondaCity.jpg','',0);

