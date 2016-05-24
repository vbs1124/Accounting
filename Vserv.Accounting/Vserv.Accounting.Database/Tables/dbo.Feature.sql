
CREATE TABLE [dbo].[Feature](
	[FeatureId] [int] IDENTITY(1,1) NOT NULL,
	[NameOption] [varchar](100) NOT NULL,
	[Controller] [varchar](100) NOT NULL,
	[Action] [varchar](100) NOT NULL,
	[Area] [varchar](100) NOT NULL,
	[ImageClass] [varchar](100) NOT NULL,
	[Activeli] [varchar](100) NOT NULL,
	[Status] [bit] NOT NULL,
	[ParentId] [int] NOT NULL,
	[IsParent] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Feature] PRIMARY KEY CLUSTERED 
(
	[FeatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Feature] ADD  CONSTRAINT [DF_Feature_NameOption]  DEFAULT ('') FOR [NameOption]
GO

ALTER TABLE [dbo].[Feature] ADD  CONSTRAINT [DF_Feature_Controller]  DEFAULT ('') FOR [Controller]
GO

ALTER TABLE [dbo].[Feature] ADD  CONSTRAINT [DF_Feature_Action]  DEFAULT ('') FOR [Action]
GO

ALTER TABLE [dbo].[Feature] ADD  CONSTRAINT [DF_Feature_Area]  DEFAULT ('') FOR [Area]
GO

ALTER TABLE [dbo].[Feature] ADD  CONSTRAINT [DF_Feature_ImageClass]  DEFAULT ('') FOR [ImageClass]
GO

ALTER TABLE [dbo].[Feature] ADD  CONSTRAINT [DF_Feature_Activeli]  DEFAULT ('') FOR [Activeli]
GO

ALTER TABLE [dbo].[Feature] ADD  CONSTRAINT [DF_Feature_ParentId]  DEFAULT ((0)) FOR [ParentId]
GO

ALTER TABLE [dbo].[Feature] ADD  CONSTRAINT [DF_Feature_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Feature] ADD  CONSTRAINT [DF_Feature_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Feature]  WITH CHECK ADD  CONSTRAINT [FK_Feature_Feature] FOREIGN KEY([FeatureId])
REFERENCES [dbo].[Feature] ([FeatureId])
GO

ALTER TABLE [dbo].[Feature] CHECK CONSTRAINT [FK_Feature_Feature]
GO