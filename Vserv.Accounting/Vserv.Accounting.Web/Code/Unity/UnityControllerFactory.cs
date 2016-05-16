using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vserv.Accounting.Web.Code.Unity
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Creates the controller.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override IController CreateController(RequestContext context, string controllerName)
        {
            try
            {
                var type = GetControllerType(context, controllerName);

                if (type == null)
                {
                    throw new InvalidOperationException(string.Format("Could not find a controller with the name {0}.", controllerName));
                }

                var container = GetContainer(context);

                return (IController)container.Resolve(type);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">context</exception>
        /// <exception cref="InvalidOperationException">
        /// You must extend the HttpApplication in your web project and implement the IContainerAccessor to properly expose your container instance
        /// or
        /// The container seems to be unavailable in your HttpApplication subclass
        /// </exception>
        protected virtual IUnityContainer GetContainer(RequestContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var unityContainerAccessor = context.HttpContext.ApplicationInstance as IUnityContainerAccessor;

            if (unityContainerAccessor == null)
            {
                throw new InvalidOperationException("You must extend the HttpApplication in your web project and implement the IContainerAccessor to properly expose your container instance");
            }

            IUnityContainer container = unityContainerAccessor.Container;

            if (container == null)
            {
                throw new InvalidOperationException("The container seems to be unavailable in your HttpApplication subclass");
            }

            return container;
        }
    }
}