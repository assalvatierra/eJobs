

insert into Customers(Name, Email, Contact1, Contact2, Remarks, Status) 
values('John Doe','johndoe@gmail.com','09950753794','09950753794','Test User','ACT');

insert into CustCats(CustomerId, CustCategoryId) 
values(3,2);

insert into CustEntMains(Name, Address, Contact1, Contact2, iconPath) 
values('NewCompany.Inc','Davao City','09950753794','09950753794','http://localhost:50382/Images/Customers/Company/organization-40.png');

insert into CustEntities(CustEntMainId, CustomerId) 
values(0,3);

update CustCategories set iconPath = 'http://localhost:50382/Images/Customers/Category/star-filled-40.png' where Id = 1; 
update CustCategories set iconPath = 'http://localhost:50382/Images/Customers/Category/Active-30.png' where Id = 2; 
update CustCategories set iconPath = 'http://localhost:50382/Images/Customers/Category//suspended-64.png' where Id = 3; 
update CustCategories set iconPath = 'http://localhost:50382/Images/Customers/Category/cancel-40.png' where Id = 4; 