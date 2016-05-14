using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vserv.Accounting.Web.Code.Unity
{
    public interface IUnityContainerAccessor
    {
        IUnityContainer Container { get; }
    }
}