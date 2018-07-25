insert into Cities(Name) values('Davao');
insert into Branches(Name, CityId, Remarks, Address, Landline, Mobile) values('Davao-01',1,'Davao Main','2nd Floor Sulit Bldg, Mac Arthur Hwy, Matina','082 297-1831','');
insert into JobStatus([Status]) values('INQUIRY'),('RESERVATION'),('CONFIRMED'),('CLOSED'),('CANCELLED'),('TEMPLATE');
insert into JobThrus([Desc]) values('PHONE'),('EMAIL'),('WALKIN');


insert into Banks([BankName],[BankBranch],[AccntName],[AccntNo]) values ('Cash','Davao','Cash','0'),('BDO','SM-Ecoland Davao','AJ88 Car Rental Services','00 086 072 9575'),('BPI','SM-Ecoland Davao','Abel S. Salvatierra','870 303 5125');

insert into Customers([Name],[Email],[Contact1],[Contact2],[Remarks],[Status]) values('<< New Customer >>','--','--',' ',' ','ACT');
insert into Customers([Name],[Email],[Contact1],[Contact2],[Remarks],[Status]) values('RealBreeze-Davao','realbreezedavao@gmail.com','Elvie/0916-755-8473','','','ACT');

insert Into Destinations([Description],[Remarks],[CityId]) 
values 
('Eden Nature Park','','1 '),
('Philippine Eagle','','1 '),
('Malagos Garden','','1 '),
('Japanese Tunnel','','1 ')
;

-- ------------------------------------------------------------
-- Sales Lead Configuration
-- ------------------------------------------------------------

insert into CustCategories([Name])
values ('PRIORITY'),('ACTIVE'),('SUSPENDED'),('BAD ACCOUNT'); 

insert into CustEntMains([Name],[Address],[Contact1],[Contact2])
values ('NEW (not yet defined)',' ',' ',' ');

insert into SalesStatusCodes([SeqNo],[Name])
values (1,'NEW'), (2,'ASSESMENT'), (3, 'PROPOSAL SENT'), (4, 'NEGOTIATION'), (5, 'ACCEPTED'), (6, 'REJECTED'), (7, 'CLOSE');

insert into SalesActCodes([Name],[Desc],[SysCode],[iconPath],[DefaultActStatus])
values 
('RFQ','Request for quotation', 'RFQ','~/Images/SalesLead/Quotation101.png',1), 
('CALL-REQUEST','Return Call request','CALL REQUEST','~/Images/SalesLead/Phone103.png',1),   
('EMAIL-REQUEST','Request to Check/reply Email','EMAIL REQUEST','~/Images/SalesLead/Email102.jpg',1),   
('CALL-DONE','Call is done', 'CALL DONE','~/Images/SalesLead/Phone103.png',2), 
('MEETING-REQUEST','Schedule an appointment','APPOINTMENT','~/Images/SalesLead/meeting102.jpg',1),   
('MEETING-DONE','Meeting done', 'APPOINTMENT_DONE','~/Images/SalesLead/meeting102.jpg',2); 

insert into SalesActStatus([Name])
values ('REQUEST'),('DONE'),('SUSPEND');

insert into SalesLeadCatCodes([CatName],[SysCode],[iconPath])
values ('Priority','PRIORITY','~/Images/SalesLead/high-importance.png'), 
('HighMargin','HIGHMARGIN','~/Images/SalesLead/GreenArrow.png'),
('LongTerm','LONGTERM','~/Images/SalesLead/Longterm.png'), 
('Corporate','CORPORATE ACCOUNT','~/Images/SalesLead/ShakeHands.png'), 
('HardOne', 'HARDONE','~/Images/SalesLead/unhappy.jpg');

-- ----------------------------------------
-- Services Configuration
-- ----------------------------------------
insert into SupplierTypes(Description) values
('Rent-a-car'),('Boat'),('Tour'),('Airline'),('Hotel');
insert Into Suppliers([Name],[Contact1],[Details],[Email],[CityId],[SupplierTypeId]) values('<< New Supplier >>','--',' ', '--','1','1');
insert Into Suppliers([Name],[Contact1],[Details],[Email],[CityId],[SupplierTypeId]) values('AJ Davao Car Rental','Abel / 0995-085-0158',' ', 'AJDavao88@gmail.com','1','1');
insert into SupplierItems([Description],[SupplierId],[Remarks],[InCharge]) values ('Default','1','Item by supplier','Supplier');

insert into Services([Name],[Description]) 
values
('Car Rental','Bus, Car, Van and other Transportation arrangements'),
('Boat Rental','Boat Arrangement, Island Hopping'),
('Tour Package','Tour Package, Land arrangements'),
('AirTicket','Airline Ticket'),
('Accommodation','Hotels, Rooms, Houses, etc'),
('Activity','Water Rafting, Scuba Diving, Caving'),
('Other','Other types of services');

insert into SrvActionCodes([CatCode],[SortNo])
values
('Arrangement',1),('Partial Payment',2),('Notification',3),('OnGoing',4),('Billing',5),('Full Payment',6),('Closing',7);

insert into SrvActionItems([Desc],[Remarks],[SortNo],[SrvActionCodeId],[ServicesId])
values
-- Car Rental Activities --
('Arrange Vehicle','',1,1,1),
('Partial Payment','',2,2,1),
('Notify Driver',''  ,3,3,1),
('Notify Operator','',4,3,1),
('Notify Guest',''   ,5,3,1),
('On Progress',''    ,6,4,1),
('Payment',''        ,7,6,1),
('Closing',''        ,8,7,1),

-- Boat Rental --
('Special Request','',1,1,2),
('Partial Payment','',2,2,2),
('Notify Operator','',3,3,2),
('Notify Guest',''   ,4,3,2),
('On Progress',''    ,5,4,2),
('Full Payment',''   ,6,6,2),
('Closing',''        ,7,7,2),

-- Tour Package Activities --
('Special Request',''    ,1,1,3),
('Partial Payment',''    ,1,1,3),
('Book Airticket',''     ,1,1,3),
('Book Transportation','',1,1,3),
('Book Hotel',''         ,1,1,3),
('Book Destinations',''  ,1,1,3),
('Book Meals',''         ,1,1,3),
('Book Tour Guide',''    ,1,1,3),
('Notify Driver',''      ,2,3,3),
('Notify Tour Guide',''  ,3,3,3),
('Notify Guest',''       ,4,3,3),
('On progress',''        ,5,4,3),
('Full Payment',''       ,6,6,3),
('Closing',''            ,7,7,3);


-- ----------------------------------------------
-- Inventory Configuration 
-- ----------------------------------------------
insert into InvItemCats([Name],[Remarks],[ImgPath],[SysCode])
Values
('Vehicle','VAN/CAR','~/Images/CarRental/car101.png','CAR'),
('Driver','Driver','~/Images/CarRental/Driver102.png','DRIVER'),
('Others','Other Types','~/Images/CarRental/Repair101.png','OTHER');

-- ----------------------------------------------
-- Supplier PO Configuration
-- ----------------------------------------------
insert into SupplierPoStatus ([Status],[OrderNo]) values ('REQUEST',1),('APPROVED',2),('CANCELLED',2),('CONFIRMED',3),('DELIVERED',4),('CLOSE',5);

