namespace dnCodeFirstExistingDatabaseSamplev1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Resource
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
