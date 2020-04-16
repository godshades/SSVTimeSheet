	CREATE DATABASE SSVTimeSheet
GO
USE SSVTimeSheet
GO

CREATE table [SWorkTime](
	Id INT IDENTITY(1,1) Primary key not NULL,
	UserId CHAR(6) not null,	
	JobType TINYINT NOT NULL,
	StartDtTm DATETIME NOT NULL,
	EndDtTm DATETIME NOT NULL,
	LeaderId CHAR(6) not null,
	IsHoliday BIT,
	IsWeekend BIT,
	RestReasonId TINYINT,
	RestNameContact NVARCHAR(30),
	RestPhoneContact CHAR(10),
	TotalTime FLOAT NOT NULL,	
	Note NVARCHAR(100),
	InsertDt DATETIME,
	[Status] TINYINT,
)
GO

CREATE table [SUser](	
	UserId CHAR(6) PRIMARY KEY NOT NULL,	
	[Password] CHAR(32) NOT NULL,
	FullName NVARCHAR(30) NOT NULL,	
	LeaderId CHAR(6),
	UserTypeId TINYINT NOT NULL,
	JoinDt DATETIME NOT NULL,
	BirthDt DATETIME NOT NULL,
	Email VARCHAR(30) NOT NULL,
	TelNo CHAR(10) NOT NULL,
	RestDay TINYINT NOT NULL,
	DelFlg BIT NOT NULL
)
GO


CREATE table [SType](	
	TypeCd CHAR(2) NOT NULL,
	TypeName NVARCHAR(25) NOT NULL,
	TypeValue TINYINT NOT NULL,
	TypeValueName NVARCHAR(30) NOT NULL
)
GO



CREATE table [SHoliday](	
	Id INT NOT NULL,
	Holiday DATE NOT NULL,
	HolidayType TINYINT NOT NULL,
	Other VARCHAR(200)
)
GO


INSERT INTO dbo.SWorkTime( UserId ,JobType ,StartDtTm ,EndDtTm ,LeaderId ,IsHoliday ,IsWeekend ,RestReasonId ,RestNameContact ,RestPhoneContact ,TotalTime ,Note ,InsertDt ,[Status]) VALUES  (@UserId ,@JobType ,@StartDtTm ,@EndDtTm ,@LeaderId ,@IsHoliday ,@IsWeekend ,@RestReasonId ,@RestNameContact ,@RestPhoneContact ,@TotalTime ,@Note ,@InsertDt ,@Status)

UPDATE dbo.SWorkTime SET [Status] = @Status WHERE Id = @Id
UPDATE dbo.SWorkTime SET UserId = @UserId, LeaderId = @LeaderId, JobType = @JobType, StartDtTm = @StartDtTm, EndDtTm = @EndDtTm, [TotalTime] = @TotalTime ,RestReasonId = @RestReasonId, RestNameContact = @RestNameContact ,RestPhoneContact = @RestPhoneContact, Note = @Note, [Status] = @Status WHERE Id = @Id


INSERT INTO dbo.SUser( UserId ,[Password] ,FullName ,LeaderId ,UserTypeId ,JoinDt ,BirthDt ,Email ,TelNo ,RestDay ,DelFlg)VALUES  ( @UserId ,@Password ,@FullName ,@LeaderId ,@UserTypeId ,@JoinDt ,@BirthDt ,@Email ,@TelNo ,@RestDay ,@DelFlg) 


	UPDATE dbo.SUser SET [Password] = @Password, FullName = @FullName , LeaderId = @LeaderId, UserTypeId = @UserTypeId, JoinDt = @JoinDt,BirthDt = @BirthDt, Email = @Email, TelNo = @TelNo, RestDay = @RestDay WHERE UserId = @UserId


	SELECT COUNT(*) FROM dbo.SUser WHERE UserId = 'VN0001'
	INSERT INTO dbo.SWorkTime
	        ( UserId ,
	          JobType ,
	          StartDtTm ,
	          EndDtTm ,
	          LeaderId ,
	          IsHoliday ,
	          IsWeekend ,
	          RestReasonId ,
	          RestNameContact ,
	          RestPhoneContact ,
	          TotalTime ,
	          Note ,
	          InsertDt ,
	          Status
	        )
			OUTPUT Inserted.Id
	VALUES  ( 'VN0050' , -- UserId - char(6)
	          1 , -- JobType - tinyint
	          GETDATE() , -- StartDtTm - datetime
	          GETDATE() , -- EndDtTm - datetime
	          'VN0020' , -- LeaderId - char(6)
	          NULL , -- IsHoliday - bit
	          NULL , -- IsWeekend - bit
	          1 , -- RestReasonId - tinyint
	          N'QUy?n' , -- RestNameContact - nvarchar(30)
	          '0966304660' , -- RestPhoneContact - char(10)
	          1.0 , -- TotalTime - float
	          N'Không' , -- Note - nvarchar(100)
	          GETDATE() , -- InsertDt - datetime
	          0  -- Status - tinyint
	        )