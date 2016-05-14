#region Namespaces
using NUnit.Framework;
using System.Web.Mvc;
using Vserv.Accounting.Web.Controllers; 
#endregion

namespace Vserv.Accounting.Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
