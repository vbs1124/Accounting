using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Common.Enums
{
    /// <summary>
    /// 5 % of CTC paid Annually or (25% of increment in June and 25% in December)
    /// </summary>
    public enum PerformanceIncentive
    {
        None = 0,
        FivePercentPadiAnnually = 1,
        TwentyFivePercentHalfYearly = 2
    }
}
