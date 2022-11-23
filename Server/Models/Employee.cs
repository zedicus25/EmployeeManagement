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

        public int PersonId { get; set; }

        public int RoleId { get; set; }

        public int? TaskId { get; set; }

        public int LoginDataId { get; set; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [StringLength(110)]
        public string Avatar { get; set; }

        public virtual EmployeesRole EmployeesRole { get; set; }

        public virtual LoginData LoginData { get; set; }

        public virtual Person Person { get; set; }

        public virtual ProjectTask ProjectTask { get; set; }
    }
}
