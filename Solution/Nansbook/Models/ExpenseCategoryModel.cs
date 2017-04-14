using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class ExpenseCategoryModel
    {
        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}