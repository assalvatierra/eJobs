


insert into CarCategory (Description, Remarks)
values ('Rental','');

insert into CarUnit ( Description, Remarks, CarCategoryId ) 
values ('Van (10 seater)','',1),
	   ('Van (14 seater)','',1),
	   ('MPV/AUV/MiniVan','Multi-Purpose Vehicle',1),
	   ('SUV','Sports Utility Vehicle',1),
	   ('Pickup','',1),
	   ('Sedan','Van',1);

insert into CarImage ( CarUnitId, ImgUrl, Remarks, SysCode)
values (1,'/Images/CarRental/Grandia.jpg'),
       (2,'/Images/CarRental/Grandia.jpg'),
       (3,'/Images/CarRental/ToyotaInnova.jpg'),
       (4,'/Images/CarRental/FordEverest.jpg'),
       (5,'/Images/CarRental/ToyotaHilux.jpg'),
       (6,'/Images/CarRental/HondaCity.jpg');

