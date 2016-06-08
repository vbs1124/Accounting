CREATE TABLE [dbo].[UserSecurityQuestion](
	[UserSecurityQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SecurityQuestionId] [int] NOT NULL,
	[Answer] [varchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_UserSecurityQuestion_IsActive]  DEFAULT ((1)),
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserSecurityQuestion] PRIMARY KEY CLUSTERED 
(
	[UserSecurityQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO