namespace Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        public int Id { get; set; }

        public int? PersonId { get; set; }

        public int? RoleId { get; set; }

        public int? ProjectId { get; set; }

        public int? LoginDataId { get; set; }

        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        public virtual LoginData LoginData { get; set; }

        public virtual Person Person { get; set; }

        public virtual Project Project { get; set; }

        public virtual EmployeesRole EmployeesRole { get; set; }
    }
}
