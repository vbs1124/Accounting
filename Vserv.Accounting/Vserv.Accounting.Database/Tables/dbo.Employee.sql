CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](250) NOT NULL,
	[MiddleName] [varchar](250) NOT NULL,
	[LastName] [varchar](250) NOT NULL,
	[FatherName] [varchar](250) NOT NULL,
	[MotherName] [varchar](250) NOT NULL,
	[UniversalAccountNumber] [varchar](20) NOT NULL,
	[PermanentAccountNumber] [varchar](20) NOT NULL,
	[AADHAARNumber] [varchar](20) NULL,
	[MobileNumber] [varchar](20) NOT NULL,
	[EmailAddress] [varchar](250) NOT NULL,
	[BirthDay] [datetime] NOT NULL,
	[JoiningDate] [datetime] NOT NULL,
	[RelievingDate] [datetime] NULL,
	[VBS_Id] [nchar](10) NOT NULL,
	[DesignationId] [int] NOT NULL,
	[SalutationId] [int] NULL,
	[GenderId] [int] NOT NULL,
	[OfficeBranchId] [int] NOT NULL,
	[DepartmentId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_MiddleName]  DEFAULT ('') FOR [MiddleName]
GO

ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_FatherName]  DEFAULT ('') FOR [FatherName]
GO

ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_ModherName]  DEFAULT ('') FOR [MotherName]
GO

ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_IsActive_1]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Designation] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designation] ([DesignationId])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Designation]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Employee]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Gender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([GenderId])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Gender]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_OfficeBranch] FOREIGN KEY([OfficeBranchId])
REFERENCES [dbo].[OfficeBranch] ([OfficeBranchId])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_OfficeBranch]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Salutation] FOREIGN KEY([SalutationId])
REFERENCES [dbo].[Salutation] ([SalutationId])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Salutation]
GO


