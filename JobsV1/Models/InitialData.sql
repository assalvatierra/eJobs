insert into Cities(Name) values('Davao');
insert into Branches(Name, CityId, Remarks, Address, Landline, Mobile) values('Davao-01',1,'Davao Main','2nd Floor Sulit Bldg, Mac Arthur Hwy, Matina','082 297-1831','');
insert into SupplierTypes(Description) values('Rent-a-car');
insert into SupplierTypes(Description) values('Boat'),('Tour'),('Airline'),('Hotel');
insert into JobStatus([Status]) values('INQUIRY'),('RESERVATION'),('CONFIRMED'),('CLOSED'),('CANCELLED'),('TEMPLATE');
insert into JobThrus([Desc]) values('PHONE'),('EMAIL'),('WALKIN');
insert into Services([Name],[Description]) values('Car Rental','Bus, Car, Van and other Transportation arrangements'),('Boat Rental','Boat Arrangement, Island Hopping'),('Tour','Tour Package, Land arrangements'),('AirTicket','Airline Ticket'),('Accommodation','Hotels, Rooms, Houses, etc'),('Other','Other types of services');
insert into Banks([BankName],[BankBranch],[AccntName],[AccntNo]) values ('Cash','Davao','Cash','0'),('BDO','SM-Ecoland Davao','AJ88 Car Rental Services','00 086 072 9575'),('BPI','SM-Ecoland Davao','Abel S. Salvatierra','870 303 5125');

insert into Customers([Name],[Email],[Contact1],[Contact2],[Remarks],[Status]) values('<< New Customer >>','--','--',' ',' ','ACT');
insert into Customers([Name],[Email],[Contact1],[Contact2],[Remarks],[Status]) values('RealBreeze-Davao','realbreezedavao@gmail.com','Elvie/0916-755-8473','','','ACT');

insert Into Suppliers([Name],[Contact1],[Details],[Email],[CityId],[SupplierTypeId]) values('<< New Supplier >>','--',' ', '--','1','1');
insert Into Suppliers([Name],[Contact1],[Details],[Email],[CityId],[SupplierTypeId]) values('AJ Davao Car Rental','Abel / 0995-085-0158',' ', 'AJDavao88@gmail.com','1','1');
insert into SupplierItems([Description],[SupplierId],[Remarks],[InCharge]) values ('Default','1','Item by supplier','Supplier');


insert Into Destinations([Description],[Remarks],[CityId]) 
values 
('Eden Nature Park','','1 '),
('Philippine Eagle','','1 '),
('Malagos Garden','','1 '),
('Japanese Tunnel','','1 ')
;


insert into CustCategories([Name])
values ('PRIORITY'),('ACTIVE'),('SUSPENDED'),('BAD ACCOUNT'); 

insert into CustEntMains([Name],[Address],[Contact1],[Contact2])
values ('NEW (not yet defined)',' ',' ',' ');

insert into SalesStatusCodes([SeqNo],[Name])
values (1,'NEW'), (2,'ASSESMENT'), (3, 'PROPOSAL SENT'), (4, 'NEGOTIATION'), (5, 'ACCEPTED'), (6, 'REJECTED');

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