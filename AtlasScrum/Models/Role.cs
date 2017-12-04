using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasScrum.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Role
    {
        public int RoleId { get; set; }
        
        [Display(Name = "Role Name")]
        public string Description { get; set; }
    }
}