
CREATE TABLE [dbo].[EPFOffice](
	[EPFOfficeId] [int] IDENTITY(1,1) NOT NULL,
	[EPFOfficeName] [varchar](50) NOT NULL,
	[EPFOfficeCode] [varchar](10) NOT NULL,
	[StateId] [varchar](10) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
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


