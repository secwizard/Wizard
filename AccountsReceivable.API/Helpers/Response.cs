using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Helpers
{
    public class Response<T>
    {
        public T Data { get; set; }
        public Status Status { get; set; } = new Status();
    }
    public class ResponseList<T>
    {
        public List<T> Data { get; set; }
        public Status Status { get; set; } = new Status();
    }
    public class Status
    {
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "Success";
        public string Response { get; set; }

    }
}
