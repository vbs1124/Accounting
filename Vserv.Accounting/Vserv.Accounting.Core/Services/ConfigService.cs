using System.Collections.Generic;
using System.Linq;
using Vserv.Accounting.Data.Entity;

namespace Vserv.Accounting.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Accounting.Core.Services.IConfigService" />
    public class ConfigService : IConfigService
    {
        /// <summary>
        /// The _database context
        /// </summary>
        private readonly IDatabaseContext _databaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigService"/> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        public ConfigService(IDatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string IConfigService.GetValue(ConfigName name)
        {
            var config = this._databaseContext.Configs.FirstOrDefault(x => x.Key.Equals(name.ToString()));
            if (config != null)
            {
                return config.Value;
            }
            return null;
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <param name="configNames">The configuration names.</param>
        /// <returns></returns>
        IDictionary<string, string> IConfigService.GetValues(ConfigName[] configNames)
        {
            var names = configNames.Select(x => x.ToString());
            var configs = this._databaseContext.Configs.Where(x => names.Contains(x.Key));

            return configs.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
