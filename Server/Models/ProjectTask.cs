namespace Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProjectTask
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        public int? ProjectId { get; set; }

        public int? DescriptionId { get; set; }

        public int? TaskConditionId { get; set; }

        public int? ImportanceId { get; set; }

        public int? TermId { get; set; }

        public virtual Importance Importance { get; set; }

        public virtual Project Project { get; set; }

        public virtual ProjectTaskDescription ProjectTaskDescription { get; set; }

        public virtual TaskCondition TaskCondition { get; set; }

        public virtual Term Term { get; set; }
    }
}
