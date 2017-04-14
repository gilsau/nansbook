//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nansbook
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Categories = new HashSet<Category>();
            this.Categories1 = new HashSet<Category>();
            this.SalesEntries = new HashSet<SalesEntry>();
            this.SalesEntries1 = new HashSet<SalesEntry>();
            this.SalesEntries2 = new HashSet<SalesEntry>();
            this.webpages_Roles = new HashSet<webpages_Roles>();
        }
    
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public Nullable<int> State { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string PhotoFileName { get; set; }
        public string SocialSecurity { get; set; }
        public Nullable<int> SecQuestion1 { get; set; }
        public Nullable<int> SecQuestion2 { get; set; }
        public Nullable<int> SecQuestion3 { get; set; }
        public string SecAnswer1 { get; set; }
        public string SecAnswer2 { get; set; }
        public string SecAnswer3 { get; set; }
    
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Category> Categories1 { get; set; }
        public virtual ICollection<SalesEntry> SalesEntries { get; set; }
        public virtual ICollection<SalesEntry> SalesEntries1 { get; set; }
        public virtual ICollection<SalesEntry> SalesEntries2 { get; set; }
        public virtual SecurityQuestion SecurityQuestion { get; set; }
        public virtual SecurityQuestion SecurityQuestion1 { get; set; }
        public virtual SecurityQuestion SecurityQuestion2 { get; set; }
        public virtual State State1 { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}
