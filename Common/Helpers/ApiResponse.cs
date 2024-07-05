using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string MessageEN { get; set; }
        public bool IsResultList { get; set; }
        public List<T> listado { get; set; }
        public T objeto { get; set; }
        public object dato { get; set; }
    }
}
