CREATE TABLE [dbo].[EPFNumber](
	[EPFNumberId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[EPFOfficeId] [int] NOT NULL,
	[EstablishmentCode] [varchar](50) NOT NULL,
	[Extension] [varchar](10) NOT NULL,
	[AccountNumber] [varchar](10) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_EPFNumber] PRIMARY KEY CLUSTERED 
(
	[EPFNumberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO