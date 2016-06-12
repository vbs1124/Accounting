CREATE PROCEDURE [dbo].[InsertInfoLog]
	(
	@appdomain	VARCHAR(256),		
	@identity	VARCHAR(256),  
	@level		VARCHAR(30), 	
	@logger		VARCHAR(100), 
	@message	VARCHAR(2000), 
	@method		VARCHAR(500), 
	@timestamp	BIGINT, 
	@thread		VARCHAR(256), 
	@type		VARCHAR(256), 
	@username	VARCHAR(100))
AS
BEGIN
	BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO [dbo].[ApplicationLog]
			   ([AppDomain]
			   ,[Date]
			   ,[Identity]
			   ,[Level]
			   ,[Logger]
			   ,[Message]
			   ,[Method]
			   ,[TimeStamp]
			   ,[Thread]
			   ,[Type]
			   ,[Username]
			   ,[UTCDate])
		 VALUES
			   (@appdomain -- <AppDomain, varchar(256),>
			   ,GETDATE() -- <Date, datetime,>
			   ,@identity -- <Identity, varchar(256),>
			   ,@level -- <Level, varchar(30),>
			   ,@logger -- <Logger, varchar(100),>
			   ,@message -- <Message, varchar(2000),>
			   ,@method -- <Method, varchar(500),>
			   ,@timestamp -- <TimeStamp, bigint,>
			   ,@thread -- <Thread, varchar(256),>
			   ,@type -- <Type, varchar(256),>
			   ,@username -- <Username, varchar(100),>
			   ,GETUTCDATE() -- <UTCDate, datetime,>
			   )
	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber
			,ERROR_SEVERITY() AS ErrorSeverity
			,ERROR_STATE() AS ErrorState
			,ERROR_PROCEDURE() AS ErrorProcedure
			,ERROR_LINE() AS ErrorLine
			,ERROR_MESSAGE() AS ErrorMessage;
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION
END

GO

