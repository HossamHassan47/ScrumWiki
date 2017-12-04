using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasScrum.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TeamMember
    {
        public int TeamMemberId { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        
        public string Description { get; set; }
    }
}