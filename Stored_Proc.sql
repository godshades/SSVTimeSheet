Create proc Sp_InsertTime (
@UserId varchar(6),
@LeaderId varchar(6),
@ClassifyTime tinyint,
@StartTime datetime,
@EndTime datetime,
@Time float,
@Reason tinyint,
@NameContact nvarchar(250),
@PhoneContact varchar(15),
@Note nvarchar(500),
@Status tinyint
)
as 
insert into dbo.SRegistTime (UserId,LeaderId,ClassifyTime,StartTime,EndTime,[Time],Reason,NameContact,PhoneContact,Note, [Status])
values (@UserId,
@LeaderId,
@ClassifyTime,
@StartTime,
@EndTime,
@Time,
@Reason,
@NameContact,
@PhoneContact,
@Note,
@Status
)
go 
