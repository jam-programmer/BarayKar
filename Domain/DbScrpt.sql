
--GO
--/****** Object:  StoredProcedure [dbo].[GetBusinesses]    Script Date: 05/04/1403 03:44:16 ب.ظ ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
---- =============================================
---- Run: EXEC GetBusinesses
---- Author:		<Movahedijam>
---- Create date: <1403-04-01>
---- Description:	<نمایش کسب و کارها به همراه فیلتر>
---- =============================================
--CREATE OR ALTER PROCEDURE [dbo].[GetBusinesses]
--	 @page INT=1,
--	 @pageSize INT =16,
--	 @search NVARCHAR(300)='',
--	 @provinces NVARCHAR(MAX)='',
--	 @cities NVARCHAR(MAX)='',
--	 @categories NVARCHAR(MAX)=''
--AS
--BEGIN
--SET @page=(@page -1) * @pageSize 
--	DECLARE @QUERY NVARCHAR(MAX)
--	SET	@QUERY=' SELECT B.Id,B.AccountName,B.Code,P.[Name] AS ProvinceName,C.[NAME] AS CityName,CY.[NAME] AS CategoryName,
--				(SELECT TOP(1) BP.Image FROM BIS.BusinessPicture AS BP WITH(NOLOCK) WHERE BP.BusinessId=B.Id) AS BusinessPictureImage
--				FROM BIS.Business AS B WITH(NOLOCK)
--					INNER JOIN DBO.Province AS P WITH(NOLOCK) ON B.ProvinceId=P.Id
--					INNER JOIN DBO.City AS C WITH(NOLOCK) ON P.Id=C.ProvinceId
--					INNER JOIN DBO.Category AS CY WITH(NOLOCK) ON B.CategoryId=CY.Id WHERE B.IsActive=1 AND B.[status] = 0 '
	
--	IF @search IS NOT NULL AND  @search <> ''
--		BEGIN
--			SET @QUERY = @QUERY +  'AND B.AccountName LIKE N''%' + @search + '%'' '
--		END

--	IF @categories IS NOT NULL AND  @categories <> ''
--		BEGIN
--			SET @QUERY = @QUERY +  'AND CY.Id IN ('''+ @categories +''')'
--		END

--	IF @provinces IS NOT NULL AND  @provinces <> ''
--		BEGIN
--			SET @QUERY = @QUERY + 'AND P.Id IN ('''+ @provinces +''')'
--		END

--	IF @cities IS NOT NULL AND  @cities <> ''
--		BEGIN
--			SET @QUERY = @QUERY +  'AND C.Id IN ('''+ @cities +''')'
--		END

--		SET @QUERY= @QUERY + ' ORDER BY B.CreateTime '
--		SET @QUERY= @QUERY + ' OFFSET '+ CAST(@page AS NVARCHAR(10)) + ' ROWS FETCH NEXT '+ CAST(@pageSize AS NVARCHAR(10)) +' ROWS ONLY  '
		
					
--EXEC sp_executesql @QUERY			
----print @QUERY

--END

--GO
--/****** Object:  StoredProcedure [dbo].[GetEmployments]    Script Date: 07/04/1403 11:34:42 ب.ظ ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
---- =============================================
---- Run: EXEC GetBusinesses
---- Author:		<Movahedijam>
---- Create date: <1403-04-01>
---- Description:	<نمایش آگهی های استخدام به همراه فیلتر>
---- =============================================
--ALTER   PROCEDURE [dbo].[GetEmployments]
--	 @page INT=1,
--	 @pageSize INT =18,
--	 @search NVARCHAR(300)='',
--	 @provinces NVARCHAR(MAX)='',
--	 @cities NVARCHAR(MAX)='',
--	 @businesses NVARCHAR(MAX)=''
--AS
--BEGIN
--SET @page=(@page -1) * @pageSize 
--	DECLARE @QUERY NVARCHAR(MAX)
--	SET	@QUERY=' SELECT E.Id,E.Code,E.Title,E.Salary,B.AccountName AS BusinessAccountName ,P.[Name] AS ProvinceName,C.[NAME] AS CityName,
--	(SELECT TOP(1) BP.Image FROM BIS.BusinessPicture AS BP WITH(NOLOCK) WHERE BP.BusinessId=B.Id) AS Image
--	FROM EMP.Employment AS E WITH(NOLOCK)
--		INNER JOIN DBO.Province AS P WITH(NOLOCK) ON E.ProvinceId=P.Id
--		INNER JOIN DBO.City AS C WITH(NOLOCK) ON P.Id=C.ProvinceId
--		INNER JOIN BIS.Business AS B WITH(NOLOCK) ON E.BusinessId=B.Id
--		WHERE E.IsActive=1 AND E.[status]=0'
	

	






--	IF @search IS NOT NULL AND  @search <> ''
--		BEGIN
--			SET @QUERY = @QUERY +  'AND E.Title LIKE N''%' + @search + '%'' '
--		END

--	IF @businesses IS NOT NULL AND  @businesses <> ''
--		BEGIN
--			SET @QUERY = @QUERY + 'AND B.Id IN ('''+ @businesses +''')'
--		END

--	IF @provinces IS NOT NULL AND  @provinces <> ''
--		BEGIN
--			SET @QUERY = @QUERY + 'AND P.Id IN ('''+ @provinces +''')'
--		END

--	IF @cities IS NOT NULL AND  @cities <> ''
--		BEGIN
--			SET @QUERY = @QUERY +  'AND C.Id IN ('''+ @cities +''')'
--		END

--		SET @QUERY= @QUERY + ' ORDER BY B.CreateTime '
--		SET @QUERY= @QUERY + ' OFFSET '+ CAST(@page AS NVARCHAR(10)) + ' ROWS FETCH NEXT '+ CAST(@pageSize AS NVARCHAR(10)) +' ROWS ONLY  '
		
					
--EXEC sp_executesql @QUERY			
----print @QUERY

--END
