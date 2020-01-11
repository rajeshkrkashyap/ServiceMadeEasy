using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceMadeEasy.Com.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Updated { get; set; }
        public bool IsActive { get; set; }
    }
}