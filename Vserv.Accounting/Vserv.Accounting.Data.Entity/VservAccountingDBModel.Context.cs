﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class VservAccountingDBEntities : DbContext
    {
        public VservAccountingDBEntities()
            : base("name=VservAccountingDBEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<ApplicationLog> ApplicationLogs { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<EmpInvestment> EmpInvestments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeArchive> EmployeeArchives { get; set; }
        public virtual DbSet<EmployeePaySlip> EmployeePaySlips { get; set; }
        public virtual DbSet<EmpSalaryDetail> EmpSalaryDetails { get; set; }
        public virtual DbSet<EmpSalaryDetailArchive> EmpSalaryDetailArchives { get; set; }
        public virtual DbSet<EmpSalaryStructure> EmpSalaryStructures { get; set; }
        public virtual DbSet<EPFNumber> EPFNumbers { get; set; }
        public virtual DbSet<EPFOffice> EPFOffices { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<IncomeTaxRate> IncomeTaxRates { get; set; }
        public virtual DbSet<InvestmentCategory> InvestmentCategories { get; set; }
        public virtual DbSet<InvestmentSubCategory> InvestmentSubCategories { get; set; }
        public virtual DbSet<InvtDeclarationComponent> InvtDeclarationComponents { get; set; }
        public virtual DbSet<LookupFrequency> LookupFrequencies { get; set; }
        public virtual DbSet<LookupMonth> LookupMonths { get; set; }
        public virtual DbSet<MedicalInsuranceRate> MedicalInsuranceRates { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<OAuthMembership> OAuthMemberships { get; set; }
        public virtual DbSet<OfficeBranch> OfficeBranches { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SalaryComponent> SalaryComponents { get; set; }
        public virtual DbSet<SalaryStructureType> SalaryStructureTypes { get; set; }
        public virtual DbSet<Salutation> Salutations { get; set; }
        public virtual DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserSecurityQuestion> UserSecurityQuestions { get; set; }
        public virtual DbSet<ZipCode> ZipCodes { get; set; }
    
        public virtual int InsertErrorLog(string appdomain, string exception, string identity, string level, Nullable<int> line, string logger, string message, string method, string ndc, string property, string stacktrace, string stacktracedetail, Nullable<long> timestamp, string thread, string type, string username)
        {
            var appdomainParameter = appdomain != null ?
                new ObjectParameter("appdomain", appdomain) :
                new ObjectParameter("appdomain", typeof(string));
    
            var exceptionParameter = exception != null ?
                new ObjectParameter("exception", exception) :
                new ObjectParameter("exception", typeof(string));
    
            var identityParameter = identity != null ?
                new ObjectParameter("identity", identity) :
                new ObjectParameter("identity", typeof(string));
    
            var levelParameter = level != null ?
                new ObjectParameter("level", level) :
                new ObjectParameter("level", typeof(string));
    
            var lineParameter = line.HasValue ?
                new ObjectParameter("line", line) :
                new ObjectParameter("line", typeof(int));
    
            var loggerParameter = logger != null ?
                new ObjectParameter("logger", logger) :
                new ObjectParameter("logger", typeof(string));
    
            var messageParameter = message != null ?
                new ObjectParameter("message", message) :
                new ObjectParameter("message", typeof(string));
    
            var methodParameter = method != null ?
                new ObjectParameter("method", method) :
                new ObjectParameter("method", typeof(string));
    
            var ndcParameter = ndc != null ?
                new ObjectParameter("ndc", ndc) :
                new ObjectParameter("ndc", typeof(string));
    
            var propertyParameter = property != null ?
                new ObjectParameter("property", property) :
                new ObjectParameter("property", typeof(string));
    
            var stacktraceParameter = stacktrace != null ?
                new ObjectParameter("stacktrace", stacktrace) :
                new ObjectParameter("stacktrace", typeof(string));
    
            var stacktracedetailParameter = stacktracedetail != null ?
                new ObjectParameter("stacktracedetail", stacktracedetail) :
                new ObjectParameter("stacktracedetail", typeof(string));
    
            var timestampParameter = timestamp.HasValue ?
                new ObjectParameter("timestamp", timestamp) :
                new ObjectParameter("timestamp", typeof(long));
    
            var threadParameter = thread != null ?
                new ObjectParameter("thread", thread) :
                new ObjectParameter("thread", typeof(string));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertErrorLog", appdomainParameter, exceptionParameter, identityParameter, levelParameter, lineParameter, loggerParameter, messageParameter, methodParameter, ndcParameter, propertyParameter, stacktraceParameter, stacktracedetailParameter, timestampParameter, threadParameter, typeParameter, usernameParameter);
        }
    
        public virtual int InsertInfoLog(string appdomain, string identity, string level, string logger, string message, string method, Nullable<long> timestamp, string thread, string type, string username)
        {
            var appdomainParameter = appdomain != null ?
                new ObjectParameter("appdomain", appdomain) :
                new ObjectParameter("appdomain", typeof(string));
    
            var identityParameter = identity != null ?
                new ObjectParameter("identity", identity) :
                new ObjectParameter("identity", typeof(string));
    
            var levelParameter = level != null ?
                new ObjectParameter("level", level) :
                new ObjectParameter("level", typeof(string));
    
            var loggerParameter = logger != null ?
                new ObjectParameter("logger", logger) :
                new ObjectParameter("logger", typeof(string));
    
            var messageParameter = message != null ?
                new ObjectParameter("message", message) :
                new ObjectParameter("message", typeof(string));
    
            var methodParameter = method != null ?
                new ObjectParameter("method", method) :
                new ObjectParameter("method", typeof(string));
    
            var timestampParameter = timestamp.HasValue ?
                new ObjectParameter("timestamp", timestamp) :
                new ObjectParameter("timestamp", typeof(long));
    
            var threadParameter = thread != null ?
                new ObjectParameter("thread", thread) :
                new ObjectParameter("thread", typeof(string));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertInfoLog", appdomainParameter, identityParameter, levelParameter, loggerParameter, messageParameter, methodParameter, timestampParameter, threadParameter, typeParameter, usernameParameter);
        }
    
        public virtual int InsertMvcErrorLog(string appdomain, string aspnetcache, string aspnetcontext, string aspnetrequest, string aspnetsession, string exception, string identity, string level, Nullable<int> line, string logger, string message, string method, string ndc, string property, string stacktrace, string stacktracedetail, Nullable<long> timestamp, string thread, string type, string username)
        {
            var appdomainParameter = appdomain != null ?
                new ObjectParameter("appdomain", appdomain) :
                new ObjectParameter("appdomain", typeof(string));
    
            var aspnetcacheParameter = aspnetcache != null ?
                new ObjectParameter("aspnetcache", aspnetcache) :
                new ObjectParameter("aspnetcache", typeof(string));
    
            var aspnetcontextParameter = aspnetcontext != null ?
                new ObjectParameter("aspnetcontext", aspnetcontext) :
                new ObjectParameter("aspnetcontext", typeof(string));
    
            var aspnetrequestParameter = aspnetrequest != null ?
                new ObjectParameter("aspnetrequest", aspnetrequest) :
                new ObjectParameter("aspnetrequest", typeof(string));
    
            var aspnetsessionParameter = aspnetsession != null ?
                new ObjectParameter("aspnetsession", aspnetsession) :
                new ObjectParameter("aspnetsession", typeof(string));
    
            var exceptionParameter = exception != null ?
                new ObjectParameter("exception", exception) :
                new ObjectParameter("exception", typeof(string));
    
            var identityParameter = identity != null ?
                new ObjectParameter("identity", identity) :
                new ObjectParameter("identity", typeof(string));
    
            var levelParameter = level != null ?
                new ObjectParameter("level", level) :
                new ObjectParameter("level", typeof(string));
    
            var lineParameter = line.HasValue ?
                new ObjectParameter("line", line) :
                new ObjectParameter("line", typeof(int));
    
            var loggerParameter = logger != null ?
                new ObjectParameter("logger", logger) :
                new ObjectParameter("logger", typeof(string));
    
            var messageParameter = message != null ?
                new ObjectParameter("message", message) :
                new ObjectParameter("message", typeof(string));
    
            var methodParameter = method != null ?
                new ObjectParameter("method", method) :
                new ObjectParameter("method", typeof(string));
    
            var ndcParameter = ndc != null ?
                new ObjectParameter("ndc", ndc) :
                new ObjectParameter("ndc", typeof(string));
    
            var propertyParameter = property != null ?
                new ObjectParameter("property", property) :
                new ObjectParameter("property", typeof(string));
    
            var stacktraceParameter = stacktrace != null ?
                new ObjectParameter("stacktrace", stacktrace) :
                new ObjectParameter("stacktrace", typeof(string));
    
            var stacktracedetailParameter = stacktracedetail != null ?
                new ObjectParameter("stacktracedetail", stacktracedetail) :
                new ObjectParameter("stacktracedetail", typeof(string));
    
            var timestampParameter = timestamp.HasValue ?
                new ObjectParameter("timestamp", timestamp) :
                new ObjectParameter("timestamp", typeof(long));
    
            var threadParameter = thread != null ?
                new ObjectParameter("thread", thread) :
                new ObjectParameter("thread", typeof(string));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertMvcErrorLog", appdomainParameter, aspnetcacheParameter, aspnetcontextParameter, aspnetrequestParameter, aspnetsessionParameter, exceptionParameter, identityParameter, levelParameter, lineParameter, loggerParameter, messageParameter, methodParameter, ndcParameter, propertyParameter, stacktraceParameter, stacktracedetailParameter, timestampParameter, threadParameter, typeParameter, usernameParameter);
        }
    
        public virtual int ArchiveEmployee(Nullable<int> employeeID, string updatedByUserName)
        {
            var employeeIDParameter = employeeID.HasValue ?
                new ObjectParameter("EmployeeID", employeeID) :
                new ObjectParameter("EmployeeID", typeof(int));
    
            var updatedByUserNameParameter = updatedByUserName != null ?
                new ObjectParameter("UpdatedByUserName", updatedByUserName) :
                new ObjectParameter("UpdatedByUserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ArchiveEmployee", employeeIDParameter, updatedByUserNameParameter);
        }
    
        public virtual ObjectResult<Nullable<bool>> ValidateUser(string userName, Nullable<int> securityQuestionId, string answer, string emailAddress, string mobileNumber)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var securityQuestionIdParameter = securityQuestionId.HasValue ?
                new ObjectParameter("SecurityQuestionId", securityQuestionId) :
                new ObjectParameter("SecurityQuestionId", typeof(int));
    
            var answerParameter = answer != null ?
                new ObjectParameter("Answer", answer) :
                new ObjectParameter("Answer", typeof(string));
    
            var emailAddressParameter = emailAddress != null ?
                new ObjectParameter("EmailAddress", emailAddress) :
                new ObjectParameter("EmailAddress", typeof(string));
    
            var mobileNumberParameter = mobileNumber != null ?
                new ObjectParameter("MobileNumber", mobileNumber) :
                new ObjectParameter("MobileNumber", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("ValidateUser", userNameParameter, securityQuestionIdParameter, answerParameter, emailAddressParameter, mobileNumberParameter);
        }
    
        public virtual ObjectResult<GetEmployeeSalaryDetail_Result> GetEmployeeSalaryDetail(Nullable<int> employeeId, Nullable<int> financialYearFrom, Nullable<int> financialYearTo)
        {
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("EmployeeId", employeeId) :
                new ObjectParameter("EmployeeId", typeof(int));
    
            var financialYearFromParameter = financialYearFrom.HasValue ?
                new ObjectParameter("FinancialYearFrom", financialYearFrom) :
                new ObjectParameter("FinancialYearFrom", typeof(int));
    
            var financialYearToParameter = financialYearTo.HasValue ?
                new ObjectParameter("FinancialYearTo", financialYearTo) :
                new ObjectParameter("FinancialYearTo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEmployeeSalaryDetail_Result>("GetEmployeeSalaryDetail", employeeIdParameter, financialYearFromParameter, financialYearToParameter);
        }
    
        public virtual ObjectResult<GetEmpAppraisalHistory_Result> GetEmpAppraisalHistory(Nullable<int> employeeId)
        {
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("EmployeeId", employeeId) :
                new ObjectParameter("EmployeeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEmpAppraisalHistory_Result>("GetEmpAppraisalHistory", employeeIdParameter);
        }
    
        public virtual ObjectResult<ArchiveEmpSalaryDetail_Result> ArchiveEmpSalaryDetail(Nullable<int> empSalaryStructureId, string updatedByUserName)
        {
            var empSalaryStructureIdParameter = empSalaryStructureId.HasValue ?
                new ObjectParameter("EmpSalaryStructureId", empSalaryStructureId) :
                new ObjectParameter("EmpSalaryStructureId", typeof(int));
    
            var updatedByUserNameParameter = updatedByUserName != null ?
                new ObjectParameter("UpdatedByUserName", updatedByUserName) :
                new ObjectParameter("UpdatedByUserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ArchiveEmpSalaryDetail_Result>("ArchiveEmpSalaryDetail", empSalaryStructureIdParameter, updatedByUserNameParameter);
        }
    
        public virtual int ResetEmpSalaryStructureId(Nullable<int> empSalaryStructureId)
        {
            var empSalaryStructureIdParameter = empSalaryStructureId.HasValue ?
                new ObjectParameter("EmpSalaryStructureId", empSalaryStructureId) :
                new ObjectParameter("EmpSalaryStructureId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ResetEmpSalaryStructureId", empSalaryStructureIdParameter);
        }
    
        public virtual ObjectResult<SalaryStructureChangeHistory_Result> SalaryStructureChangeHistory(Nullable<int> empSalaryStructureId)
        {
            var empSalaryStructureIdParameter = empSalaryStructureId.HasValue ?
                new ObjectParameter("EmpSalaryStructureId", empSalaryStructureId) :
                new ObjectParameter("EmpSalaryStructureId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SalaryStructureChangeHistory_Result>("SalaryStructureChangeHistory", empSalaryStructureIdParameter);
        }
    
        public virtual ObjectResult<EmpSalaryDetailArchiveByUniqueChangeId_Result> EmpSalaryDetailArchiveByUniqueChangeId(Nullable<System.Guid> uniqueChangeId)
        {
            var uniqueChangeIdParameter = uniqueChangeId.HasValue ?
                new ObjectParameter("UniqueChangeId", uniqueChangeId) :
                new ObjectParameter("UniqueChangeId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmpSalaryDetailArchiveByUniqueChangeId_Result>("EmpSalaryDetailArchiveByUniqueChangeId", uniqueChangeIdParameter);
        }
    
        public virtual int GenerateEmpPaySlip(Nullable<int> employeeId, Nullable<int> monthId, Nullable<int> year)
        {
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("EmployeeId", employeeId) :
                new ObjectParameter("EmployeeId", typeof(int));
    
            var monthIdParameter = monthId.HasValue ?
                new ObjectParameter("MonthId", monthId) :
                new ObjectParameter("MonthId", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GenerateEmpPaySlip", employeeIdParameter, monthIdParameter, yearParameter);
        }
    
        public virtual ObjectResult<GetEmpSalaryStructureComparisonList_Result> GetEmpSalaryStructureComparisonList(Nullable<int> employeeId, Nullable<System.Guid> uniqueChangeId, Nullable<int> financialYearFrom)
        {
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("EmployeeId", employeeId) :
                new ObjectParameter("EmployeeId", typeof(int));
    
            var uniqueChangeIdParameter = uniqueChangeId.HasValue ?
                new ObjectParameter("UniqueChangeId", uniqueChangeId) :
                new ObjectParameter("UniqueChangeId", typeof(System.Guid));
    
            var financialYearFromParameter = financialYearFrom.HasValue ?
                new ObjectParameter("FinancialYearFrom", financialYearFrom) :
                new ObjectParameter("FinancialYearFrom", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEmpSalaryStructureComparisonList_Result>("GetEmpSalaryStructureComparisonList", employeeIdParameter, uniqueChangeIdParameter, financialYearFromParameter);
        }
    
        public virtual int UpdateMediclaimAmount(Nullable<decimal> zeroToFive, Nullable<decimal> fiveToTen, Nullable<decimal> tenPlus, Nullable<System.DateTime> effectiveFrom, Nullable<System.DateTime> effectiveTo)
        {
            var zeroToFiveParameter = zeroToFive.HasValue ?
                new ObjectParameter("ZeroToFive", zeroToFive) :
                new ObjectParameter("ZeroToFive", typeof(decimal));
    
            var fiveToTenParameter = fiveToTen.HasValue ?
                new ObjectParameter("FiveToTen", fiveToTen) :
                new ObjectParameter("FiveToTen", typeof(decimal));
    
            var tenPlusParameter = tenPlus.HasValue ?
                new ObjectParameter("TenPlus", tenPlus) :
                new ObjectParameter("TenPlus", typeof(decimal));
    
            var effectiveFromParameter = effectiveFrom.HasValue ?
                new ObjectParameter("EffectiveFrom", effectiveFrom) :
                new ObjectParameter("EffectiveFrom", typeof(System.DateTime));
    
            var effectiveToParameter = effectiveTo.HasValue ?
                new ObjectParameter("EffectiveTo", effectiveTo) :
                new ObjectParameter("EffectiveTo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateMediclaimAmount", zeroToFiveParameter, fiveToTenParameter, tenPlusParameter, effectiveFromParameter, effectiveToParameter);
        }
    
        public virtual ObjectResult<GetEmpAnnualContributionUnderChapterVIA_Result> GetEmpAnnualContributionUnderChapterVIA(Nullable<int> employeeId, Nullable<int> financialYear)
        {
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("EmployeeId", employeeId) :
                new ObjectParameter("EmployeeId", typeof(int));
    
            var financialYearParameter = financialYear.HasValue ?
                new ObjectParameter("FinancialYear", financialYear) :
                new ObjectParameter("FinancialYear", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEmpAnnualContributionUnderChapterVIA_Result>("GetEmpAnnualContributionUnderChapterVIA", employeeIdParameter, financialYearParameter);
        }
    
        public virtual ObjectResult<GetEmployeeMonthlyTaxDeductions_Result> GetEmployeeMonthlyTaxDeductions(Nullable<int> employeeId, Nullable<int> financialYear)
        {
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("EmployeeId", employeeId) :
                new ObjectParameter("EmployeeId", typeof(int));
    
            var financialYearParameter = financialYear.HasValue ?
                new ObjectParameter("FinancialYear", financialYear) :
                new ObjectParameter("FinancialYear", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEmployeeMonthlyTaxDeductions_Result>("GetEmployeeMonthlyTaxDeductions", employeeIdParameter, financialYearParameter);
        }
    
        public virtual ObjectResult<GetEmployeeYearlyTaxExemptions_Result> GetEmployeeYearlyTaxExemptions(Nullable<int> employeeId, Nullable<int> financialYear)
        {
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("EmployeeId", employeeId) :
                new ObjectParameter("EmployeeId", typeof(int));
    
            var financialYearParameter = financialYear.HasValue ?
                new ObjectParameter("FinancialYear", financialYear) :
                new ObjectParameter("FinancialYear", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEmployeeYearlyTaxExemptions_Result>("GetEmployeeYearlyTaxExemptions", employeeIdParameter, financialYearParameter);
        }
    
        public virtual ObjectResult<GetMonthlyTaxDeduction_Result> GetMonthlyTaxDeduction(Nullable<int> employeeId, Nullable<int> financialYear)
        {
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("EmployeeId", employeeId) :
                new ObjectParameter("EmployeeId", typeof(int));
    
            var financialYearParameter = financialYear.HasValue ?
                new ObjectParameter("FinancialYear", financialYear) :
                new ObjectParameter("FinancialYear", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetMonthlyTaxDeduction_Result>("GetMonthlyTaxDeduction", employeeIdParameter, financialYearParameter);
        }
    }
}
