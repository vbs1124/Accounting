USE [VservAccountingDB]
GO

/****** Object:  StoredProcedure [dbo].[ArchiveEmployee]    Script Date: 08-06-2016 12:17:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ArchiveEmployee]
	@EmployeeID INT,
	@UpdatedByUserName VARCHAR(250)
AS
BEGIN
	BEGIN TRY
		INSERT INTO [dbo].[EmployeeArchive] 
		SELECT [EmployeeId]
			,[FirstName]
			,[MiddleName]
			,[LastName]
			,[FatherName]
			,[MotherName]
			,[PermanentAccountNumber]
			,[UniversalAccountNumber]
			,[EPFNumber]
			,[AADHAARNumber]
			,[ESINumber]
			,[MobileNumber]
			,[OfficialEmailAddress]
			,[PersonalEmailAddress]
			,[BirthDay]
			,[JoiningDate]
			,[RelievingDate]
			,[ResignationDate]
			,[VBS_Id]
			,[DesignationId]
			,[SalutationId]
			,[GenderId]
			,[OfficeBranchId]
			,[PermanentAddress1]
			,[PermanentAddress2]
			,[PermanentCity]
			,[PermanentZipCode]
			,[PermanentStateId]
			,[PermanentCountryId]
			,[MailingAddress1]
			,[MailingAddress2]
			,[MailingCity]
			,[MailingZipCode]
			,[MailingStateId]
			,[MailingCountryId]
			,[IsMetro]
			,[BankAccountNumber]
			,[BankId]
			,[BankIFSCCode]
			,[BankMICRCode]
			,[IsActive]
			,[CreatedBy]
			,@UpdatedByUserName --[UpdatedBy]
			,[CreatedDate]
			,GETDATE() --[UpdatedDate]
		FROM Employee WHERE EmployeeId = @EmployeeID
	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber
			,ERROR_SEVERITY() AS ErrorSeverity
			,ERROR_STATE() AS ErrorState
			,ERROR_PROCEDURE() AS ErrorProcedure
			,ERROR_LINE() AS ErrorLine
			,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END

GO

