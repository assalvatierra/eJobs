insert into InvItems([ItemCode],[Description],[Remarks])
values ('RNY301','Toyota Innova E M/T 2013 Dsl',''),
('EOK873','Honda City A/T 2018 1.5E',''),
('AbelS','Abel Salvatierra','');


insert into InvItemCategories([InvItemCatId],[InvItemId])
values (1,1), (1,2), (2,3);

Insert into JobServiceItems([JobServicesId],[InvItemId])
values(1,2),(1,3);