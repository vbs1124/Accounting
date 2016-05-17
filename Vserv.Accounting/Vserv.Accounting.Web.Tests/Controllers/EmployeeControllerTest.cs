#region Namespaces
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Vserv.Accounting.Web;
using Vserv.Accounting.Web.Controllers;
using FakeItEasy;

#endregion

namespace Vserv.Accounting.Web.Tests.Controllers
{
    [TestFixture]
    public class EmployeeControllerTest
    {
        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void List()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();

            // Act
            ViewResult result = controller.List() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Add()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();

            // Act
            ViewResult result = controller.Add() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Edit()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();
            int employeeId = A.Dummy<int>();

            // Act
            ViewResult result = controller.Edit(employeeId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();
            int employeeId = A.Dummy<int>();

            // Act
            ViewResult result = controller.Delete(employeeId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Banking()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();

            // Act
            ViewResult result = controller.Banking() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Taxation()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();

            // Act
            ViewResult result = controller.Taxation() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Salary()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();

            // Act
            ViewResult result = controller.Salary() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
