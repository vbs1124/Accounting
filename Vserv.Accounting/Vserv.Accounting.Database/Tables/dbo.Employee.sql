CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](250) NOT NULL,
	[MiddleName] [varchar](250) NULL,
	[LastName] [varchar](250) NULL,
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
	[VBS_Id] [varchar](50) NOT NULL,
	[DesignationId] [int] NOT NULL,
	[SalutationId] [int] NOT NULL,
	[GenderId] [int] NOT NULL,
	[OfficeBranchId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_EmployeeEmailAddress] UNIQUE NONCLUSTERED 
(
	[EmailAddress] ASC
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

ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Designation] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designation] ([DesignationId])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Designation]
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