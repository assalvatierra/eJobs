select * from JobServices a
left outer join JobServiceItems b on b.JobServicesId = a.Id
;