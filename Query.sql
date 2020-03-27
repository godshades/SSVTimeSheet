SELECT R.Id
	,(
		SELECT TypeName
		FROM SType
		WHERE TypeId = R.ClassifyTime
			AND GroupId = 1
		) AS ClassifyTime
	,(
		SELECT TypeName
		FROM SType
		WHERE TypeId = R.ReasonId
			AND GroupId = 3
		) AS ReasonId
	,(
		SELECT TypeName
		FROM SType
		WHERE TypeId = R.[Status]
			AND GroupId = 4
		) AS [Status]
FROM SRegistTime R

SELECT *,
(select  TypeId  from SType t where r.Status = t.TypeId and GroupId = '3')
FROM dbo.SRegistTime r
JOIN dbo.SUser u ON r.UserId = u.UserId
WHERE [Status] = '1'
	AND LeaderId = 'VN0022'
	

	SELECT *,(SELECT  TypeName FROM SType t where r.ReasonId = t.TypeId and GroupId = '3') FROM dbo.SRegistTime r JOIN dbo.SUser u ON r.UserId = u.UserId WHERE [Status] = '1' AND LeaderId = 'VN0022'
	SELECT *FROM dbo.SRegistTime WHERE Status ='1' AND LeaderId = 'VN0022'