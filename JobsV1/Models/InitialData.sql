insert into Cities(Name) values('Davao');
insert into Branches(Name, CityId, Remarks, Address, Landline, Mobile) values('Davao-01',1,'Davao Main','2nd Floor Sulit Bldg, Mac Arthur Hwy, Matina','082 297-1831','');
insert into SupplierTypes(Description) values('Rent-a-car');
insert into SupplierTypes(Description) values('Boat'),('Tour'),('Airline'),('Hotel');
insert into JobStatus([Status]) values('INQUIRY'),('RESERVATION'),('CONFIRMED'),('CLOSED'),('CANCELLED');
insert into JobThrus([Desc]) values('PHONE'),('EMAIL'),('WALKIN');
insert into Services([Name],[Description]) values('Car Rental','Bus, Car, Van and other Transportation arrangements'),('Boat Rental','Boat Arrangement, Island Hopping'),('Tour','Tour Package, Land arrangements'),('AirTicket','Airline Ticket'),('Accommodation','Hotels, Rooms, Houses, etc'),('Other','Other types of services');
insert into Banks([BankName],[BankBranch],[AccntName],[AccntNo]) values ('Cash','Davao','Cash','0'),('BDO','SM-Ecoland Davao','AJ88 Car Rental Services','00 086 072 9575'),('BPI','SM-Ecoland Davao','Abel S. Salvatierra','870 303 5125');

insert into Customers([Name],[Email],[Contact1],[Contact2],[Remarks],[Status]) values('<< New Customer >>','--','--',' ',' ','ACT');
insert into Customers([Name],[Email],[Contact1],[Contact2],[Remarks],[Status]) values('RealBreeze-Davao','realbreezedavao@gmail.com','Elvie/0916-755-8473','','','ACT');

insert Into Suppliers([Name],[Contact1],[Details],[Email],[CityId],[SupplierTypeId]) values('<< New Supplier >>','--',' ', '--','1','1');
insert Into Suppliers([Name],[Contact1],[Details],[Email],[CityId],[SupplierTypeId]) values('AJ Davao Car Rental','Abel / 0995-085-0158',' ', 'AJDavao88@gmail.com','1','1');
insert into SupplierItems([Description],[SupplierId],[Remarks],[InCharge]) values ('Default','1','Item by supplier','Supplier');


