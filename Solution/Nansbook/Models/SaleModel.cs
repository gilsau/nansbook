using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class SaleModel
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
	    public int? ServiceId { get; set; }
	    public int SalePaymentMethodId { get; set; }
        public decimal SaleAmount { get; set; }
        public int? TipPaymentMethodId { get; set; }
        public decimal? TipAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DeductionAmount { get; set; }
    }
}