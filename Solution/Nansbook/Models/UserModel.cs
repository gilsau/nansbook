using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nansbook.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string IdsForDelete { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public HttpPostedFileBase PhotoFileName { get; set; }
        public string SocialSecurity { get; set; }
        public string Role { get; set; }
    }
}