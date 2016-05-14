CREATE PROCEDURE [dbo].[InsertMvcErrorLog]
	(
	@appdomain varchar(256), 
	@aspnetcache varchar(max), 
	@aspnetcontext varchar(max), 
	@aspnetrequest varchar(max), 
	@aspnetsession varchar(max), 	 
	@exception varchar(max), 
	@identity varchar(256), 
	@level varchar(30), 
	@line int, 
	@logger varchar(100), 
	@message varchar(2000), 
	@method varchar(500), 
	@ndc varchar(500), 
	@property varchar(500), 
	@stacktrace varchar(max), 
	@stacktracedetail varchar(max), 
	@timestamp bigint, 
	@thread varchar(256), 
	@type varchar(256), 
	@username varchar(100)
	)
AS
BEGIN
	INSERT INTO dbo.ApplicationLog
                                     (
                                      [appdomain], aspnetcache, aspnetcontext, 
                                      aspnetrequest, aspnetsession, [date], 
                                      exception, [identity], [level], 
                                      line, logger, [message], method, ndc, 
                                      property, stacktrace, stacktracedetail, 
                                      [timestamp], thread, [type], username, 
                                      utcdate
                                     ) 
                              VALUES (
                                      @appdomain, @aspnetcache, @aspnetcontext, 
                                      @aspnetrequest, @aspnetsession, GETDATE(), 
                                      @exception, @identity, 
                                      @level, @line, @logger, @message, @method, 
                                      @ndc, @property, @stacktrace, @stacktracedetail, 
                                      @timestamp, @thread, @type, @username, 
                                      GETUTCDATE()
                                     )
END