#region Namespaces

using System.Collections.Generic;
using System.ComponentModel.Composition;
using Vserv.Accounting.Data.Entity;
using System.Linq;
using Vserv.Accounting.Common;
using System;
#endregion

namespace Vserv.Accounting.Data.Repositories
{
    [Export(typeof(IEmpSalaryStructureRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmpSalaryStructureRepo : DataRepositoryBase<EmpSalaryStructure>, IEmpSalaryStructureRepo
    {
        public EmpSalaryStructure SaveEmpSalaryStructure(EmpSalaryStructure empSalaryStructure)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.EmpSalaryStructures.Add(empSalaryStructure);
                context.SaveChanges();
                return empSalaryStructure;
            }
        }

        public List<GetEmpAppraisalHistory_Result> GetEmployeeAppraisalHistory(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.GetEmpAppraisalHistory(employeeId).ToList();
                return result;
            }
        }

        public EmpSalaryStructure GetCurrentEmpSalaryStructure(int employeeId)
        {
            using (var context = new VservAccountingDBEntities())
            {
                var result = context.EmpSalaryStructures.Include("EmpSalaryDetails").OrderByDescending(order => order.CreatedDate).FirstOrDefault(structure => structure.EmployeeId == employeeId);
                return result;
            }
        }

        public bool InsertEmpSalaryStructure(EmpSalaryStructure empSalaryStructure)
        {
            using (var context = new VservAccountingDBEntities())
            {
                context.EmpSalaryStructures.Add(empSalaryStructure);
                return context.SaveChanges() > 0;
            }
        }

        public bool SaveEmpSalaryStructure(EmpSalaryStructure empSalaryStructure, List<EmpSalaryDetail> employeeSalaryDetails, string userName)
        {
            using (var context = new VservAccountingDBEntities())
            {
                if (employeeSalaryDetails.IsNotNull() && employeeSalaryDetails.Any())
                {
                    var recordsToInsert = employeeSalaryDetails.Where(condition => condition.EmpSalaryDetailId <= 0).ToList();
                    var recordsToUpate = employeeSalaryDetails.Except(recordsToInsert).ToList();

                    // Update the existing Record
                    if (recordsToUpate.IsNotNull() && recordsToUpate.Any())
                    {
                        foreach (var item in recordsToUpate)
                        {
                            item.UpdatedBy = userName;
                            item.UpdatedDate = DateTime.Now;
                            context.EmpSalaryDetails.Add(item);
                            //context.SaveChanges();
                        }
                    }

                    // Insert EmpSalaryStructure
                    EmpSalaryStructure currentEmpSalaryStructureInserted = GetCurrentEmpSalaryStructure(empSalaryStructure.EmployeeId);
                    empSalaryStructure.CreatedDate = DateTime.Now;
                    empSalaryStructure.CreatedBy = userName;
                    empSalaryStructure.IsActive = true;

                    if (currentEmpSalaryStructureInserted.IsNotNull())
                    {
                        empSalaryStructure.ParentId = currentEmpSalaryStructureInserted.EmpSalaryStructureId;
                    }

                    Add(empSalaryStructure, userName);

                    currentEmpSalaryStructureInserted = GetCurrentEmpSalaryStructure(empSalaryStructure.EmployeeId);
                    // Insert new records against the currently generated EmpSalaryStructure.
                    if (recordsToInsert.IsNotNull() && recordsToInsert.Any())
                    {
                        foreach (var item in recordsToInsert)
                        {
                            item.EmpSalaryStructureId = currentEmpSalaryStructureInserted.EmpSalaryStructureId;
                            context.EmpSalaryDetails.Add(item);
                            context.SaveChanges();
                        }
                    }

                    // Update EmpSalaryStructureId of those breakups which were updated.
                    foreach (var item in recordsToUpate)
                    {
                        item.EmpSalaryStructureId = currentEmpSalaryStructureInserted.EmpSalaryStructureId;
                        context.EmpSalaryDetails.Add(item);
                    }

                    context.SaveChanges();
                }
            }

            return true;
        }
    }
}