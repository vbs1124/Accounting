using System.Collections.Generic;
using System.Linq;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Core.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IDatabaseContext _databaseContext;

        public ConfigService(IDatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        string IConfigService.GetValue(ConfigName name)
        {
            var config = this._databaseContext.Configs.FirstOrDefault(x => x.Key.Equals(name.ToString()));
            if (config != null)
            {
                return config.Value;
            }
            return null;
        }

        IDictionary<string, string> IConfigService.GetValues(ConfigName[] configNames)
        {
            var names = configNames.Select(x => x.ToString());
            var configs = this._databaseContext.Configs.Where(x => names.Contains(x.Key));

            return configs.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
