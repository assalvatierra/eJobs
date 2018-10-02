
insert into CarCategories (Description, Remarks)
values ('Rental','');

insert into CarUnits ( Description, Remarks, CarCategoryId , SelfDrive) 
values ('Van (10 seater)','Gl Grandia',1,1),
	   ('Van (14 seater)','Nissan Premium',1,1),
	   ('SUV','Ford Everest',1,0),
	   ('MPV/AUV/MiniVan','Toyota Innova',1,0),	
	   ('Sedan','Honda City',1,0),
	   ('Pickup','Pickups',1,0);

insert into CarImages ( CarUnitId, ImgUrl, Remarks, SysCode)
values (1,'glgrandia-car-rental.png','','MAIN'),
       (2,'nissan-premium-car-rental.png','','MAIN'),
       (3,'ford-everest-car-rental.png','','MAIN'),
       (4,'toyota-innova-car-rental.png','','MAIN'),
       (5,'honda-city-car-rental.png','','MAIN'),
       (6,'pickup-car-rental.png','','MAIN');
	   
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

insert into CarUnitMetas(carUnitId,PageTitle,MetaDesc,HomeDesc)
values
	-- Van -- 
	(1, 'Vehicle (Toyota Grandia GL) for Rent in Davao City, Reliable car rental company in Davao',
	'Toyota Grandia GL is 10-12 seater van for business, tour and family travel needs. Car Rental offers affordable and flexible rental rates',
	'Comfortable 10 Seater van for rent for business and family occasions. Popular unit for Rent-A-Car in Davao.'
	),

	-- Nissan Premium -- 
	(2, 'Vehicle (Nissan Urvan Premium) for Rent in Davao City, Reliable van rental company in Davao City',
	'Nissan Urvan Premium is comfortable 10-14 seater van for business, tour and family travel needs. Very few rent-a-car company in Davao offers this type of vehicle',
	'Highroof van that can accommodate 14pax with individual reclining seats. No jump seats. Very few rent-a-car company in Davao offers this type of vehicle.'
	),

	-- SUVs -- 
	(3, 'SUV (Sports Utility Vehicle) for Rent in Davao City, Rent-A-Car company in Davao City',
	'SUV for rent in Davao City for your business needs and personal needs that can accommodate 7 persons. Open for SelfDrive or with driver rental option. Available units: Ford Everest, Toyota Fortuner, Toyota Innova',
	'SUV for business and personal needs that can accommodate 7persons. Units: Ford Everest, Toyota Fortuner, Toyota Innova.'
	),

	-- AUVs-- 
	(4, 'MPV (Multi purpose vehicle) for Rent in Davao City, Rent-A-Car company in Davao City',
	'MPV/AUV - Toyota Innova - for rent in Davao City for your travel needs. Open for SelfDrive or with driver rental option.',
	'Toyota Innova is a versatile vehicle for family and business use. Accommodates like a small van or SUV yet rides like a car.'
	),

	-- Sedan -- 
	(5, 'Sedan for Rent in Davao City, Car Rental company in Davao City',
	'Sedan - Honda City - for rent in Davao City for your business and travel needs. Open for SelfDrive or with driver rental option.',
	'Sedan is a practical vehicle for light travel for personal and business use. Available units: Honda City, Toyota Vios'
	),

	-- Pickup -- 
	(6, 'Pickup for Rent in Davao City, 4x4 rental, rent-a-car',
	'Pickup 4x4 for rent in Davao City for difficult terrain or bigger luggages. Units: Mitsubishi strada 4x4, Toyota Hilux 4x4',
	'Pickup 4x4 is best for difficult and unknown terrains. Can also use in hauling huge luggages.'
	);


Insert into CarRatePackages (Description,Remarks,DailyMeals,DailyRoom,DaysMin)
values 
	('SELFDRIVE', 'Fuel by renter', 0,0,0 ),
	('Davao City Tour', 'Car Rental for City (Downtown) Tour Package', 0,0,0 ),
	('Davao CountrySide Tour', 'Eden Nature Park, Philipine Eagle, Malagos, Japanese Tunnel, Shrine, Jacks Ridge', 0,0,0),
	('Samal Tour', 'Bat cave, Hagimit falls, Maxima Aquafun, Penaplata', 0,0,0 ),
	('Panabo','One round trip',300,400,1), 
	('Tagum','One round trip',300,400,1), 
	('Davao Del Norte','One round trip',300,400,1), 
	('Comval','One round trip',300,400,1), 
	('Governor Generoso','One round trip',300,400,1), 
	('Mati City','One round trip',300,400,1), 
	('Davao Oriental','One round trip',300,400,1), 
	('Agusan Del Sur','One round trip',300,400,1), 
	('Agusan Del Norte','One round trip',300,400,1), 
	('Surigao Del Sur','One round trip',300,400,1), 
	('Surigao Del Norte','One round trip',300,400,1);

Insert into CarRateUnitPackages (CarUnitId,CarRatePackageId,DailyRate,FuelLonghaul,FuelDaily)
values
	-- regular van ( Grandia GL )
	( 1, 1, 3000, 0,0 ), --selfdrive
	( 1, 2, 3000, 0,0 ), --city
	( 1, 3, 3800, 0,0 ), -- Cuntryside
	( 1, 4, 3800, 0,0 ), -- Samal
	( 1, 5, 3000, 500, 500 ), --panabo
	( 1, 6, 3000, 800,500 ), --tagum
	( 1, 7, 3500, 800,700 ), --davao del norte
	( 1, 8, 3500, 1000,700 ), --comval
	( 1, 9, 3500, 1500,500 ), --govgen
	( 1, 10, 3500, 1500,500 ), --Mati
	( 1, 11, 4000, 2000,800 ), --Davao Oriental
	( 1, 12, 4000, 2000,800 ), --Agusan del sur
	( 1, 13, 4500, 3000,800 ), --Agusan del Norte
	( 1, 14, 4000, 2500,800 ), --Surigao del sur
	( 1, 15, 5000, 3000,800 ), --Surigao del Norte

	-- big van ( Nissan Premium)
	( 2, 1, 3000, 0,0 ), --selfdrive
	( 2, 2, 3500, 0,0 ), --city
	( 2, 3, 4000, 0,0 ), -- Cuntryside
	( 2, 4, 4000, 0,0 ), -- Samal
	( 2, 5, 3500, 500, 500 ), --panabo
	( 2, 6, 3500, 800,500 ), --tagum
	( 2, 7, 4000, 800,700 ), --davao del norte
	( 2, 8, 4000, 1000,700 ), --comval
	( 2, 9, 4000, 1500,500 ), --govgen
	( 2, 10,4000, 1500,500 ), --Mati
	( 2, 11, 4500, 2000,800 ), --Davao Oriental
	( 2, 12, 4500, 2000,800 ), --Agusan del sur
	( 2, 13, 5000, 3000,800 ), --Agusan del Norte
	( 2, 14, 4500, 2500,800 ), --Surigao del sur
	( 2, 15, 6000, 3000,800 ), --Surigao del Norte

	-- SUV ( Ford Everest / fortuner )
	( 3, 1, 3000, 0,0 ), --selfdrive
	( 3, 2, 3000, 0,0 ), --city
	( 3, 3, 3800, 0,0 ), -- Cuntryside
	( 3, 4, 3800, 0,0 ), -- Samal
	( 3, 5, 3000, 500, 500 ), --panabo
	( 3, 6, 3000, 800,500 ), --tagum
	( 3, 7, 3500, 800,700 ), --davao del norte
	( 3, 8, 3500, 1000,700 ), --comval
	( 3, 9, 3500, 1500,500 ), --govgen
	( 3,10, 3500, 1500,500 ), --Mati
	( 3, 11, 4000, 2000,800 ), --Davao Oriental
	( 3, 12, 4000, 2000,800 ), --Agusan del sur
	( 3, 13, 4500, 3000,800 ), --Agusan del Norte
	( 3, 14, 4000, 2500,800 ), --Surigao del sur
	( 3, 15, 5000, 3000,800 ), --Surigao del Norte

	-- MPV ( Innova )
	( 4, 1, 3000, 0,0 ), --selfdrive
	( 4, 2, 2500, 0,0 ), --city
	( 4, 3, 3200, 0,0 ), -- Cuntryside
	( 4, 4, 3200, 0,0 ), -- Samal
	( 4, 5, 3000, 500, 500 ), --panabo
	( 4, 6, 3000, 800,500 ), --tagum
	( 4, 7, 3000, 800,700 ), --davao del norte
	( 4, 8, 3000, 1000,700 ), --comval
	( 4, 9, 3000, 1500,500 ), --govgen
	( 4,10, 3000, 1500,500 ), --Mati
	( 4, 11, 3500, 2000,800 ), --Davao Oriental
	( 4, 12, 3500, 2000,800 ), --Agusan del sur
	( 4, 13, 4000, 3000,800 ), --Agusan del Norte
	( 4, 14, 3500, 2500,800 ), --Surigao del sur
	( 4, 15, 5000, 3000,800 ), --Surigao del Norte


	-- MPV ( Innova )
	( 5, 1, 3000, 0,0 ), --selfdrive
	( 5, 2, 2500, 0,0 ), --city
	( 5, 3, 3000, 0,0 ), -- Cuntryside
	( 5, 4, 3000, 0,0 ), -- Samal
	( 5, 5, 3000, 500, 500 ), --panabo
	( 5, 6, 3000, 800,500 ), --tagum

	-- Pickup ( strada / hilux )
	( 6, 1, 3000, 0,0 ), --selfdrive
	( 6, 2, 3000, 0,0 ), --city
	( 6, 3, 3800, 0,0 ), -- Cuntryside
	( 6, 4, 3800, 0,0 ), -- Samal
	( 6, 5, 3000, 500, 500 ), --panabo
	( 6, 6, 3000, 800,500 ), --tagum
	( 6, 7, 3500, 800,700 ), --davao del norte
	( 6, 8, 3500, 1000,700 ), --comval
	( 6, 9, 3500, 1500,500 ), --govgen
	( 6,10, 3500, 1500,500 ), --Mati
	( 6, 11, 4000, 2000,800 ), --Davao Oriental
	( 6, 12, 4000, 2000,800 ), --Agusan del sur
	( 6, 13, 4500, 3000,800 ), --Agusan del Norte
	( 6, 14, 4000, 2500,800 ), --Surigao del sur
	( 6, 15, 5000, 3000,800 ); --Surigao del Norte