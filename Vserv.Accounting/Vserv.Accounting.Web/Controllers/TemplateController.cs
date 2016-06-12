using System.Web.Mvc;

namespace Vserv.Accounting.Web.Controllers
{
    public class TemplateController : ViewControllerBase
    {
        public PartialViewResult Render(string feature, string name)
        {
            return PartialView(string.Format("~/Scripts/App/{0}/templates/{1}", feature, name));
        }
	}
}