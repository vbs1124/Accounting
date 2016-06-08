CREATE TABLE [dbo].[PaySheet](
	[PaySheetId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[CTCPerMonth] [numeric](19, 4) NOT NULL,
	[Basic] [numeric](19, 4) NOT NULL,
	[HRA] [numeric](19, 4) NOT NULL,
	[Conveyance] [numeric](19, 4) NULL,
	[SpecialAllowance] [numeric](19, 4) NOT NULL,
	[PerformanceIncentive] [numeric](19, 4) NOT NULL,
	[LeaveEncashment] [numeric](19, 4) NULL,
	[SalaryArrears] [numeric](19, 4) NULL,
	[CabDeductions] [numeric](19, 4) NULL,
	[OtherDeduction] [numeric](19, 4) NULL,
	[Commission] [numeric](19, 4) NULL,
	[Others] [numeric](19, 4) NULL,
	[Medical] [numeric](19, 4) NULL,
	[FoodCoupons] [numeric](19, 4) NULL,
	[ProjectIncentive] [numeric](19, 4) NULL,
	[CarLease] [numeric](19, 4) NULL,
	[LTC] [numeric](19, 4) NULL,
	[PF] [numeric](19, 4) NOT NULL,
	[Mediclaim] [numeric](19, 4) NOT NULL,
	[Gratuity] [numeric](19, 4) NOT NULL,
	[Month] [int] NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_PaySheet_IsActive]  DEFAULT ((1)),
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_PaySheet] PRIMARY KEY CLUSTERED 
(
	[PaySheetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
