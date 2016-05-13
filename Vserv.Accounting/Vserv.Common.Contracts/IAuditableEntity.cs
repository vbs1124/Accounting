using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Common.Contracts
{
    public interface IAuditableEntity
    {
        DateTime? CreatedDate { get; set; }

        DateTime? UpdatedDate { get; set; }

        string CreatedBy { get; set; }

        string UpdatedBy { get; set; }
    }
}
