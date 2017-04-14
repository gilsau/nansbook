using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class SalaryModel
    {
        public int? Id { get; set; }
        public int UserXStoreId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}