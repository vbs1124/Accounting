CREATE TABLE [dbo].[EmployeeSalaryDetail](
	[EmployeeSalaryDetailId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[SalaryComponentId] [int] NOT NULL,
	[MonthId] [int] NOT NULL,
	[Year] [int] NULL,
	[Amount] [numeric](19, 4) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeSalaryDetail] PRIMARY KEY CLUSTERED 
(
	[EmployeeSalaryDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO