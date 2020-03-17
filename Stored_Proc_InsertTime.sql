Create proc Sp_InsertWorkTime(
@UserId varchar(6),
@LeaderId varchar(6),
@ClassifyTime nvarchar(250),
@StartWorkTime datetime,
@EndWorkTime datetime,
@WorkTime float,
@Note nvarchar(500))
as 
insert into dbo.SRegistTime (UserId,LeaderId,ClassifyTime,StartWorkTime,EndWorkTime,WorkTime,Note)
values (@UserId,
@LeaderId,
@ClassifyTime,
@StartWorkTime,
@EndWorkTime,
@WorkTime,
@Note)
go 

Create proc Sp_InsertRestTime(
@UserId varchar(6),
@LeaderId varchar(6),
@StartRestTime datetime,
@EndRestTime datetime,
@RestTime float,
@Note nvarchar(500))
as 
insert into dbo.SRegistTime (UserId,LeaderId,StartRestTime,EndRestTime,RestTime,Note)
values (@UserId,
@LeaderId,
@StartRestTime,
@EndRestTime,
@RestTime,
@Note)
go