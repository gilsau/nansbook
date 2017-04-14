using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class PaycheckModel
    {
        public int? Id { get; set; }
        public decimal Amount { get; set; }
	    public string Comments { get; set; }
	    public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}