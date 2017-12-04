using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasScrum.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BacklogItem
    {
        public int BacklogItemId { get; set; }

        [Display(Name = "Item Description")]
        public string Description { get; set; }

        public decimal Estimate { get; set; }

        [Display(Name = "Sprint")]
        public int SprintId { get; set; }

        public virtual Sprint Sprint { get; set; }
    }
}