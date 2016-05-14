
CREATE TABLE [dbo].[Salutation](
	[SalutationId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nchar](6) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Salutation] PRIMARY KEY CLUSTERED 
(
	[SalutationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

 
ALTER TABLE [dbo].[Salutation] ADD  CONSTRAINT [DF_Salutation_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Salutation] ADD  CONSTRAINT [DF_Salutation_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


