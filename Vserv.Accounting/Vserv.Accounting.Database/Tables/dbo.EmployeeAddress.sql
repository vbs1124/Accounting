CREATE TABLE [dbo].[EmployeeAddress](
	[EmployeeAddressId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[AddressId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeAddress] PRIMARY KEY CLUSTERED 
(
	[EmployeeAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

