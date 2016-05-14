CREATE PROCEDURE [dbo].[InsertErrorLog]
	(
	@appdomain varchar(256), 		
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
                                      [appdomain], [date], 
                                      exception, [identity], [level], 
                                      line, logger, [message], method, ndc, 
                                      property, stacktrace, stacktracedetail, 
                                      [timestamp], thread, [type], username, 
                                      utcdate
                                     ) 
                              VALUES (
                                      @appdomain, GETDATE(), 
                                      @exception, @identity,  
                                      @level, @line, @logger, @message, @method, 
                                      @ndc, @property, @stacktrace, @stacktracedetail, 
                                      @timestamp, @thread, @type, @username, 
                                      GETUTCDATE()
                                     )
END
