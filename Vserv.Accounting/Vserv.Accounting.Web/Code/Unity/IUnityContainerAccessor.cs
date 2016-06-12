using Microsoft.Practices.Unity;

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