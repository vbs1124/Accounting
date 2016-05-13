/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DECLARE @CurrentUTCDate DATETIME		= GETUTCDATE()
DECLARE @CurrentDate	DATETIME		= GETDATE()
DECLARE @UserID			INT				= 1

SET NOCOUNT ON

BEGIN
	print '[dbo].[AddressType]'
	DECLARE @AddressTypeTable TABLE(
	[OfficeBranchId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nchar](6) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL)
 
	INSERT INTO @AddressTypeTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('ADR000', 'Unknown', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @AddressTypeTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('ADR001', 'Permanent Address', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @AddressTypeTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('ADR002', 'Mailing Address', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)
  
	MERGE [dbo].[AddressType] AS d  -- destination
	USING (SELECT [Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate] FROM @AddressTypeTable) AS s -- source
	ON (d.[Code] = s.[Code])
	WHEN MATCHED THEN
	UPDATE SET d.[Code] = s.[Code]
		  ,d.[Name] = s.[Name]
		  ,d.[Description] = s.[Description]
		  ,d.[DisplayOrder] = s.[DisplayOrder]
		  ,d.[IsActive] = s.[IsActive]
		  ,d.[CreatedById] = s.[CreatedById]
		  ,d.[UpdatedById] = s.[UpdatedById]
		  ,d.[CreatedDate] = s.[CreatedDate]
		  ,d.[UpdatedDate] = s.[UpdatedDate]
	WHEN NOT MATCHED THEN 
	INSERT 
	VALUES (s.[Code], s.[Name], s.[Description], s.[DisplayOrder], s.[IsActive], s.[CreatedById], s.[UpdatedById], s.[CreatedDate], s.[UpdatedDate]);
END

BEGIN 
	print '[dbo].[Department]'

	DECLARE @DepartmentTable TABLE(
	[OfficeBranchId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nchar](6) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL)
 
	INSERT INTO @DepartmentTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DPT000', 'Unknown', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DepartmentTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DPT001', 'Admin', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DepartmentTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DPT002', 'Account', '', 0, 1, @UserID, NULL, @CurrentDate, NULL) 

	INSERT INTO @DepartmentTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DPT003', 'Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL) 

	INSERT INTO @DepartmentTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DPT004', 'User', '', 0, 1, @UserID, NULL, @CurrentDate, NULL) 

	INSERT INTO @DepartmentTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('PMT005', 'PMO', '', 0, 1, @UserID, NULL, @CurrentDate, NULL) 
  
	MERGE [dbo].[Department] AS d  -- destination
	USING (SELECT [Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate] FROM @DepartmentTable) AS s -- source
	ON (d.[Code] = s.[Code])
	WHEN MATCHED THEN
	UPDATE SET d.[Code] = s.[Code]
		  ,d.[Name] = s.[Name]
		  ,d.[Description] = s.[Description]
		  ,d.[DisplayOrder] = s.[DisplayOrder]
		  ,d.[IsActive] = s.[IsActive]
		  ,d.[CreatedById] = s.[CreatedById]
		  ,d.[UpdatedById] = s.[UpdatedById]
		  ,d.[CreatedDate] = s.[CreatedDate]
		  ,d.[UpdatedDate] = s.[UpdatedDate]
	WHEN NOT MATCHED THEN 
	INSERT 
	VALUES (s.[Code], s.[Name], s.[Description], s.[DisplayOrder], s.[IsActive], s.[CreatedById], s.[UpdatedById], s.[CreatedDate], s.[UpdatedDate]);

END

BEGIN 
	print '[dbo].[Designation]'
	DECLARE @DesignationTable TABLE(
	[OfficeBranchId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nchar](6) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG000', 'Unknown', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG001', 'CEO', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG002', 'Program Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG003', 'Project Lead', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG004', 'Team Lead', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG005', 'Senior Software Engineer', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG006', 'Software Engineer', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG007', 'Software Trainee', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG008', 'Test Engineer', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG009', 'Unassigned', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG010', 'Manager Sales', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG011', 'Admin Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG012', 'Business Development Executive', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG013', 'Account Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG014', 'Executive Tele-sales', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG015', 'Graphics/Web Developer', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG016', 'Research Executive', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG017', 'Alliance Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG018', 'Assistant Manager - Resourcing', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG019', 'Assistant Manager - IT', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG020', 'Assistant Manager - Admin', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG021', 'Assistant Project Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG022', 'Assistant Test Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG023', 'Associate Team Lead', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG024', 'Associate Team Lead - Finance', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG025', 'Director - Global Sales', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG026', 'Group Manager - Delivery', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG027', 'Group Manager - Testing', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG028', 'Manager - Finance', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG029', 'Manager - IT', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG030', 'Network Engineer', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG031', 'Operations Head', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG032', 'Project Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG033', 'Research Executive Trainee', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG034', 'Senior Executive - Resourcing', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG035', 'Senior Executive - HR', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG036', 'Senior Manager - Quality', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG037', 'Senior Network Engineer', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG038', 'Senior System Analyst', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG039', 'Senior Test Engineer', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG040', 'System Analyst', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG041', 'Technical Architect', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG042', 'Test Lead', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)
	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG043', 'Test Trainee', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG044', ' Executive - Resourcing', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG045', 'Consultant', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG046', 'Test Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG047', 'Technical Lead', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG048', 'Principal Consultant', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG050', 'Associate Test lead', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG051', 'Associate Technical Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG052', 'Sr. Consultant', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG053', 'Technical Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG054', 'Delivery Head', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG055', 'Oracle Apps DBA', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG056', 'Sr. Executive Finance', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG057', 'Information Security Analyst', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG058', 'Lead Architect', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG059', 'Head Architect', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG060', 'Associate Tech Lead', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG061', 'Sr. Test Analyst', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG062', 'Sr. Test Manager', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG063', 'Sr. Technical Lead', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG064', 'Test Analyst', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG065', 'Associate Technical Architect', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG066', 'Data Analyst', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG067', 'Network Trainee', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @DesignationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('DSG068', 'Sr. Data Analyst', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	MERGE [dbo].[Designation] AS d  -- destination
	USING (SELECT [Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate] FROM @DesignationTable) AS s -- source
	ON (d.[Code] = s.[Code])
	WHEN MATCHED THEN
	UPDATE SET d.[Code] = s.[Code]
		  ,d.[Name] = s.[Name]
		  ,d.[Description] = s.[Description]
		  ,d.[DisplayOrder] = s.[DisplayOrder]
		  ,d.[IsActive] = s.[IsActive]
		  ,d.[CreatedById] = s.[CreatedById]
		  ,d.[UpdatedById] = s.[UpdatedById]
		  ,d.[CreatedDate] = s.[CreatedDate]
		  ,d.[UpdatedDate] = s.[UpdatedDate]
	WHEN NOT MATCHED THEN 
	INSERT 
	VALUES (s.[Code], s.[Name], s.[Description], s.[DisplayOrder], s.[IsActive], s.[CreatedById], s.[UpdatedById], s.[CreatedDate], s.[UpdatedDate]);
END

BEGIN 
	print '[dbo].[OfficeBranch]'
	
	DECLARE @OfficeBranchTable TABLE(
		[OfficeBranchId] [int] IDENTITY(1,1) NOT NULL,
		[Code] [nchar](6) NOT NULL,
		[Name] [varchar](100) NOT NULL,
		[Description] [varchar](250) NOT NULL,
		[DisplayOrder] [int] NOT NULL,
		[IsActive] [bit] NOT NULL,
		[CreatedById] [int] NOT NULL,
		[UpdatedById] [int] NULL,
		[CreatedDate] [datetime] NOT NULL,
		[UpdatedDate] [datetime] NULL
	)

	INSERT INTO @OfficeBranchTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('OFBR00', 'Unknown', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @OfficeBranchTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('OFBR01', 'Gurgaon', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @OfficeBranchTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('OFBR02', 'Mohali', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	MERGE [dbo].[OfficeBranch] AS d  -- destination
	USING (SELECT [Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate] FROM @OfficeBranchTable) AS s -- source
	ON (d.[Code] = s.[Code])
	WHEN MATCHED THEN
	UPDATE SET d.[Code] = s.[Code]
		  ,d.[Name] = s.[Name]
		  ,d.[Description] = s.[Description]
		  ,d.[DisplayOrder] = s.[DisplayOrder]
		  ,d.[IsActive] = s.[IsActive]
		  ,d.[CreatedById] = s.[CreatedById]
		  ,d.[UpdatedById] = s.[UpdatedById]
		  ,d.[CreatedDate] = s.[CreatedDate]
		  ,d.[UpdatedDate] = s.[UpdatedDate]
	WHEN NOT MATCHED THEN 
	INSERT 
	VALUES (s.[Code], s.[Name], s.[Description], s.[DisplayOrder], s.[IsActive], s.[CreatedById], s.[UpdatedById], s.[CreatedDate], s.[UpdatedDate]);
END

BEGIN 
	print '[dbo].[Salutation]'

	DECLARE @SalutationTable TABLE(
		[SalutationId] [int] IDENTITY(1,1) NOT NULL,
		[Code] [nchar](6) NOT NULL,
		[Name] [varchar](100) NOT NULL,
		[Description] [varchar](250) NOT NULL,
		[DisplayOrder] [int] NOT NULL,
		[IsActive] [bit] NOT NULL,
		[CreatedById] [int] NOT NULL,
		[UpdatedById] [int] NULL,
		[CreatedDate] [datetime] NOT NULL,
		[UpdatedDate] [datetime] NULL
	)
	
	INSERT INTO @SalutationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('SALN00', 'Unknown', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @SalutationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('SALN01', 'Dr.', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @SalutationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('SALN02', 'Mr.', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @SalutationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('SALN03', 'Mr. & Mrs.', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @SalutationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('SALN04', 'Mrs.', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @SalutationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('SALN05', 'Ms.', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @SalutationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('SALN06', 'Rev.', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @SalutationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('SALN07', 'Rev. & Mrs.', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)

	INSERT INTO @SalutationTable
	([Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], [UpdatedById], [CreatedDate], [UpdatedDate])
	VALUES ('SALN08', 'Sen.', '', 0, 1, @UserID, NULL, @CurrentDate, NULL)
	
	MERGE dbo.Salutation AS d  -- destination
	USING (SELECT [Code], [Name], [Description], [DisplayOrder], [IsActive], [CreatedById], 
		[UpdatedById], [CreatedDate], [UpdatedDate] FROM @SalutationTable) AS s -- source
	ON (d.[Code] = s.[Code])
	WHEN MATCHED THEN
	UPDATE SET d.[Code] = s.[Code]
		  ,d.[Name] = s.[Name]
		  ,d.[Description] = s.[Description]
		  ,d.[DisplayOrder] = s.[DisplayOrder]
		  ,d.[IsActive] = s.[IsActive]
		  ,d.[CreatedById] = s.[CreatedById]
		  ,d.[UpdatedById] = s.[UpdatedById]
		  ,d.[CreatedDate] = s.[CreatedDate]
		  ,d.[UpdatedDate] = s.[UpdatedDate]
	WHEN NOT MATCHED THEN 
	INSERT 	VALUES (s.[Code], s.[Name], s.[Description], s.[DisplayOrder], 
		s.[IsActive], s.[CreatedById], s.[UpdatedById], s.[CreatedDate], s.[UpdatedDate]);
END