using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Helpers
{
    public class ReplyFormat
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string error { get; set; }
        public dynamic data { get; set; }
    }
}
