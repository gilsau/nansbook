using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class ExpenseModel
    {
        public int? Id { get; set; }
        public int ExpenseCategoryId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}