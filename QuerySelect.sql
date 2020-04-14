er
	-- Query cho leader
	SELECT *,(SELECT  TypeValueName FROM SType t where r.RestReasonId = t.TypeValue and TypeCd = '3') AS RestReasonName, (SELECT TypeValueName FROM SType WHERE TypeValue = r.JobType AND TypeCd = '1') AS JobTypeName FROM dbo.SWorkTime r JOIN dbo.SUser u ON r.UserId = u.UserId WHERE r.[Status] = '1' AND r.LeaderId = 'VN0020'
	-- Query cho manager
	SELECT *,(SELECT  TypeValueName FROM SType t where r.RestReasonId = t.TypeValue and TypeCd = '3') AS RestReasonName,(SELECT TypeValueName FROM SType WHERE TypeValue = r.JobType AND TypeCd = '1') AS JobTypeName FROM dbo.SWorkTime r JOIN dbo.SUser u ON r.UserId = u.UserId WHERE r.[Status] = '3' AND r.LeaderId = (SELECT u.UserId FROM dbo.SUser u WHERE u.LeaderId = 'VN0010' AND r.LeaderId = u.UserId)

-- Đếm số yêu cầu còn lại:
SELECT COUNT(*) FROM dbo.SWorkTime r WHERE [Status] = '3' AND LeaderId = (SELECT u.UserId FROM dbo.SUser u WHERE u.LeaderId = 'VN0010' AND r.LeaderId = u.UserId)



-- Xoá time đã đăng ký 

DELETE FROM dbo.SWorkTime WHERE Id = 1;



-- Lay tat ca User cho trang Admin

SELECT *,(SELECT FullName FROM dbo.SUser s WHERE u.LeaderId = s.UserId) AS [LeaderName],(SELECT TypeValueName FROM dbo.SType t WHERE u.UserTypeId = t.TypeValue AND t.TypeCd = '2') AS [UserTypeName] FROM [SSVTimeSheet].[dbo].[SUser] u WHERE u.DelFlg = 'false'

UPDATE dbo.SUser SET DelFlg = 'false' WHERE UserId = 'VN0011'

-- Lay quyen user 
SELECT * FROM dbo.SType WHERE TypeCd ='2'


SELECT *,(SELECT FullName FROM dbo.SUser s WHERE u.LeaderId = s.UserId) AS [LeaderName],(SELECT TypeValueName FROM dbo.SType t WHERE u.UserTypeId = t.TypeValue AND t.TypeCd = '2') AS [UserTypeName] FROM [SSVTimeSheet].[dbo].[SUser] u WHERE UserId = 'VN0001'