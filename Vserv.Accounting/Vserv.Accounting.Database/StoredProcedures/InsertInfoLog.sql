CREATE PROCEDURE [dbo].[InsertInfoLog]
	(
	@appdomain varchar(256),		
	@identity varchar(256),  
	@level varchar(30), 	
	@logger varchar(100), 
	@message varchar(2000), 
	@method varchar(500), 
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
                                       [identity],  [level], 
                                      logger, [message], method, 
                                      
                                      [timestamp], thread, [type], username, 
                                      utcdate
                                     ) 
                              VALUES (
                                      @appdomain, GETDATE(), 
                                      @identity,  
                                      @level, @logger, @message, @method,                                       
                                      @timestamp, @thread, @type, @username, 
                                      GETUTCDATE()
                                     )
END
