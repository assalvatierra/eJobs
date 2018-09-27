
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
values (1,'grandiaPrimary.png','','MAIN'),
       (2,'NissanPremiumPrimary.png','','MAIN'),
       (3,'fordeverest_primary.jpg','','MAIN'),
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
values ('Self Drive', 'Self Driver', 0,0, 1),
	   ('City Tour', 'With Driver', 200,300, 1),
	   ('Countryside Tour', 'With Driver', 200,300, 1),
	   ('Samal Tour', 'With Driver', 200,300, 1);

insert into CarRateUnitPackages (CarRatePackageId, CarUnitId, DailyRate, FuelLonghaul, FuelDaily)
		-- Van -- 
values (1, 1, 3500, 0, 0),	   -- self drive -- 
	   (2, 1, 3500, 0, 500),   -- city tour -- 
	   (3, 1, 3500, 500, 500), -- Countryside tour --  
	   (4, 1, 3500, 500, 500), -- samal tour --  
	   -- Van Nissan Premium -- 
	   (1, 2, 3500, 0, 0),	   -- self drive -- 
	   (2, 2, 3500, 0, 500),   -- city tour -- 
	   (3, 2, 3500, 500, 500), -- Countryside tour --  
	   (4, 2, 3500, 500, 500), -- samal tour --   
	   -- SUV -- 
	   (1, 3, 2500, 0, 0),	   -- self drive -- 
	   (2, 3, 2500, 0, 500),   -- city tour -- 
	   (3, 3, 2500, 500, 500), -- Countryside tour --  
	   (4, 3, 2500, 500, 500), -- samal tour --   
	   -- MPV -- 
	   (1, 4, 3500, 0, 0),	   -- self drive -- 
	   (2, 4, 3500, 0, 500),   -- city tour -- 
	   (3, 4, 3500, 500, 500), -- Countryside tour --  
	   (4, 4, 3500, 500, 500), -- samal tour --   
	   -- Sedan -- 
	   (1, 5, 2000, 0, 0),	   -- self drive -- 
	   (2, 5, 2000, 0, 500),   -- city tour -- 
	   (3, 5, 2000, 500, 500), -- Countryside tour --  
	   (4, 5, 2000, 500, 500), -- samal tour --  
	   -- Van -- 
	   (1, 6, 3000, 0, 0),	   -- self drive -- 
	   (2, 6, 3000, 0, 500),   -- city tour -- 
	   (3, 6, 3000, 500, 500), -- Countryside tour --  
	   (4, 6, 3000, 500, 500)  -- samal tour --  
	   ;
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
