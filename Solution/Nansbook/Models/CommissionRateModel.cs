using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class CommissionRateModel
    {
        public int? Id { get; set; }
        public int UserXStoreId { get; set; }
        public int StoreCommPct { get; set; }
	    public int StoreCashPct { get; set; }
    }
}