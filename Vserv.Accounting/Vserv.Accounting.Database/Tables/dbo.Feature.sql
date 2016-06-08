CREATE TABLE [dbo].[Feature](
	[FeatureId] [int] IDENTITY(1,1) NOT NULL,
	[NameOption] [varchar](100) NOT NULL CONSTRAINT [DF_Feature_NameOption]  DEFAULT (''),
	[Controller] [varchar](100) NOT NULL CONSTRAINT [DF_Feature_Controller]  DEFAULT (''),
	[Action] [varchar](100) NOT NULL CONSTRAINT [DF_Feature_Action]  DEFAULT (''),
	[Area] [varchar](100) NOT NULL CONSTRAINT [DF_Feature_Area]  DEFAULT (''),
	[ImageClass] [varchar](100) NOT NULL CONSTRAINT [DF_Feature_ImageClass]  DEFAULT (''),
	[Activeli] [varchar](100) NOT NULL CONSTRAINT [DF_Feature_Activeli]  DEFAULT (''),
	[Status] [bit] NOT NULL,
	[ParentId] [int] NOT NULL CONSTRAINT [DF_Feature_ParentId]  DEFAULT ((0)),
	[IsParent] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_Feature_IsActive]  DEFAULT ((1)),
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_Feature_CreatedDate]  DEFAULT (getdate()),
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Feature] PRIMARY KEY CLUSTERED 
(
	[FeatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO