namespace dnSchedulev01.EFCFFDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ScheduleCalendar")]
    public partial class ScheduleCalendar
    {
        public int ID { get; set; }      
        public int? UserId { get; set; }
        public int? Status { get; set; }
        [StringLength(50)]
        public string Subject { get; set; }
        public string Description { get; set; }
        public int? Label { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [StringLength(50)]
        public string Location { get; set; }
        public bool AllDay { get; set; }
        public int? EventType { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }

        #region dn Custom Field
        public int? OpportunityId { get; set; }
        public int? CustomerId { get; set; }
        public int? ScheduleTypeId { get; set; }
        public string RequestBy { get; set; }
        public DateTime? RequestedDate { get; set; }
        
        [Column(TypeName = "smallmoney")]
        public decimal? Price { get; set; }
        public string ContactInfo { get; set; }
        
        #endregion
        
    }
}
