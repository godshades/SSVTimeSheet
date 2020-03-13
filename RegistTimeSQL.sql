create table [SRegistTime](
	Id INT IDENTITY(1,1) Primary key not null
	UserId VARCHAR(6) Primary Key not null,
	LeaderId VARCHAR(6) not null,
	ClassifyTime NVARCHAR(250),
	StartWorkTime DATETIME,
	EndWorkTime DATETIME,
	WorkTime FLOAT,
	StartRestTime DATETIME,
	EndRestTime DATETIME,
	RestTime TINYINT,
	Note NVARCHAR(500),
	[Status] BIT,
)