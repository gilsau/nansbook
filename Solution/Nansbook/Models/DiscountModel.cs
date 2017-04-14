using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class DiscountModel
    {
        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public int? Percentage { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}