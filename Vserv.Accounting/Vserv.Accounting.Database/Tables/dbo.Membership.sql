
CREATE TABLE [dbo].[Membership](
	[UserId] [int] NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[LoweredEmail] [nvarchar](256) NOT NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsApproved] [nchar](10) NULL,
	[IsConfirmed] [bit] NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[LastPasswordChangedDate] [datetime] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NULL,
	[Comment] [ntext] NOT NULL,
 CONSTRAINT [PK__Membersh__1788CC4C3CC6BE96] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Membership] ADD  CONSTRAINT [DF__Membershi__IsCon__5C6CB6D7]  DEFAULT ((0)) FOR [IsConfirmed]
GO

ALTER TABLE [dbo].[Membership] ADD  CONSTRAINT [DF__Membershi__Passw__5D60DB10]  DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess]
GO

ALTER TABLE [dbo].[Membership] ADD  CONSTRAINT [DF_Membership_FailedPasswordAttemptCount]  DEFAULT ((0)) FOR [FailedPasswordAttemptCount]
GO

ALTER TABLE [dbo].[Membership] ADD  CONSTRAINT [DF_Membership_FailedPasswordAnswerAttemptCount]  DEFAULT ((0)) FOR [FailedPasswordAnswerAttemptCount]
GO

ALTER TABLE [dbo].[Membership] ADD  CONSTRAINT [DF_Membership_Comment]  DEFAULT ('') FOR [Comment]
GO


