using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class DeductionModel
    {
        public int? Id { get; set; }
        public int UserXStoreId { get; set; }
        public decimal? FixedAmtPerSvcSale { get; set; }
	    public int? PctPerSvcSale { get; set; }
	    public int? PctTotAllSvcSales { get; set; }
	    public decimal? DailyAmt { get; set; }
	    public decimal? DailyCleaningExp { get; set; }
	    public int? DailyCleaningExpWkday { get; set; }
        public int? TipProcessingPct { get; set; }
    }
}