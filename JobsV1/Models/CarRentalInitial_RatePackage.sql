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
('Surigao Del Norte','One round trip',300,400,1),
('Marilog', 'One round trip', 0,0,0 ),
('Buda', 'Seagull', 0,0,0 ),
('Valencia', 'One round trip', 0,0,0 ),
('Malaybalay', 'One round trip', 0,0,0 ),
('Manolo fortich', 'One round trip', 0,0,0 ),
('Cagayan De Oro', 'One round trip', 0,0,0 ),
('Misamis Oriental', 'One round trip', 0,0,0 ),
('Iligan', 'One round trip', 0,0,0 ),

('Santa Cruz', 'Davao Del Sur. One round trip', 0,0,0 ),
('Digos', 'Davao Del Sur. One round trip', 0,0,0 ),
('Davao Del Sur', 'One round trip', 0,0,0 ),
('Malita', 'One round trip', 0,0,0 ),
('Don Marcelino', 'One round trip', 0,0,0 ),
('General Santos', 'One round trip', 0,0,0 ),
('Sarangani', 'One round trip', 0,0,0 ),
('Koronadal', 'One round trip', 0,0,0 ),
('Isulan', 'One round trip', 0,0,0 ),
('Sultan Kudarat', 'One round trip', 0,0,0 );



Insert into CarRateUnitPackages (CarUnitId,CarRatePackageId,DailyRate,FuelLonghaul,FuelDaily)
values
-- regular van ( Grandia GL )
( 1, 1, 3000, 0,0 ), --city
( 1, 2, 3800, 0,0 ), -- Cuntryside
( 1, 3, 3800, 0,0 ), -- Samal
( 1, 4, 3000, 500, 500 ), --panabo
( 1, 5, 3000, 800,500 ), --tagum
( 1, 6, 3500, 800,700 ), --davao del norte
( 1, 7, 3500, 1000,700 ), --comval
( 1, 8, 3500, 1500,500 ), --govgen
( 1, 9, 3500, 1500,500 ), --Mati
( 1, 10, 4000, 2000,800 ), --Davao Oriental
( 1, 11, 4000, 2000,800 ), --Agusan del sur
( 1, 12, 4500, 3000,800 ), --Agusan del Norte
( 1, 13, 4000, 2500,800 ), --Surigao del sur
( 1, 14, 5000, 3000,800 ), --Surigao del Norte

-- big van ( Nissan Premium)
( 2, 1, 3500, 0,0 ), --city
( 2, 2, 4000, 0,0 ), -- Cuntryside
( 2, 3, 4000, 0,0 ), -- Samal
( 2, 4, 3500, 500, 500 ), --panabo
( 2, 5, 3500, 800,500 ), --tagum
( 2, 6, 4000, 800,700 ), --davao del norte
( 2, 7, 4000, 1000,700 ), --comval
( 2, 8, 4000, 1500,500 ), --govgen
( 2, 9, 4000, 1500,500 ), --Mati
( 2, 10, 4500, 2000,800 ), --Davao Oriental
( 2, 11, 4500, 2000,800 ), --Agusan del sur
( 2, 12, 5000, 3000,800 ), --Agusan del Norte
( 2, 13, 4500, 2500,800 ), --Surigao del sur
( 2, 14, 6000, 3000,800 ), --Surigao del Norte

-- SUV ( Ford Everest / fortuner )
( 3, 1, 3000, 0,0 ), --city
( 3, 2, 3800, 0,0 ), -- Cuntryside
( 3, 3, 3800, 0,0 ), -- Samal
( 3, 4, 3000, 500, 500 ), --panabo
( 3, 5, 3000, 800,500 ), --tagum
( 3, 6, 3500, 800,700 ), --davao del norte
( 3, 7, 3500, 1000,700 ), --comval
( 3, 8, 3500, 1500,500 ), --govgen
( 3, 9, 3500, 1500,500 ), --Mati
( 3, 10, 4000, 2000,800 ), --Davao Oriental
( 3, 11, 4000, 2000,800 ), --Agusan del sur
( 3, 12, 4500, 3000,800 ), --Agusan del Norte
( 3, 13, 4000, 2500,800 ), --Surigao del sur
( 3, 14, 5000, 3000,800 ), --Surigao del Norte

-- MPV ( Innova )
( 4, 1, 2500, 0,0 ), --city
( 4, 2, 3200, 0,0 ), -- Cuntryside
( 4, 3, 3200, 0,0 ), -- Samal
( 4, 4, 3000, 500, 500 ), --panabo
( 4, 5, 3000, 800,500 ), --tagum
( 4, 6, 3000, 800,700 ), --davao del norte
( 4, 7, 3000, 1000,700 ), --comval
( 4, 8, 3000, 1500,500 ), --govgen
( 4, 9, 3000, 1500,500 ), --Mati
( 4, 10, 3500, 2000,800 ), --Davao Oriental
( 4, 11, 3500, 2000,800 ), --Agusan del sur
( 4, 12, 4000, 3000,800 ), --Agusan del Norte
( 4, 13, 3500, 2500,800 ), --Surigao del sur
( 4, 14, 5000, 3000,800 ), --Surigao del Norte


-- MPV ( Innova )
( 5, 1, 2500, 0,0 ), --city
( 5, 2, 3000, 0,0 ), -- Cuntryside
( 5, 3, 3000, 0,0 ), -- Samal
( 5, 4, 3000, 500, 500 ), --panabo
( 5, 5, 3000, 800,500 ), --tagum

-- Pickup ( strada / hilux )
( 6, 1, 3000, 0,0 ), --city
( 6, 2, 3800, 0,0 ), -- Cuntryside
( 6, 3, 3800, 0,0 ), -- Samal
( 6, 4, 3000, 500, 500 ), --panabo
( 6, 5, 3000, 800,500 ), --tagum
( 6, 6, 3500, 800,700 ), --davao del norte
( 6, 7, 3500, 1000,700 ), --comval
( 6, 8, 3500, 1500,500 ), --govgen
( 6, 9, 3500, 1500,500 ), --Mati
( 6, 10, 4000, 2000,800 ), --Davao Oriental
( 6, 11, 4000, 2000,800 ), --Agusan del sur
( 6, 12, 4500, 3000,800 ), --Agusan del Norte
( 6, 13, 4000, 2500,800 ), --Surigao del sur
( 6, 14, 5000, 3000,800 ); --Surigao del Norte
