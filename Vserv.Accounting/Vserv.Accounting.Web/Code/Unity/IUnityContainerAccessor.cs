using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vserv.Accounting.Web.Code.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnityContainerAccessor
    {
        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        IUnityContainer Container { get; }
    }
}