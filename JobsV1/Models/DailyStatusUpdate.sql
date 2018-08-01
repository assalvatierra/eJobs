
select
'New SalesLead' as StatusCategory, 
a.DtEntered dtTaken,  
a.Id refId, a.CustName + ' / ' + a.Details Details 
from SalesLeads a
where datediff(day, getdate(),a.DtEntered) > - 1

union

select 
'SalesLead Activity' as StatusCategory,
a.DtActivity dtTaken,
a.SalesLeadId refId,
a.Particulars Details
from 
SalesActivities a
where a.SalesActStatusId=2 AND datediff(day, getdate(),a.DtActivity) > -2

union

select 
'JobOrder Activity' as StatusCategory,
j.DtPerformed as dtTaken,
j.JobServicesId as refId,
j.Remarks as Details
from 
JobActions j
where datediff(day, getdate(),j.DtPerformed) > - 1;

