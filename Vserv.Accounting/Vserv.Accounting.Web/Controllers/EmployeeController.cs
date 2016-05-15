#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vserv.Accounting.Business.Managers;
using Vserv.Accounting.Common.Enums;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Models;
using Vserv.Common.Extensions;
#endregion

namespace Vserv.Accounting.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        #region Action Methods

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            var employees = _employeeManager.GetEmployees();
            return View(employees);
        }

        public ActionResult Add()
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            EmployeeModel employeeModel = new EmployeeModel();
            SetDropdownValues(employeeModel);
            return View(employeeModel);
        }

        [HttpPost]
        public ActionResult Add(EmployeeModel employeeModel)
        {
            employeeModel.CreatedBy = User.Identity.Name;
            employeeModel.CreatedDate = DateTime.Now;
            EmployeeManager _employeeManager = new EmployeeManager();

            // Perform Save for Employee
            if (ModelState.IsValid)
            {
                Employee employee = ConvertTo(employeeModel);
                _employeeManager.AddEmployee(employee);
                return RedirectToAction("Success", "Home", new { successMessage = "Employee added successfully." });
            }
            ModelState.AddModelError("emp_errors", "Please fill the required fields...");
            SetDropdownValues(employeeModel);
            return View(employeeModel);
        }

        public ActionResult Edit(int employeeId)
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            var employee = _employeeManager.GetEmployee(employeeId);
            EmployeeModel employeeModel = ConvertTo(employee);
            SetDropdownValues(employeeModel);
            return View(employeeModel);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModel employeeModel)
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            employeeModel.UpdatedBy = User.Identity.Name;
            employeeModel.UpdatedDate = DateTime.Now;
            // Perform Save for Employee
            if (ModelState.IsValid)
            {
                Employee employee = ConvertTo(employeeModel);
                _employeeManager.EditEmployee(employee);
                return RedirectToAction("Success", "Home", new { successMessage = "Employee updated successfully." });
            }

            SetDropdownValues(employeeModel);
            return View(employeeModel);
        }

        public ActionResult Delete(int employeeId)
        {
            EmployeeManager _employeeManager = new EmployeeManager();
            _employeeManager.DeleteEmployee(employeeId);
            return RedirectToAction("List");
        }

        public ActionResult Banking()
        {
            return View();
        }

        public ActionResult Taxation()
        {
            return View();
        }

        public ActionResult Salary()
        {
            return View();
        }

        #endregion

        #region Private Methods

        private List<SelectListItem> GetGenders()
        {
            var genders = new List<SelectListItem>();
            genders.Add(new SelectListItem { Text = "Unknown", Value = "0" });
            genders.Add(new SelectListItem { Text = "Male", Value = "1" });
            genders.Add(new SelectListItem { Text = "Female", Value = "2" });

            return genders;
        }

        private List<CityModel> ConvertTo(List<City> cities)
        {
            var result = new List<CityModel>();

            if (cities.IsNotNull())
            {
                cities.ForEach(city => result.Add(new CityModel
                {
                    CityId = city.CityId,
                    StateId = city.StateId,
                    Name = city.Name,
                    Description = city.Description,
                    DisplayOrder = city.DisplayOrder,
                    IsActive = city.IsActive,
                    CreatedBy = city.CreatedBy,
                    UpdatedBy = city.UpdatedBy,
                    CreatedDate = city.CreatedDate,
                    UpdatedDate = city.UpdatedDate,
                    //Addresses = item.XXXXX,
                    //State = item.XXXXX,
                    //ZipCodes = item.XXXXX
                }));
            }

            return result;
        }

        private List<StateModel> ConvertTo(List<State> states)
        {
            List<StateModel> stateModelList = new List<StateModel>();
            if (states.IsNotNull())
            {
                states.ForEach(item => stateModelList.Add(new StateModel
                {
                    StateId = item.StateId,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    UpdatedBy = item.UpdatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate,
                }));
            }

            return stateModelList;
        }

        private List<SalutationModel> ConvertTo(List<Salutation> salutations)
        {
            var result = new List<SalutationModel>();

            if (salutations.IsNotNull())
            {
                salutations.ForEach(item => result.Add(new SalutationModel
                {
                    SalutationId = item.SalutationId,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    UpdatedBy = item.UpdatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate,
                }));
            }

            return result;
        }

        private List<OfficeBranchModel> ConvertTo(List<OfficeBranch> officeBranches)
        {
            var result = new List<OfficeBranchModel>();

            if (officeBranches.IsNotNull())
            {
                officeBranches.ForEach(item => result.Add(new OfficeBranchModel
                {
                    OfficeBranchId = item.OfficeBranchId,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    UpdatedBy = item.UpdatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate
                }));
            }

            return result;
        }

        private List<DesignationModel> ConvertTo(List<Designation> designations)
        {
            var result = new List<DesignationModel>();

            if (designations.IsNotNull())
            {
                designations.ForEach(item => result.Add(new DesignationModel
                {
                    DesignationId = item.DesignationId,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    UpdatedBy = item.UpdatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate
                }));
            }

            return result;
        }

        private List<DepartmentModel> ConvertTo(List<Department> departments)
        {
            var result = new List<DepartmentModel>();

            if (departments.IsNotNull())
            {
                departments.ForEach(item => result.Add(new DepartmentModel
                {
                    DepartmentId = item.DepartmentId,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    UpdatedBy = item.UpdatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate,
                }));
            }

            return result;
        }

        private List<AddressTypeModel> ConvertTo(List<AddressType> addressTypes)
        {
            var result = new List<AddressTypeModel>();

            if (addressTypes.IsNotNull())
            {
                addressTypes.ForEach(item => result.Add(new AddressTypeModel
                {
                    AddressTypeId = item.AddressTypeId,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    UpdatedBy = item.UpdatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate,
                }));
            }

            return result;
        }

        private Employee ConvertTo(EmployeeModel employeeModel)
        {
            if (employeeModel.IsNull())
            {
                return new Employee();
            }
            List<EmployeeAddress> addresses = new List<EmployeeAddress>();

            if (employeeModel.PermanentAddress.IsNotNull())
            {
                Address permanentAddress = new Address
                {
                    AddressId = employeeModel.PermanentAddress.AddressId,
                    Address1 = employeeModel.PermanentAddress.Address1,
                    Address2 = employeeModel.PermanentAddress.Address2,
                    CountryId = null,
                    StateId = employeeModel.PermanentAddress.StateId,
                    CityId = null,
                    ZipCodeId = null,
                    City = employeeModel.PermanentAddress.City,
                    ZipCode = employeeModel.PermanentAddress.ZipCode,
                    Latitude = employeeModel.PermanentAddress.Latitude,
                    Longitude = employeeModel.PermanentAddress.Longitude,
                    IsCommunicationAddress = false,
                    AddressTypeId = Convert.ToInt32(AddressTypeEnum.PermanentAddress),
                    IsActive = true,
                    CreatedBy = User.Identity.Name,
                    UpdatedBy = null,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = null
                };
                addresses.Add(new EmployeeAddress { Address = permanentAddress });
            }

            if (employeeModel.MailingAddress.IsNotNull())
            {
                Address mailingAddress = new Address
                {
                    AddressId = employeeModel.MailingAddress.AddressId,
                    Address1 = employeeModel.MailingAddress.Address1,
                    Address2 = employeeModel.MailingAddress.Address2,
                    CountryId = null,
                    StateId = employeeModel.MailingAddress.StateId,
                    CityId = null,
                    ZipCodeId = null,
                    City = employeeModel.MailingAddress.City,
                    ZipCode = employeeModel.MailingAddress.ZipCode,
                    Latitude = employeeModel.MailingAddress.Latitude,
                    Longitude = employeeModel.MailingAddress.Longitude,
                    IsCommunicationAddress = false,
                    AddressTypeId = Convert.ToInt32(AddressTypeEnum.MailingAddress),
                    IsActive = true,
                    CreatedBy = User.Identity.Name,
                    UpdatedBy = null,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = null,
                };

                addresses.Add(new EmployeeAddress { Address = mailingAddress });
            }

            return new Employee
            {
                EmployeeId = employeeModel.EmployeeId,
                FirstName = employeeModel.FirstName,
                MiddleName = employeeModel.MiddleName,
                LastName = employeeModel.LastName,
                FatherName = employeeModel.FatherName,
                MotherName = employeeModel.MotherName,
                UniversalAccountNumber = employeeModel.UniversalAccountNumber,
                PermanentAccountNumber = employeeModel.PermanentAccountNumber,
                AADHAARNumber = employeeModel.AADHAARNumber,
                MobileNumber = employeeModel.MobileNumber,
                EmailAddress = employeeModel.EmailAddress,
                BirthDay = employeeModel.BirthDay.IsNotNull() && employeeModel.BirthDay.HasValue ? employeeModel.BirthDay.Value : DateTime.Now,
                JoiningDate = employeeModel.JoiningDate.IsNotNull() && employeeModel.JoiningDate.HasValue ? employeeModel.JoiningDate.Value : DateTime.Now,
                RelievingDate = employeeModel.RelievingDate,
                VBS_Id = employeeModel.VBS_Id,
                DesignationId = employeeModel.DesignationId.Value,
                SalutationId = employeeModel.SalutationId.Value,
                GenderId = employeeModel.GenderId.Value,
                OfficeBranchId = employeeModel.OfficeBranchId.Value,
                IsActive = employeeModel.IsActive,
                CreatedBy = User.Identity.Name,
                UpdatedBy = employeeModel.UpdatedBy,
                CreatedDate = DateTime.Now,
                UpdatedDate = employeeModel.UpdatedDate,
                EmployeeAddresses = addresses
            };
        }

        private void SetDropdownValues(EmployeeModel employeeModel)
        {
            EmployeeManager _employeeManager = new EmployeeManager();

            //employeeModel.AddressTypes = ConvertTo(_employeeManager.GetAddressTypes());
            employeeModel.Departments = ConvertTo(_employeeManager.GetDepartments().Where(condition => condition.IsActive).ToList());
            employeeModel.Designations = ConvertTo(_employeeManager.GetDesignations().Where(condition => condition.IsActive).ToList());
            employeeModel.OfficeBranches = ConvertTo(_employeeManager.GetOfficeBranches().Where(condition => condition.IsActive).ToList());
            employeeModel.Salutations = ConvertTo(_employeeManager.GetSalutations().Where(condition => condition.IsActive).ToList());
            employeeModel.Genders = GetGenders();
            employeeModel.States = ConvertTo(_employeeManager.GetStates().Where(condition => condition.IsActive).ToList());
            //employeeModel.Cities = ConvertTo(_employeeManager.GetCities());
        }

        private EmployeeModel ConvertTo(Employee employee)
        {
            AddressModel permanentAddress = null;
            AddressModel mailingAddress = null;

            foreach (var item in employee.EmployeeAddresses)
            {
                if (item.Address.IsNotNull() && item.Address.AddressTypeId.Equals(Convert.ToInt32(AddressTypeEnum.PermanentAddress)))
                {
                    permanentAddress = new AddressModel
                    {
                        AddressId = item.Address.AddressId,
                        Address1 = item.Address.Address1,
                        Address2 = item.Address.Address2,
                        CountryId = item.Address.CountryId,
                        StateId = item.Address.StateId,
                        City = item.Address.City,
                        ZipCode = item.Address.ZipCode,
                        Latitude = item.Address.Latitude,
                        Longitude = item.Address.Longitude,
                        IsCommunicationAddress = item.Address.IsCommunicationAddress,
                        AddressTypeId = item.Address.AddressTypeId,
                        IsActive = item.Address.IsActive,
                        CreatedBy = item.Address.CreatedBy,
                        UpdatedBy = item.Address.UpdatedBy,
                        CreatedDate = item.Address.CreatedDate,
                        UpdatedDate = item.Address.UpdatedDate,
                    };
                }

                if (item.Address.IsNotNull() && item.Address.AddressTypeId.Equals(Convert.ToInt32(AddressTypeEnum.MailingAddress)))
                {
                    mailingAddress = new AddressModel
                    {
                        AddressId = item.Address.AddressId,
                        Address1 = item.Address.Address1,
                        Address2 = item.Address.Address2,
                        CountryId = item.Address.CountryId,
                        StateId = item.Address.StateId,
                        City = item.Address.City,
                        ZipCode = item.Address.ZipCode,
                        Latitude = item.Address.Latitude,
                        Longitude = item.Address.Longitude,
                        IsCommunicationAddress = item.Address.IsCommunicationAddress,
                        AddressTypeId = item.Address.AddressTypeId,
                        IsActive = item.Address.IsActive,
                        CreatedBy = item.Address.CreatedBy,
                        UpdatedBy = item.Address.UpdatedBy,
                        CreatedDate = item.Address.CreatedDate,
                        UpdatedDate = item.Address.UpdatedDate,
                    };
                }
            }

            return new EmployeeModel
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                FatherName = employee.FatherName,
                MotherName = employee.MotherName,
                UniversalAccountNumber = employee.UniversalAccountNumber,
                PermanentAccountNumber = employee.PermanentAccountNumber,
                AADHAARNumber = employee.AADHAARNumber,
                MobileNumber = employee.MobileNumber,
                EmailAddress = employee.EmailAddress,
                BirthDay = employee.BirthDay,
                JoiningDate = employee.JoiningDate,
                RelievingDate = employee.RelievingDate,
                VBS_Id = employee.VBS_Id,
                DesignationId = employee.DesignationId,
                SalutationId = employee.SalutationId,
                GenderId = employee.GenderId,
                OfficeBranchId = employee.OfficeBranchId,
                IsActive = employee.IsActive,
                CreatedBy = employee.CreatedBy,
                UpdatedBy = employee.UpdatedBy,
                CreatedDate = employee.CreatedDate,
                UpdatedDate = employee.UpdatedDate,
                PermanentAddress = permanentAddress,
                MailingAddress = mailingAddress
            };
        }

        #endregion Private Methods
    }
}