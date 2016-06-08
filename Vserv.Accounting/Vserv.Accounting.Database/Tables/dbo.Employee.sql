CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[FatherName] [varchar](100) NULL,
	[MotherName] [varchar](100) NULL,
	[PermanentAccountNumber] [varchar](20) NULL,
	[UniversalAccountNumber] [varchar](20) NULL,
	[EPFNumber] [varchar](20) NULL,
	[AADHAARNumber] [varchar](20) NULL,
	[ESINumber] [varchar](20) NULL,
	[MobileNumber] [varchar](20) NULL,
	[OfficialEmailAddress] [varchar](250) NULL,
	[PersonalEmailAddress] [varchar](250) NULL,
	[BirthDay] [datetime] NOT NULL,
	[JoiningDate] [datetime] NOT NULL,
	[RelievingDate] [datetime] NULL,
	[ResignationDate] [datetime] NULL,
	[VBS_Id] [char](7) NULL,
	[DesignationId] [int] NOT NULL,
	[SalutationId] [int] NOT NULL,
	[GenderId] [int] NOT NULL,
	[OfficeBranchId] [int] NOT NULL,
	[PermanentAddress1] [varchar](250) NULL,
	[PermanentAddress2] [varchar](250) NULL,
	[PermanentCity] [varchar](50) NULL,
	[PermanentZipCode] [varchar](6) NULL,
	[PermanentStateId] [int] NULL,
	[PermanentCountryId] [int] NULL,
	[MailingAddress1] [varchar](250) NULL,
	[MailingAddress2] [varchar](250) NULL,
	[MailingCity] [varchar](50) NOT NULL,
	[MailingZipCode] [varchar](6) NULL,
	[MailingStateId] [int] NULL,
	[MailingCountryId] [int] NULL,
	[IsMetro] [bit] NOT NULL,
	[BankAccountNumber] [varchar](100) NULL,
	[BankId] [int] NULL,
	[BankIFSCCode] [varchar](20) NULL,
	[BankMICRCode] [varchar](20) NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_Employee_IsActive]  DEFAULT ((1)),
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_EmployeeEmailAddress] UNIQUE NONCLUSTERED 
(
	[OfficialEmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_EmployeeMobileNumber] UNIQUE NONCLUSTERED 
(
	[MobileNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_EmployeeVBS_Id] UNIQUE NONCLUSTERED 
(
	[VBS_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO