namespace dnCodeFirstExistingDatabaseSamplev1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointment
    {
        public int ID { get; set; }

        public int? EventType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? AllDay { get; set; }

        public string Subject { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public int? Status { get; set; }

        public int? Label { get; set; }

        public int? ResourceID { get; set; }

        public string RecurrenceInfo { get; set; }

        public string ReminderInfo { get; set; }

        public string ContactInfo { get; set; }
    }
}
