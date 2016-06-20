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
		EmpSalaryStructureId INT,
		SCName			VARCHAR(250), 
		SCCode			VARCHAR(6), 
		SCDescription	VARCHAR(250), 
		Amount			VARCHAR(MAX), 
		[Month]			VARCHAR(250),
		DisplayOrder	INT)

	INSERT INTO @tmpTable
	SELECT esd.EmployeeId, 
		EmployeeSalaryDetail.EmpSalaryStructureId,
		sc.[Name],
		sc.[Code],
		sc.[Description],
		--esd.Amount,
		CONVERT(VARCHAR, esd.EmployeeSalaryDetailId)+'#'	+Convert(VARCHAR, esd.Amount) AS Amount,
		lm.Name,
		sc.DisplayOrder
	FROM EmployeeSalaryDetail esd
	LEFT JOIN SalaryComponent sc on sc.SalaryComponentId = esd.SalaryComponentId
	LEFT JOIN LookupMonth lm on lm.MonthId= esd.MonthId
	WHERE esd.EmployeeId = @EmployeeId AND esd.IsActive = 1 AND
		(([Year] = @FinancialYearFrom AND esd.MonthId IN(4,5,6,7,8,9,10,11,12)) OR ([Year] = @FinancialYearTo AND esd.MonthId IN(1,2,3)))
	ORDER BY sc.DisplayOrder;
	--select * from @tmpTable;
	WITH YearlySalaryBreakup
	AS
	(	SELECT *
		FROM @tmpTable
		PIVOT(MAX(Amount) FOR [Month]
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
		, EmpSalaryStructureId
		, SCName
		, SCCode
		, SCDescription
		, DisplayOrder
		, April
		, May
		, June
		, July
		, August
		, September
		, October
		, November
		, December
		, January
		, February
		, March
	FROM YearlySalaryBreakup ysb
	ORDER BY ysb.DisplayOrder
END
GO

