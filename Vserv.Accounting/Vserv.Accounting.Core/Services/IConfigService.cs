using System.Collections.Generic;

namespace Vserv.Accounting.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string GetValue(ConfigName name);

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <param name="configNames">The configuration names.</param>
        /// <returns></returns>
        IDictionary<string, string> GetValues(ConfigName[] configNames);
    }
}
