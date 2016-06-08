CREATE TABLE [dbo].[ApplicationLog](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[AppDomain] [varchar](256) NULL,
	[AspnetCache] [varchar](max) NULL,
	[AspnetContext] [varchar](max) NULL,
	[AspnetRequest] [varchar](max) NULL,
	[AspnetSession] [varchar](max) NULL,
	[Date] [datetime] NOT NULL,
	[Exception] [varchar](max) NULL,
	[Identity] [varchar](256) NULL,
	[Level] [varchar](30) NULL,
	[Line] [int] NULL,
	[Logger] [varchar](100) NULL,
	[Message] [varchar](2000) NULL,
	[Method] [varchar](500) NULL,
	[NDC] [varchar](500) NULL,
	[Property] [varchar](500) NULL,
	[StackTrace] [varchar](max) NULL,
	[StackTraceDetail] [varchar](max) NULL,
	[TimeStamp] [bigint] NULL,
	[Thread] [varchar](256) NULL,
	[Type] [varchar](256) NULL,
	[Username] [varchar](100) NULL,
	[UTCDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

