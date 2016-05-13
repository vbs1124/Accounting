
CREATE TABLE [dbo].[EPFOffice](
	[EPFOfficeId] [int] IDENTITY(1,1) NOT NULL,
	[EPFOfficeName] [nchar](10) NULL,
	[EPFOfficeCode] [nchar](10) NULL,
	[StateId] [nchar](10) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_EPFOffice] PRIMARY KEY CLUSTERED 
(
	[EPFOfficeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EPFOffice] ADD  CONSTRAINT [DF_EPFOffice_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[EPFOffice] ADD  CONSTRAINT [DF_EPFOffice_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


