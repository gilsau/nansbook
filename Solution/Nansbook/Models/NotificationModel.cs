using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class NotificationModel
    {
        public int? Id { get; set; }
        public string Message { get; set; }
	    public string Link { get; set; }
	    public string CreatedBy { get; set; }
	    public DateTime CreatedDate { get; set; }
    }
}