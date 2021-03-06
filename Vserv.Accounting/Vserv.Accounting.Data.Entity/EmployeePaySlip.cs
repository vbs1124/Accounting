//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vserv.Accounting.Data.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeePaySlip
    {
        public int EmployeePaySlipId { get; set; }
        public int EmployeeId { get; set; }
        public string VBS_Id { get; set; }
        public string PersonID { get; set; }
        public string EmployeeFullName { get; set; }
        public string PaySlipMonth { get; set; }
        public int PaySlipYear { get; set; }
        public System.DateTime GeneratedOn { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string PayPeriod { get; set; }
        public string Designation { get; set; }
        public System.DateTime JoiningDate { get; set; }
        public string Gender { get; set; }
        public string PermanentAccountNumber { get; set; }
        public string UniversalAccountNumber { get; set; }
        public string EPFNumber { get; set; }
        public string AADHAARNumber { get; set; }
        public string ESINumber { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Band { get; set; }
        public string DaysWorkedInMonth { get; set; }
        public string LWPCurrentOrPreviousMonth { get; set; }
        public string SabbaticalLeaveCurrentOrPreviousMon { get; set; }
        public string StandardBasicSalary { get; set; }
        public string StandardHRA { get; set; }
        public string StandardConveyanceAllowance { get; set; }
        public string StandardCityCompensatoryAllowan { get; set; }
        public string StandardHolidayAllowance { get; set; }
        public string StandardMedicalAllowance { get; set; }
        public string StandardDepAllowOrPerfomInc { get; set; }
        public string EarningsBasicSalary { get; set; }
        public string EarningsHRA { get; set; }
        public string EarningsConveyanceAllowance { get; set; }
        public string EarningsCityCompensatoryAllowan { get; set; }
        public string EarningsHolidayAllowance { get; set; }
        public string EarningsMedicalAllowance { get; set; }
        public string EarningsDepAllowOrPerfomInc { get; set; }
        public string PowerOf1Deduction { get; set; }
        public string EePFContribution { get; set; }
        public string ProfessionalTax { get; set; }
        public string IncomeTax { get; set; }
        public string TotalStandardSalary { get; set; }
        public string GrossEarnings { get; set; }
        public string GrossDeductions { get; set; }
        public string NetPay { get; set; }
        public string HRAAnnualExemption { get; set; }
        public string ConveyanceAnnualExempt { get; set; }
        public string TotalExemption { get; set; }
        public string NMetroHRARcvdOrProj { get; set; }
        public string NMetroOfBasic { get; set; }
        public string NMetroRentPaid { get; set; }
        public string LeastOfMetroOrNMetroIsExempt { get; set; }
        public string MonthsRemainingTillMarch { get; set; }
        public string TaxableIncometillPrMonth { get; set; }
        public string CurrentMthTaxableIncome { get; set; }
        public string ProjectedStandardSalary { get; set; }
        public string TaxableAnnPerks { get; set; }
        public string AnnualMedicalExemption { get; set; }
        public string GrossSalary { get; set; }
        public string ExemptionUS10 { get; set; }
        public string TaxOnEmployment { get; set; }
        public string IncomeUnderHeadSalary { get; set; }
        public string InterestOnHouseProperty { get; set; }
        public string GrossTotalIncome { get; set; }
        public string AggOfChapterVI { get; set; }
        public string TotalIncome { get; set; }
        public string TaxOnTotalIncome { get; set; }
        public string TaxCredit { get; set; }
        public string EducationCess { get; set; }
        public string TaxPayable { get; set; }
        public string TaxDeductedSofar { get; set; }
        public string BalanceTax { get; set; }
        public string ProvidentFund { get; set; }
        public string VoluntaryPF { get; set; }
        public string PaymentTowardsLifeInsurancePolicy { get; set; }
        public string TotalContributionUnderChapterVIA { get; set; }
        public string TotalMonthlyTaxDeduction { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
