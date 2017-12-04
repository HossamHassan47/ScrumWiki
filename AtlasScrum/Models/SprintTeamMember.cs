using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasScrum.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SprintTeamMember
    {
        public int SprintTeamMemberId { get; set; }

        public int SprintId { get; set; }

        public int TeamMemberId { get; set; }

        public int RoleId { get; set; }

        [Display(Name = "Man of Sprint")]
        public bool IsManOfSprint { get; set; }

        public virtual Sprint Sprint { get; set; }

        public virtual TeamMember TeamMember { get; set; }

        public virtual Role Role { get; set; }
    }
}