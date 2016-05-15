CREATE TABLE [dbo].[ZipCode](
	[ZipCodeId] [int] IDENTITY(1,1) NOT NULL,
	[PinCode] [varchar](50) NOT NULL,
	[DivisionName] [varchar](255) NOT NULL,
	[Taluk] [varchar](255) NOT NULL,
	[CityId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ZipCode] PRIMARY KEY CLUSTERED 
(
	[ZipCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ZipCode] ADD  CONSTRAINT [DF_ZipCode_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[ZipCode] ADD  CONSTRAINT [DF_ZipCode_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[ZipCode]  WITH CHECK ADD  CONSTRAINT [FK_ZipCode_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([CityId])
GO

ALTER TABLE [dbo].[ZipCode] CHECK CONSTRAINT [FK_ZipCode_City]
GO

ALTER TABLE [dbo].[ZipCode]  WITH CHECK ADD  CONSTRAINT [FK_ZipCode_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO

ALTER TABLE [dbo].[ZipCode] CHECK CONSTRAINT [FK_ZipCode_State]
GO