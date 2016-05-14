CREATE TABLE [dbo].[Address](
	[AddressId] [int] NOT NULL,
	[Address1] [nvarchar](255) NOT NULL,
	[Address2] [nvarchar](255) NOT NULL,
	[City] [nvarchar](255) NOT NULL,
	[ZipCode] [varchar](50) NOT NULL,
	[Latitude] [decimal](16, 13) NULL,
	[Longitude] [decimal](16, 13) NULL,
	[IsCommunicationAddress] [bit] NOT NULL,
	[AddressTypeId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NULL,
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