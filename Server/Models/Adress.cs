namespace Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adress
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adress()
        {
            Persons = new HashSet<Person>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(90)]
        public string Country { get; set; }

        [Required]
        [StringLength(90)]
        public string City { get; set; }

        [Required]
        [StringLength(90)]
        public string Street { get; set; }

        [Required]
        [StringLength(10)]
        public string House_Number { get; set; }

        [StringLength(250)]
        public string Full_Adress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> Persons { get; set; }
    }
}
