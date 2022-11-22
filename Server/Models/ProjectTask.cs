namespace Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProjectTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectTask()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        public int ProjectId { get; set; }

        public int DescriptionId { get; set; }

        public int TaskConditionId { get; set; }

        public int ImportanceId { get; set; }

        public int TermId { get; set; }

        public virtual Description Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual Importance Importance { get; set; }

        public virtual Project Project { get; set; }

        public virtual TaskCondition TaskCondition { get; set; }

        public virtual Term Term { get; set; }
    }
}
