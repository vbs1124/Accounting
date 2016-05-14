CREATE PROCEDURE [dbo].[InsertMvcErrorLog]
	(
	@appdomain			VARCHAR(256), 
	@aspnetcache		VARCHAR(MAX), 
	@aspnetcontext		VARCHAR(MAX), 
	@aspnetrequest		VARCHAR(MAX), 
	@aspnetsession		VARCHAR(MAX), 	 
	@exception			VARCHAR(MAX), 
	@identity			VARCHAR(256), 
	@level				VARCHAR(30), 
	@line				INT, 
	@logger				VARCHAR(100), 
	@message			VARCHAR(2000), 
	@method				VARCHAR(500), 
	@ndc				VARCHAR(500), 
	@property			VARCHAR(500), 
	@stacktrace			VARCHAR(MAX), 
	@stacktracedetail	VARCHAR(MAX), 
	@timestamp			BIGINT, 
	@thread				VARCHAR(256), 
	@type				VARCHAR(256), 
	@username			VARCHAR(100)
	)
AS
BEGIN
INSERT INTO [dbo].[ApplicationLog]
           ([AppDomain]
           ,[AspnetCache]
           ,[AspnetContext]
           ,[AspnetRequest]
           ,[AspnetSession]
           ,[Date]
           ,[Exception]
           ,[Identity]
           ,[Level]
           ,[Line]
           ,[Logger]
           ,[Message]
           ,[Method]
           ,[NDC]
           ,[Property]
           ,[StackTrace]
           ,[StackTraceDetail]
           ,[TimeStamp]
           ,[Thread]
           ,[Type]
           ,[Username]
           ,[UTCDate])
     VALUES
           (@appdomain -- <AppDomain, varchar(256),>
           ,@aspnetcache -- <AspnetCache, varchar(max),>
           ,@aspnetcontext -- <AspnetContext, varchar(max),>
           ,@aspnetrequest -- <AspnetRequest, varchar(max),>
           ,@aspnetsession -- <AspnetSession, varchar(max),>
           ,GETDATE() -- <Date, datetime,>
           ,@exception -- <Exception, varchar(max),>
           ,@identity -- <Identity, varchar(256),>
           ,@level -- <Level, varchar(30),>
           ,@line -- <Line, int,>
           ,@logger -- <Logger, varchar(100),>
           ,@message -- <Message, varchar(2000),>
           ,@method -- <Method, varchar(500),>
           ,@ndc -- <NDC, varchar(500),>
           ,@property -- <Property, varchar(500),>
           ,@stacktrace -- <StackTrace, varchar(max),>
           ,@stacktracedetail -- <StackTraceDetail, varchar(max),>
           ,@timestamp -- <TimeStamp, bigint,>
           ,@thread -- <Thread, varchar(256),>
           ,@type -- <Type, varchar(256),>
           ,@username -- <Username, varchar(100),>
           ,GETUTCDATE() -- <UTCDate, datetime,>
		   )
END