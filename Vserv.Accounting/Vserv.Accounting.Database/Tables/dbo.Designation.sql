CREATE TABLE [dbo].[Designation](
	[DesignationId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nchar](6) NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](250) NULL,
	[DisplayOrder] [int] NOT NULL CONSTRAINT [DF_Designation_DisplayOrder]  DEFAULT ((0)),
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_Designation_IsActive]  DEFAULT ((1)),
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_Designation_CreatedDate]  DEFAULT (getdate()),
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Designation] PRIMARY KEY CLUSTERED 
(
	[DesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
