namespace dnCodeFirstExistingDatabaseSamplev1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(32)]
        public string From { get; set; }

        [Required]
        [StringLength(128)]
        public string Subject { get; set; }

        [Required]
        public string Text { get; set; }

        public bool HasAttachment { get; set; }

        [StringLength(16)]
        public string Folder { get; set; }

        public bool IsReply { get; set; }
    }
}
