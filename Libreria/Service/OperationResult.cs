using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.Service
{
    public class OperationResult
    {
        public bool IsSuccessful { get; set; }

        public Exception exception { get; set; }
    }


}