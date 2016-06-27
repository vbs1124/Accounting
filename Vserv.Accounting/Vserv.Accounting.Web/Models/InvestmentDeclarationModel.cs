using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vserv.Accounting.Web.Code;

namespace Vserv.Accounting.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class InvestmentDeclarationModel
    {
        public string FinalYearFrom { get; set; }
        public string FinalYearTo { get; set; }

        [Display(Name = "April")]
        public Decimal? April { get; set; }

        [Display(Name = "May")]
        public Decimal? May { get; set; }

        [Display(Name = "June")]
        public Decimal? June { get; set; }

        [Display(Name = "July")]
        public Decimal? July { get; set; }

        [Display(Name = "August")]
        public Decimal? August { get; set; }

        [Display(Name = "September")]
        public Decimal? September { get; set; }

        [Display(Name = "October")]
        public Decimal? October { get; set; }

        [Display(Name = "November")]
        public Decimal? November { get; set; }

        [Display(Name = "December")]
        public Decimal? December { get; set; }

        [Display(Name = "January")]
        public Decimal? January { get; set; }

        [Display(Name = "February")]
        public Decimal? February { get; set; }

        [Display(Name = "March")]
        public Decimal? March { get; set; }

        [Display(Name = "Interest on Housing Loan u/s 24")]
        public Decimal? HousingLoan24_1 { get; set; }

        [Display(Name = "Interest on Housing Loan u/s 24")]
        public Decimal? HousingLoan24_2 { get; set; }

        [Display(Name = "Medical Allowance")]
        public Decimal? MedicalAllowance { get; set; }

        [Display(Name = "Sec 80D (Medi Claim Policy) Cash payment is not allowed as deduction U/S 80D")]
        public Decimal? Sec80D_1 { get; set; }

        [Display(Name = "Sec 80D (Medi Claim Policy) Cash payment is not allowed as deduction U/S 80D")]
        public Decimal? Sec80D_2 { get; set; }

        [Display(Name = "Sec 80D (Preventive health check up). Cash payment is not allowed as deduction U/S 80D")]
        public Decimal? Sec80D_3 { get; set; }

        [Display(Name = "Sec 80DD (Maintenance including Medical treatment of dependant person with disability)")]
        public Decimal? Sec80DD { get; set; }

        [Display(Name = "Sec 80DDB (Medical treatment for specified diseases or ailments) - Self / Dependents")]
        public Decimal? Sec80DDB { get; set; }

        [Display(Name = "Sec 80E (Education Loan)")]
        public Decimal? Sec80E { get; set; }

        [Display(Name = "Sec 80U (For person with a disability)")]
        public Decimal? Sec80U { get; set; }

        [Display(Name = "Life Insurance Premium")]
        public Decimal? LifeInsurancePremium { get; set; }

        [Display(Name = "Public Provident Fund (PPF)")]
        public Decimal? PublicProvidentFund { get; set; }

        [Display(Name = "Sukanya Samriddhi Account ")]
        public Decimal? SukanyaSamriddhiAccount { get; set; }

        [Display(Name = "Pension Fund Contribution ")]
        public Decimal? PensionFundContribution { get; set; }

        [Display(Name = "National Savings Certificate(NSC)")]
        public Decimal? NationalSavingsCertificate { get; set; }

        [Display(Name = "Interest Accrued on NSC (Reinvested)")]
        public Decimal? InterestAccruedNSC { get; set; }

        [Display(Name = "UnitLinkedInsurancePolicy")]
        public Decimal? UnitLinkedInsurancePolicy { get; set; }

        [Display(Name = "Equity Linked Savings Schemes (ELSS) - Mutual Funds")]
        public Decimal? EquityLinkedSavingsSchemes { get; set; }

        [Display(Name = "Payment of Tuition fees for Children  (Max 2 Children)")]
        public Decimal? PaymentTuitionFeesforChildren { get; set; }

        [Display(Name = "Principal repayment of Housing Loan ")]
        public Decimal? PrincipalRepaymentofHousingLoan { get; set; }

        [Display(Name = "Registration charges incurred for Buying House (I year Only)")]
        public Decimal? RegistrationChargesIncurredforBuyingHouse { get; set; }

        [Display(Name = "Infrastructure Bonds ")]
        public Decimal? InfrastructureBonds { get; set; }

        [Display(Name = "Bank Fixed Deposit for 5 Years & above")]
        public Decimal? BankFixedDeposit5YearsAbove { get; set; }

        [Display(Name = "Post office Term Deposit for  5years & above ")]
        public Decimal? PostOfficeTermDepositfor5yearsAbove { get; set; }

        [Display(Name = "New Pension Scheme (NPS) (u/s 80CCC)")]
        public Decimal? NewPensionScheme { get; set; }

        [Display(Name = "Additional exemption National Pension Scheme (Section 80CCD)")]
        public Decimal? Section80CCD { get; set; }

        [Display(Name = "Food Coupon")]
        public Decimal? FoodCoupon { get; set; }

    }
}