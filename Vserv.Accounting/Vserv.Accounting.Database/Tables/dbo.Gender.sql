
CREATE TABLE [dbo].[Gender](
	[GenderId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nchar](6) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[GenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Gender] ADD  CONSTRAINT [DF_Gender_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Gender] ADD  CONSTRAINT [DF_Gender_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


