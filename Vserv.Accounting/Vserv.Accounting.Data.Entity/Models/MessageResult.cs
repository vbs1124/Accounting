using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity.Models
{
    public class MessageResult
    {
        public bool IsErrorOccurred { get; set; }
        public string Message { get; set; }
    }
}
