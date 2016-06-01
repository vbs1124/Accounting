using System.Web;
using System.Web.Mvc;
using Vserv.Accounting.Web.Code.Utilities;

namespace Vserv.Accounting.Web.Helpers
{
	public static class JsonHtmlHelpers
	{
		public static IHtmlString JsonFor<T>(this HtmlHelper helper, T obj)
		{
			return helper.Raw(obj.ToJson());
		}
	}
}