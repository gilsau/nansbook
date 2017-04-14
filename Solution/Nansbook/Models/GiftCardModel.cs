using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class GiftCardModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
	    public decimal CreditAmount { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}