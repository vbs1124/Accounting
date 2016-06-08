USE [VservAccountingDB]
GO

/****** Object:  StoredProcedure [dbo].[GetEmployeeSalaryDetail]    Script Date: 08-06-2016 12:18:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Arun, Kumar
-- Description:	This SPROC will pick all tha salary breakup for 1 complete financial year.
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployeeSalaryDetail]
	@EmployeeId INT,
	@FinancialYearFrom INT,
	@FinancialYearTo INT
AS
BEGIN
	SET NOCOUNT ON;
	SET FMTONLY OFF;

	DECLARE @tmpTable TABLE (
		EmployeeId		INT,
		ComponentName	VARCHAR(250), 
		Amount			NUMERIC(19, 2), 
		[Month]			VARCHAR(250),
		[Year]			INT,  
		DisplayOrder	INT)

	INSERT INTO @tmpTable
	SELECT esd.EmployeeId, 
		sc.[Description],
		esd.Amount,
		lm.Name,
		2016,
		sc.DisplayOrder
	FROM EmployeeSalaryDetail esd
	LEFT JOIN SalaryComponent sc on sc.SalaryComponentId = esd.SalaryComponentId
	LEFT JOIN LookupMonth lm on lm.MonthId= esd.MonthId
	WHERE EmployeeId = @EmployeeId AND 
		(([Year] = @FinancialYearFrom AND esd.MonthId < 10) OR ([Year] = @FinancialYearTo AND esd.MonthId > 9))
	ORDER BY sc.DisplayOrder;

	WITH YearlySalaryBreakup
	AS
	(	SELECT *
		FROM @tmpTable
		PIVOT(SUM(Amount) FOR [Month]
		IN([April]
			, [May]
			, [June]
			, [July]
			, [August]
			, [September]
			, [October]
			, [November]
			, [December]
			, [January]
			, [February]
			, [March])) AS YearlySalaryBreakupPivot)
	SELECT EmployeeId
		, ComponentName
		, [Year]
		, DisplayOrder
		, ISNULL(April, 0)		AS April
		, ISNULL(May, 0)		AS May
		, ISNULL(June, 0)		AS June
		, ISNULL(July, 0)		AS July
		, ISNULL(August, 0)		AS August
		, ISNULL(September, 0)	AS September
		, ISNULL(October, 0)	AS October
		, ISNULL(November, 0)	AS November
		, ISNULL(December, 0)	AS December
		, ISNULL(January, 0)	AS January
		, ISNULL(February, 0)	AS February
		, ISNULL(March, 0)		AS March
	FROM YearlySalaryBreakup
	ORDER BY YearlySalaryBreakup.DisplayOrder
END

GO

