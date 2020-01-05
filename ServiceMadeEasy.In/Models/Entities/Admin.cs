using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceMadeEasy.In.Models
{
    public class Admin : ApplicationUser
    {
        public string Name { get; set; }
        public string Pincode { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}