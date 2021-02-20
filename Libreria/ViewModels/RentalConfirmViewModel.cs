using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class RentalConfirmViewModel
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime PickStartDate { get; set; }
        public DateTime PickEndDate { get; set; }

    }
}