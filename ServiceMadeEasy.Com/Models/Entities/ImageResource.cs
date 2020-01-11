using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceMadeEasy.Com.Models
{
    public partial class ImageResource
    {
        [Key] //this line
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsActive { get; set; }
    }
}