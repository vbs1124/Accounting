CREATE TABLE [dbo].[ZipCode](
	[ZipCodeId] [int] IDENTITY(1,1) NOT NULL,
	[PinCode] [varchar](50) NOT NULL,
	[DivisionName] [varchar](255) NOT NULL,
	[Taluk] [varchar](255) NOT NULL,
	[CityId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_ZipCode_IsActive]  DEFAULT ((1)),
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_ZipCode_CreatedDate]  DEFAULT (getdate()),
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ZipCode] PRIMARY KEY CLUSTERED 
(
	[ZipCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO