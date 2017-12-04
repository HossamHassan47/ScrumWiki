using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasScrum.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Sprint
    {
        public int SprintId { get; set; }

        [Display(Name = "Sprint Name")]
        public string SprintName { get; set; }

        [Display(Name = "Sprint Goal")]
        public string SprintGoal { get; set; }

        public int ProjectId { get; set; }

        [Display(Name = "From")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Display(Name = "To")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [Display(Name = "Daily Scrum")]
        public string DailyScrum { get; set; }

        [Display(Name = "Sprint Demo")]
        public string SprintDemo { get; set; }

        [Display(Name = "Is Running")]
        public bool IsCurrent { get; set; }

        public virtual Project Project { get; set; }

        [Display(Name = "Backlog Items")]
        public virtual ICollection<BacklogItem> BacklogItems { get; set; }

        [Display(Name = "Team Members")]
        public virtual ICollection<SprintTeamMember> SprintTeamMembers { get; set; } 
    }
}