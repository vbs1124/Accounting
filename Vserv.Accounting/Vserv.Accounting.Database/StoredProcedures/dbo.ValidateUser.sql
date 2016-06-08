USE [VservAccountingDB]
GO

/****** Object:  StoredProcedure [dbo].[ValidateUser]    Script Date: 08-06-2016 12:18:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValidateUser]
	@UserName VARCHAR(250),
	@SecurityQuestionId INT,
	@Answer VARCHAR(250),
	@EmailAddress VARCHAR(250),
	@MobileNumber VARCHAR(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET FMTONLY OFF;
	DECLARE @IsValidUser BIT = 0;

	SELECT @IsValidUser = CASE WHEN(COUNT(1) > 0) THEN 1 ELSE 0 END  FROM UserProfile UP
	INNER JOIN Membership M ON M.UserId = UP.UserId
	LEFT JOIN UserSecurityQuestion USQ ON USQ.UserId=UP.UserId AND  USQ.SecurityQuestionId = @SecurityQuestionId
	WHERE UP.LoweredUserName = @UserName
	AND (M.Email = @EmailAddress OR UP.MobileAlias = @MobileNumber)
	AND USQ.Answer = @Answer

	SELECT @IsValidUser AS IsValidUser
END

GO

