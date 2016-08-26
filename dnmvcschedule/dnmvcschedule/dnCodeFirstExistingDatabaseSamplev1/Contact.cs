namespace dnCodeFirstExistingDatabaseSamplev1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contact
    {
        public int ID { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Address { get; set; }

        [Required]
        [StringLength(16)]
        public string Phone { get; set; }

        [Required]
        [StringLength(16)]
        public string Country { get; set; }

        [Required]
        [StringLength(16)]
        public string City { get; set; }
    }
}
