namespace Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Phone_Numbers
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone_Number { get; set; }

        public virtual Person Person { get; set; }
    }
}
