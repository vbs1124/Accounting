CREATE TABLE [dbo].[SecurityQuestion](
	[SecurityQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[CollectionId] [int] NULL,
	[Question] [varchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_SecurityQuestion_IsActive]  DEFAULT ((1)),
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_SecurityQuestion] PRIMARY KEY CLUSTERED 
(
	[SecurityQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

