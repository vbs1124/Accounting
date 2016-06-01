using System.Web.Mvc;
using Vserv.Accounting.Web.Code.ActionResults;

namespace Vserv.Accounting.Web.Controllers
{
    public abstract class ViewControllerBase : Controller
    {
        protected CustomJsonResult<T> CustomJson<T>(T model)
        {
            return new CustomJsonResult<T>() { Data = model };
        }
    }
}