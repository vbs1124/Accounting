using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity
{
    public class ForgotPassword
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityQuestionAnswer { get; set; }
        public string MobileNumber { get; set; }
        public int SecurityQuestionId { get; set; }
    }
}
