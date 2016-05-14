using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vserv.Accounting.Data;

namespace Vserv.Accounting.Business.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Initialize()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(EmployeeRepository).Assembly));
            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(UserProfileRepository).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Vserv.Accounting.Data.DataRepositoryFactory).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Vserv.Common.Logger).Assembly));

            return Vserv.Common.Bootstrapper.MEFLoader.Initialize(catalog.Catalogs);
        }
    }
}
