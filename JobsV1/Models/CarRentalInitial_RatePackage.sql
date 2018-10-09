﻿Insert into CarRatePackages (Description,Remarks,DailyMeals,DailyRoom,DaysMin)
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
('Sultan Kudarat', 'One round trip', 0,0,0 ),

('Kidapawan', 'Kidapawan. One round trip', 0,0,0 ),
('Ilomavis', 'Ilomavis. One round trip', 0,0,0 ),
('Kabacan', 'Kabacan. One round trip', 0,0,0 ),
('Arakan', 'Arakan. One round trip', 0,0,0 ),
('Midsayap', 'Midsayap. One round trip', 0,0,0 ),
('Cotabato City', 'Cotabato City. One round trip', 0,0,0 ),
('North Cotabato', 'Kidapawan. One round trip', 0,0,0 )
;


--insert into CarRates (Daily,Weekly,Monthly,KmFree,KmRate,CarUnitId,OtRate)
--values (2500,2500,2250,100,5,1,300), --grandia
--	   (2700,2500,2250,100,5,2,300), --premium
--	   (2500,2500,2250,100,5,3,300), --everest
--	   (2000,1800,1500,100,5,4,300), --innova
--	   (2000,1800,1500,100,5,5,300), --honda
--	   (2500,2500,2250,100,5,6,300); --pickup



Insert into CarRateUnitPackages (CarUnitId,CarRatePackageId,DailyRate,FuelLonghaul,FuelDaily,DailyAddon)
values
-- regular van ( Grandia GL )
( 1, 1, 0, 0, 0, 0 ), --selfdrive
( 1, 2, 0, 0, 500, 300 ), --city
( 1, 3, 0, 500, 500, 300 ), -- Cuntryside
( 1, 4, 0, 500, 500, 300 ), -- Samal
( 1, 5, 300, 500, 500, 300 ), --panabo
( 1, 6, 300, 500,500, 500 ), --tagum
( 1, 7, 500, 800,700, 500 ), --davao del norte
( 1, 8, 500, 800,700, 500 ), --comval
( 1, 9, 500, 1000,500, 500 ), --govgen
( 1, 10, 500, 1000,500, 500 ), --Mati
( 1, 11, 1000, 1500,700, 500 ), --Davao Oriental
( 1, 12, 1000, 3000,700, 500 ), --Agusan del sur
( 1, 13, 1500, 3500,700, 500 ), --Agusan del Norte
( 1, 14, 1000, 3000,700, 500 ), --Surigao del sur
( 1, 15, 2000, 4500,700, 500 ), --Surigao del Norte

( 1, 16, 4000, 2000,800, 500 ), --Marilog
( 1, 17, 4000, 2000,800, 500 ), --Buda
( 1, 18, 4500, 3000,800, 500 ), --Valencia
( 1, 19, 4000, 2500,800, 500 ), --Malaybalay
( 1, 20, 5000, 3000,800, 300 ), --Manolo fortich
( 1, 21, 4000, 2500,800, 300 ), --Cagayan
( 1, 22, 4000, 2500,800, 500 ), --Misamis
( 1, 23, 4000, 2500,800, 500 ), --Iligan
( 1, 24, 4000, 2500,800, 500 ), --Santa Cruz, davao del sur
( 1, 25, 4000, 2500,800, 500 ), --Malita
( 1, 26, 4000, 2500,800, 500 ), --Don Marcelino
( 1, 27, 4000, 2500,800, 500 ), --General Santos
( 1, 28, 4000, 2500,800, 500 ), --Saranggani
( 1, 29, 4000, 2500,800, 500 ), --Koronadal
( 1, 30, 4000, 2500,800, 500 ), --Isulan
( 1, 31, 4000, 2500,800, 500 ), --Sultan Kudarat
( 1, 32, 4000, 2500,800, 500 ), --Kidapawan
( 1, 33, 4000, 2500,800, 500 ), --Ilomavis
( 1, 34, 4000, 2500,800, 500 ), --Kabacan
( 1, 35, 4000, 2500,800, 500 ), --Arakan
( 1, 36, 4000, 2500,800, 500 ), --Midsayap
( 1, 37, 4000, 2500,800, 500 ), --Cotabato City
( 1, 38, 4000, 2500,800, 500 ), --North Cotabato

-- big van ( Nissan Premium)
( 2, 1, 3000, 0,0, 500 ), --selfdrive
( 2, 2, 3500, 0,0, 500 ), --city
( 2, 3, 4000, 0,0, 500 ), -- Cuntryside
( 2, 4, 4000, 0,0, 500 ), -- Samal
( 2, 5, 3500, 500, 500, 500 ), --panabo
( 2, 6, 3500, 800,500, 500 ), --tagum
( 2, 7, 4000, 800,700, 500 ), --davao del norte
( 2, 8, 4000, 1000,700, 500 ), --comval
( 2, 9, 4000, 1500,500, 500 ), --govgen
( 2, 10,4000, 1500,500, 500 ), --Mati
( 2, 11, 4500, 2000,800, 500 ), --Davao Oriental
( 2, 12, 4500, 2000,800, 500 ), --Agusan del sur
( 2, 13, 5000, 3000,800, 500 ), --Agusan del Norte
( 2, 14, 4500, 2500,800, 500 ), --Surigao del sur
( 2, 15, 6000, 3000,800, 500 ), --Surigao del Norte
( 2, 16, 4000, 2000,800, 500 ), --Marilog
( 2, 17, 4000, 2000,800, 500 ), --Buda
( 2, 18, 4500, 3000,800, 500 ), --Valencia
( 2, 19, 4000, 2500,800, 500 ), --Malaybalay
( 2, 20, 5000, 3000,800, 500 ), --Manolo fortich
( 2, 21, 4000, 2500,800, 500 ), --Cagayan
( 2, 22, 4000, 2500,800, 500 ), --Misamis
( 2, 23, 4000, 2500,800, 500 ), --Iligan
( 2, 24, 4000, 2500,800, 500 ), --Santa Cruz, davao del sur
( 2, 25, 4000, 2500,800, 500 ), --Malita
( 2, 26, 4000, 2500,800, 500 ), --Don Marcelino
( 2, 27, 4000, 2500,800, 500 ), --General Santos
( 2, 28, 4000, 2500,800, 500 ), --Saranggani
( 2, 29, 4000, 2500,800, 500 ), --Koronadal
( 2, 30, 4000, 2500,800, 500 ), --Isulan
( 2, 31, 4000, 2500,800, 500 ), --Sultan Kudarat
( 2, 32, 4000, 2500,800, 500 ), --Kidapawan
( 2, 33, 4000, 2500,800, 500 ), --Ilomavis
( 2, 34, 4000, 2500,800, 500 ), --Kabacan
( 2, 35, 4000, 2500,800, 500 ), --Arakan
( 2, 36, 4000, 2500,800, 500 ), --Midsayap
( 2, 37, 4000, 2500,800, 500 ), --Cotabato City
( 2, 38, 4000, 2500,800, 500 ), --North Cotabato

-- SUV ( Ford Everest / fortuner )
( 3, 1, 3000, 0,0, 500 ), --selfdrive
( 3, 2, 3000, 0,0, 500 ), --city
( 3, 3, 3800, 0,0, 500 ), -- Cuntryside
( 3, 4, 3800, 0,0, 500 ), -- Samal
( 3, 5, 3000, 500, 500, 500 ), --panabo
( 3, 6, 3000, 800,500, 500 ), --tagum
( 3, 7, 3500, 800,700, 500 ), --davao del norte
( 3, 8, 3500, 1000,700, 500 ), --comval
( 3, 9, 3500, 1500,500, 500 ), --govgen
( 3,10, 3500, 1500,500, 500 ), --Mati
( 3, 11, 4000, 2000,800, 500 ), --Davao Oriental
( 3, 12, 4000, 2000,800, 500 ), --Agusan del sur
( 3, 13, 4500, 3000,800, 500 ), --Agusan del Norte
( 3, 14, 4000, 2500,800, 500 ), --Surigao del sur
( 3, 15, 5000, 3000,800, 500 ), --Surigao del Norte
( 3, 16, 4000, 2000,800, 500 ), --Marilog
( 3, 17, 4000, 2000,800, 500 ), --Buda
( 3, 18, 4500, 3000,800, 500 ), --Valencia
( 3, 19, 4000, 2500,800, 500 ), --Malaybalay
( 3, 20, 5000, 3000,800, 500 ), --Manolo fortich
( 3, 21, 4000, 2500,800, 500 ), --Cagayan
( 3, 22, 4000, 2500,800, 500 ), --Misamis
( 3, 23, 4000, 2500,800, 500 ), --Iligan
( 3, 24, 4000, 2500,800, 500 ), --Santa Cruz, davao del sur
( 3, 25, 4000, 2500,800, 500 ), --Malita
( 3, 26, 4000, 2500,800, 500 ), --Don Marcelino
( 3, 27, 4000, 2500,800, 500 ), --General Santos
( 3, 28, 4000, 2500,800, 500 ), --Saranggani
( 3, 29, 4000, 2500,800, 500 ), --Koronadal
( 3, 30, 4000, 2500,800, 500 ), --Isulan
( 3, 31, 4000, 2500,800, 500 ), --Sultan Kudarat
( 3, 32, 4000, 2500,800, 500 ), --Kidapawan
( 3, 33, 4000, 2500,800, 500 ), --Ilomavis
( 3, 34, 4000, 2500,800, 500 ), --Kabacan
( 3, 35, 4000, 2500,800, 500 ), --Arakan
( 3, 36, 4000, 2500,800, 500 ), --Midsayap
( 3, 37, 4000, 2500,800, 500 ), --Cotabato City
( 3, 38, 4000, 2500,800, 500 ), --North Cotabato

-- MPV ( Innova )
( 4, 1, 3000, 0,0, 500 ), --selfdrive
( 4, 2, 2500, 0,0, 500 ), --city
( 4, 3, 3200, 0,0, 500 ), -- Cuntryside
( 4, 4, 3200, 0,0, 500 ), -- Samal
( 4, 5, 3000, 500, 500, 500 ), --panabo
( 4, 6, 3000, 800,500, 500 ), --tagum
( 4, 7, 3000, 800,700, 500 ), --davao del norte
( 4, 8, 3000, 1000,700, 500 ), --comval
( 4, 9, 3000, 1500,500, 500 ), --govgen
( 4,10, 3000, 1500,500, 500 ), --Mati
( 4, 11, 3500, 2000,800, 500 ), --Davao Oriental
( 4, 12, 3500, 2000,800, 500 ), --Agusan del sur
( 4, 13, 4000, 3000,800, 500 ), --Agusan del Norte
( 4, 14, 3500, 2500,800, 500 ), --Surigao del sur
( 4, 15, 5000, 3000,800, 500 ), --Surigao del Norte
( 4, 16, 4000, 2000,800, 500 ), --Marilog
( 4, 17, 4000, 2000,800, 500 ), --Buda
( 4, 18, 4500, 3000,800, 500 ), --Valencia
( 4, 19, 4000, 2500,800, 500 ), --Malaybalay
( 4, 20, 5000, 3000,800, 500 ), --Manolo fortich
( 4, 21, 4000, 2500,800, 500 ), --Cagayan
( 4, 22, 4000, 2500,800, 500 ), --Misamis
( 4, 23, 4000, 2500,800, 500 ), --Iligan
( 4, 24, 4000, 2500,800, 500 ), --Santa Cruz, davao del sur
( 4, 25, 4000, 2500,800, 500 ), --Malita
( 4, 26, 4000, 2500,800, 500 ), --Don Marcelino
( 4, 27, 4000, 2500,800, 500 ), --General Santos
( 4, 28, 4000, 2500,800, 500 ), --Saranggani
( 4, 29, 4000, 2500,800, 500 ), --Koronadal
( 4, 30, 4000, 2500,800, 500 ), --Isulan
( 4, 31, 4000, 2500,800, 500 ), --Sultan Kudarat
( 4, 32, 4000, 2500,800, 500 ), --Kidapawan
( 4, 33, 4000, 2500,800, 500 ), --Ilomavis
( 4, 34, 4000, 2500,800, 500 ), --Kabacan
( 4, 35, 4000, 2500,800, 500 ), --Arakan
( 4, 36, 4000, 2500,800, 500 ), --Midsayap
( 4, 37, 4000, 2500,800, 500 ), --Cotabato City
( 4, 38, 4000, 2500,800, 500 ), --North Cotabato

-- Sedan ( Honda City )
( 5, 1, 3000, 0,0, 500 ), --selfdrive
( 5, 2, 2500, 0,0, 500 ), --city
( 5, 3, 3000, 0,0, 500 ), -- Cuntryside
( 5, 4, 3000, 0,0, 500 ), -- Samal
( 5, 5, 3000, 500, 500, 500 ), --panabo
( 5, 6, 3000, 800,500, 500 ), --tagum
( 5, 7, 3500, 800,700, 500 ), --davao del norte
( 5, 8, 3500, 1000,700, 500 ), --comval
( 5, 9, 3500, 1500,500, 500 ), --govgen
( 5, 10, 3500, 1500,500, 500 ), --Mati
( 5, 11, 4000, 2000,800, 500 ), --Davao Oriental
( 5, 12, 4000, 2000,800, 500 ), --Agusan del sur
( 5, 13, 4500, 3000,800, 500 ), --Agusan del Norte
( 5, 14, 4000, 2500,800, 500 ), --Surigao del sur
( 5, 15, 5000, 3000,800, 500 ), --Surigao del Norte
( 5, 16, 4000, 2000,800, 500 ), --Marilog
( 5, 17, 4000, 2000,800, 500 ), --Buda
( 5, 18, 4500, 3000,800, 500 ), --Valencia
( 5, 19, 4000, 2500,800, 500 ), --Malaybalay
( 5, 20, 5000, 3000,800, 500 ), --Manolo fortich
( 5, 21, 4000, 2500,800, 500 ), --Cagayan
( 5, 22, 4000, 2500,800, 500 ), --Misamis
( 5, 23, 4000, 2500,800, 500 ), --Iligan
( 5, 24, 4000, 2500,800, 500 ), --Santa Cruz, davao del sur
( 5, 25, 4000, 2500,800, 500 ), --Malita
( 5, 26, 4000, 2500,800, 500 ), --Don Marcelino
( 5, 27, 4000, 2500,800, 500 ), --General Santos
( 5, 28, 4000, 2500,800, 500 ), --Saranggani
( 5, 29, 4000, 2500,800, 500 ), --Koronadal
( 5, 30, 4000, 2500,800, 500 ), --Isulan
( 5, 31, 4000, 2500,800, 500 ), --Sultan Kudarat
( 5, 32, 4000, 2500,800, 500 ), --Kidapawan
( 5, 33, 4000, 2500,800, 500 ), --Ilomavis
( 5, 34, 4000, 2500,800, 500 ), --Kabacan
( 5, 35, 4000, 2500,800, 500 ), --Arakan
( 5, 36, 4000, 2500,800, 500 ), --Midsayap
( 5, 37, 4000, 2500,800, 500 ), --Cotabato City
( 5, 38, 4000, 2500,800, 500 ), --North Cotabato

-- Pickup ( strada / hilux )
( 6, 1, 3000, 0,0, 500 ), --selfdrive
( 6, 2, 3000, 0,0, 500 ), --city
( 6, 3, 3800, 0,0, 500 ), -- Cuntryside
( 6, 4, 3800, 0,0, 500 ), -- Samal
( 6, 5, 3000, 500, 500, 500 ), --panabo
( 6, 6, 3000, 800,500, 500 ), --tagum
( 6, 7, 3500, 800,700, 500 ), --davao del norte
( 6, 8, 3500, 1000,700, 500 ), --comval
( 6, 9, 3500, 1500,500, 500 ), --govgen
( 6, 10, 3500, 1500,500, 500 ), --Mati
( 6, 11, 4000, 2000,800, 500 ), --Davao Oriental
( 6, 12, 4000, 2000,800, 500 ), --Agusan del sur
( 6, 13, 4500, 3000,800, 500 ), --Agusan del Norte
( 6, 14, 4000, 2500,800, 500 ), --Surigao del sur
( 6, 15, 5000, 3000,800, 500 ), --Surigao del Norte
( 6, 16, 4000, 2000,800, 500 ), --Marilog
( 6, 17, 4000, 2000,800, 500 ), --Buda
( 6, 18, 4500, 3000,800, 500 ), --Valencia
( 6, 19, 4000, 2500,800, 500 ), --Malaybalay
( 6, 20, 5000, 3000,800, 500 ), --Manolo fortich
( 6, 21, 4000, 2500,800, 500 ), --Cagayan
( 6, 22, 4000, 2500,800, 500 ), --Misamis
( 6, 23, 4000, 2500,800, 500 ), --Iligan
( 6, 24, 4000, 2500,800, 500 ), --Santa Cruz, davao del sur
( 6, 25, 4000, 2500,800, 500 ), --Malita
( 6, 26, 4000, 2500,800, 500 ), --Don Marcelino
( 6, 27, 4000, 2500,800, 500 ), --General Santos
( 6, 28, 4000, 2500,800, 500 ), --Saranggani
( 6, 29, 4000, 2500,800, 500 ), --Koronadal
( 6, 30, 4000, 2500,800, 500 ), --Isulan
( 6, 31, 4000, 2500,800, 500 ), --Sultan Kudarat
( 6, 32, 4000, 2500,800, 500 ), --Kidapawan
( 6, 33, 4000, 2500,800, 500 ), --Ilomavis
( 6, 34, 4000, 2500,800, 500 ), --Kabacan
( 6, 35, 4000, 2500,800, 500 ), --Arakan
( 6, 36, 4000, 2500,800, 500 ), --Midsayap
( 6, 37, 4000, 2500,800, 500 ), --Cotabato City
( 6, 38, 4000, 2500,800, 500 ); --North Cotabato
