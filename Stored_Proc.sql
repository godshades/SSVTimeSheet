Create proc Sp_InsertTime (
@UserId varchar(6),
@LeaderId varchar(6),
@ClassifyTime tinyint,
@StartTime datetime,
@EndTime datetime,
@Time float,
@ReasonId tinyint,
@NameContact nvarchar(250),
@PhoneContact varchar(15),
@Note nvarchar(500),
@Status tinyint
)
as 
insert into dbo.SRegistTime (UserId,LeaderId,ClassifyTime,StartTime,EndTime,[Time],ReasonId,NameContact,PhoneContact,Note, [Status])
values (@UserId,
@LeaderId,
@ClassifyTime,
@StartTime,
@EndTime,
@Time,
@ReasonId,
@NameContact,
@PhoneContact,
@Note,
@Status
)
go 

CREATE PROCEDURE Sp_UpdateSttRegistTime
@Id tinyint,
@Status tinyint
AS
BEGIN 
UPDATE SRegistTime 
SET [Status] =  @Status
WHERE Id = @Id
END 
GO

CREATE PROCEDURE Sp_SelectRequire (@LeaderId varchar(6))
as
SELECT * FROM dbo.SRegistTime join dbo.SUser on SRegistTime.UserId = SUser.UserId WHERE Status ='0' AND LeaderId = @LeaderId
GO


CREATE PROCEDURE Sp_UpdateRegistTime
@Id tinyint,
@UserId varchar(6),
@LeaderId varchar(6),
@ClassifyTime tinyint,
@StartTime datetime,
@EndTime datetime,
@Time float,
@ReasonId tinyint,
@NameContact nvarchar(250),
@PhoneContact varchar(15),
@Note nvarchar(500),
@Status tinyint
AS
BEGIN 
UPDATE SRegistTime 
SET UserId = @UserId,
LeaderId = @LeaderId,
ClassifyTime = @ClassifyTime,
StartTime = @StartTime,
EndTime = @EndTime,
[Time] = @Time,
ReasonId = @ReasonId,
NameContact = @NameContact,
PhoneContact = @PhoneContact,
Note = @Note,
[Status] = @Status
WHERE Id = @Id
END 
GO