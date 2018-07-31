
select
'New SalesLead' as StatusCategory, 
a.DtEntered dtTaken,  
a.Id refId, a.CustName + ' / ' + a.Details Details 
from SalesLeads a
where datediff(day, getdate(),a.DtEntered) > -2

union

select 
'SalesLead Activity' as StatusCategory,
a.DtActivity dtTaken,
a.SalesLeadId refId,
a.Particulars Details
from 
SalesActivities a
where a.SalesActStatusId=2 AND datediff(day, getdate(),a.DtActivity) > -2;

