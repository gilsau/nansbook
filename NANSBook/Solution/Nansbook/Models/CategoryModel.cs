using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string IdsForDelete { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }

    public enum CategoryTypes
    {
        Expense = 1,
        Service = 2,
        PaymentMethod = 3,
        Discount = 4,
        Sales = 5,
        Product = 6
    }
}