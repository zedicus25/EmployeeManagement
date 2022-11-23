namespace Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Email
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        [Column("Email")]
        [Required]
        [StringLength(30, MinimumLength = 10)]
        [DataType(DataType.EmailAddress)]
        public string Email1 { get; set; }

        public virtual Person Person { get; set; }
    }
}
