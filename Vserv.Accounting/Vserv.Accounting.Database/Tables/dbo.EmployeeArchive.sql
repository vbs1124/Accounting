
CREATE TABLE [dbo].[EmployeeArchive](
	[EmployeeArchiveId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
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
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeArchive] PRIMARY KEY CLUSTERED 
(
	[EmployeeArchiveId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EmployeeArchive] ADD  CONSTRAINT [DF_EmployeeArchive_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[EmployeeArchive]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeArchive_Bank] FOREIGN KEY([BankId])
REFERENCES [dbo].[Bank] ([BankId])
GO

ALTER TABLE [dbo].[EmployeeArchive] CHECK CONSTRAINT [FK_EmployeeArchive_Bank]
GO

ALTER TABLE [dbo].[EmployeeArchive]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeArchive_Designation] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designation] ([DesignationId])
GO

ALTER TABLE [dbo].[EmployeeArchive] CHECK CONSTRAINT [FK_EmployeeArchive_Designation]
GO

ALTER TABLE [dbo].[EmployeeArchive]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeArchive_Gender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([GenderId])
GO

ALTER TABLE [dbo].[EmployeeArchive] CHECK CONSTRAINT [FK_EmployeeArchive_Gender]
GO

ALTER TABLE [dbo].[EmployeeArchive]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeArchive_MailingState] FOREIGN KEY([MailingStateId])
REFERENCES [dbo].[State] ([StateId])
GO

ALTER TABLE [dbo].[EmployeeArchive] CHECK CONSTRAINT [FK_EmployeeArchive_MailingState]
GO

ALTER TABLE [dbo].[EmployeeArchive]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeArchive_OfficeBranch] FOREIGN KEY([OfficeBranchId])
REFERENCES [dbo].[OfficeBranch] ([OfficeBranchId])
GO

ALTER TABLE [dbo].[EmployeeArchive] CHECK CONSTRAINT [FK_EmployeeArchive_OfficeBranch]
GO

ALTER TABLE [dbo].[EmployeeArchive]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeArchive_PermanentState] FOREIGN KEY([PermanentStateId])
REFERENCES [dbo].[State] ([StateId])
GO

ALTER TABLE [dbo].[EmployeeArchive] CHECK CONSTRAINT [FK_EmployeeArchive_PermanentState]
GO

ALTER TABLE [dbo].[EmployeeArchive]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeArchive_Salutation] FOREIGN KEY([SalutationId])
REFERENCES [dbo].[Salutation] ([SalutationId])
GO

ALTER TABLE [dbo].[EmployeeArchive] CHECK CONSTRAINT [FK_EmployeeArchive_Salutation]
GO


