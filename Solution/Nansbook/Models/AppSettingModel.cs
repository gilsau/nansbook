using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class AppSettingModel
    {
        public int? Id { get; set; }
        public bool NotifyByEmailForInvites { get; set; }
	    public bool NotifyByEmailForPaychecks { get; set; }
	    public bool NotifyInAppForInvites { get; set; }
        public bool NotifyInAppForPaychecks { get; set; }
    }
}