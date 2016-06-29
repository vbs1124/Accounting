#region Namespaces

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Vserv.Accounting.Common;
using Vserv.Accounting.Data.Entity;
using System.Linq;
using Vserv.Accounting.Data.Entity.Models;
#endregion

namespace Vserv.Accounting.Data
{
    [Export(typeof(IInvestmentCategoryRepo))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InvestmentCategoryRepo : DataRepositoryBase<InvestmentCategory>, IInvestmentCategoryRepo
    {
        public List<InvestmentCategory> GetInvestmentCatogories(int financialYear)
        {
            using (VservAccountingDBEntities context = new VservAccountingDBEntities())
            {
                InvtDeclarationComponent invtDeclarationComponent = context.InvtDeclarationComponents.FirstOrDefault(mapping => mapping.Year == financialYear);

                if (invtDeclarationComponent.IsNotNull())
                {
                    return context.InvestmentCategories.Include("InvestmentSubCategories").Where(condition => condition.MappingId == invtDeclarationComponent.MappingId).ToList();
                }

                return new List<InvestmentCategory>(); // return blank list.
            }
        }

        public bool SaveEmployeeInvestments(List<EmpInvestment> investmentCatogories)
        {
            List<EmpInvestment> UpdaterecordList = investmentCatogories.Where(x => x.EmpInvestmentId > 0).ToList();
            List<EmpInvestment> NewlyrecordList = investmentCatogories.Where(x => x.EmpInvestmentId == 0).ToList();
            using (VservAccountingDBEntities context=new VservAccountingDBEntities())
            {
                if(NewlyrecordList.Count>0)
                {
                    context.EmpInvestments.AddRange(NewlyrecordList);
                }
                if(UpdaterecordList.Count>0)
                {
                    foreach(var row in UpdaterecordList)
                    {
                        EmpInvestment investmentDetail = context.EmpInvestments.Where(x => x.EmpInvestmentId == row.EmpInvestmentId).FirstOrDefault();
                        investmentDetail.DeclaredAmount = row.DeclaredAmount;
                        investmentDetail.UpdatedBy = row.UpdatedBy;
                        investmentDetail.UpdatedDate = System.DateTime.Now;
                    }
                }
                
                context.SaveChanges();
                return true;
            }

        }

        public List<EmpInvestment> GetEmpInvestmentByEmpId(int employeeId)
        {
            using (VservAccountingDBEntities context = new VservAccountingDBEntities())
            {
                return context.EmpInvestments.Where(x => x.EmployeeId == employeeId).ToList();
            }
        }

        public List<InvestmentCategory> GetInvestmentCategories(int financialYear,int employeeId)
        {
            List<InvestmentCategory> investmentCategoryList = new List<InvestmentCategory>();           
            List<EmpInvestment> empInvestmentList = new List<EmpInvestment>();
            EmpInvestmentDeclarationModel investmentDeclaration = new EmpInvestmentDeclarationModel();
            using (VservAccountingDBEntities context = new VservAccountingDBEntities())
            {
                InvtDeclarationComponent invtDeclarationComponent = context.InvtDeclarationComponents.FirstOrDefault(mapping => mapping.Year == financialYear);

                if (invtDeclarationComponent.IsNotNull())
                {
                    investmentCategoryList= context.InvestmentCategories.Include("InvestmentSubCategories").Where(condition => condition.MappingId == invtDeclarationComponent.MappingId).ToList();
                    empInvestmentList = context.EmpInvestments.Where(x => x.EmployeeId == employeeId).ToList();
                    investmentDeclaration.EmployeeId = employeeId;
                    
                    if(empInvestmentList.Count>0)
                    {
                        int j = 0;
                        for(int i=0;i<investmentCategoryList.Count;i++)
                        {
                            if(investmentCategoryList[i].InvestmentCategoryId==empInvestmentList[j].CategoryId)
                            {
                                List<InvestmentSubCategory> investmentSubCategory = new List<InvestmentSubCategory>();
                                foreach (var subcat in investmentCategoryList[i].InvestmentSubCategories)
                                {
                                    subcat.DefaultAmount = empInvestmentList[j].DeclaredAmount;
                                    investmentSubCategory.Add(subcat);
                                    j++;
                                }
                                investmentCategoryList[i].InvestmentSubCategories = investmentSubCategory;
                            }
                        }
                        return investmentCategoryList;
                    }
                    else
                    {
                        return investmentCategoryList;
                    }
                }

                return new List<InvestmentCategory>(); // return blank list.
            }
        }
    }
}
