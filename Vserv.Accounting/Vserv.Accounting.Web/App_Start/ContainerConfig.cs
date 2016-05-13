using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Vserv.Accounting.Core.Services;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Web.Code.Unity;

namespace Vserv.Accounting.Web
{
    public static class ContainerConfig
    {
        public static void RegisterTypes(UnityContainer container)
        {
            container.RegisterInstance<IUnityContainer>(container);

            container.RegisterType<VservAccountingDBEntities>(new HttpContextLifetimeManager<VservAccountingDBEntities>(), new InjectionConstructor());

            container.RegisterType<IDatabaseContext, VservAccountingDBEntities>();

            container.RegisterType<IUsersService, UsersService>();

            container.RegisterType<SmtpClient>(new InjectionConstructor());

            container.RegisterType<IEmailService, EmailService>();

            container.RegisterType<IConfigService, ConfigService>();

            ControllerBuilder.Current.SetControllerFactory(typeof(UnityControllerFactory));
        }
    }
}