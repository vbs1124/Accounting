
CREATE TABLE [dbo].[UserSecurityQuestion](
	[UserSecurityQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SecurityQuestionId] [int] NOT NULL,
	[Answer] [varchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
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

ALTER TABLE [dbo].[UserSecurityQuestion] ADD  CONSTRAINT [DF_UserSecurityQuestion_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[UserSecurityQuestion]  WITH CHECK ADD  CONSTRAINT [FK_UserSecurityQuestion_SecurityQuestion] FOREIGN KEY([SecurityQuestionId])
REFERENCES [dbo].[SecurityQuestion] ([SecurityQuestionId])
GO

ALTER TABLE [dbo].[UserSecurityQuestion] CHECK CONSTRAINT [FK_UserSecurityQuestion_SecurityQuestion]
GO

ALTER TABLE [dbo].[UserSecurityQuestion]  WITH CHECK ADD  CONSTRAINT [FK_UserSecurityQuestion_UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[UserSecurityQuestion] CHECK CONSTRAINT [FK_UserSecurityQuestion_UserProfile]
GO