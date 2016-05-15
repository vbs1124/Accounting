CREATE TABLE [dbo].[Address](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[Address1] [nvarchar](255) NOT NULL,
	[Address2] [nvarchar](255) NOT NULL,
	[CountryId] [int] NULL,
	[StateId] [int] NULL,
	[CityId] [int] NULL,
	[ZipCodeId] [int] NULL,
	[City] [varchar](250) NULL,
	[ZipCode] [varchar](10) NOT NULL,
	[Latitude] [decimal](16, 13) NULL,
	[Longitude] [decimal](16, 13) NULL,
	[IsCommunicationAddress] [bit] NOT NULL,
	[AddressTypeId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_Address2]  DEFAULT ('') FOR [Address2]
GO

ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_StateId]  DEFAULT ((1)) FOR [StateId]
GO

ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_CityId]  DEFAULT ((1)) FOR [CityId]
GO

ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_IsCommunicationAddress]  DEFAULT ((0)) FOR [IsCommunicationAddress]
GO

ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_AddressType] FOREIGN KEY([AddressTypeId])
REFERENCES [dbo].[AddressType] ([AddressTypeId])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_AddressType]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([CityId])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_City]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_State]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_ZipCode] FOREIGN KEY([ZipCodeId])
REFERENCES [dbo].[ZipCode] ([ZipCodeId])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_ZipCode]
GO