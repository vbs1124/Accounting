CREATE TABLE [dbo].[EmpSalaryStructure](
	[EmpSalaryStructureId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[ParentId] [int] NULL,
	[CTC] [numeric](19, 0) NOT NULL,
	[MonthlyCabDeductions] [numeric](19, 0) NULL,
	[MonthlyProjectIncentive] [numeric](19, 0) NULL,
	[MonthlyCarLease] [numeric](19, 0) NULL,
	[MonthlyFoodCoupons] [numeric](19, 0) NULL,
	[EffectiveFrom] [datetime] NOT NULL,
	[EffectiveTo] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_EmpSalaryStructure] PRIMARY KEY CLUSTERED 
(
	[EmpSalaryStructureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EmpSalaryStructure] ADD  CONSTRAINT [DF_EmpSalaryStructure_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[EmpSalaryStructure] ADD  CONSTRAINT [DF_EmpSalaryStructure_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
