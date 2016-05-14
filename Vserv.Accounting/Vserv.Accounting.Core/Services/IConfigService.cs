using System.Collections.Generic;

namespace Vserv.Accounting.Core.Services
{
    public interface IConfigService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetValue(ConfigName name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configNames"></param>
        /// <returns></returns>
        IDictionary<string, string> GetValues(ConfigName[] configNames);
    }
}
