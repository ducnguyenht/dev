using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler.Internal;

namespace Utility
{
    [Serializable]
    public class CustomEvent
    {
        object id;
        DateTime start;
        DateTime end;
        string subject;
        int status;
        string description;
        long label;
        string location;
        bool allday;
        int eventType;
        string recurrenceInfo;
        string reminderInfo;
        object ownerId;
        double price;
        string contactInfo;

        public CustomEvent()
        {
        }

        public DateTime StartTime { get { return start; } set { start = value; } }
        public DateTime EndTime { get { return end; } set { end = value; } }
        public string Subject { get { return subject; } set { subject = value; } }
        public int Status { get { return status; } set { status = value; } }
        public string Description { get { return description; } set { description = value; } }
        public long Label { get { return label; } set { label = value; } }
        public string Location { get { return location; } set { location = value; } }
        public bool AllDay { get { return allday; } set { allday = value; } }
        public int EventType { get { return eventType; } set { eventType = value; } }
        public string RecurrenceInfo { get { return recurrenceInfo; } set { recurrenceInfo = value; } }
        public string ReminderInfo { get { return reminderInfo; } set { reminderInfo = value; } }
        public object OwnerId { get { return ownerId; } set { ownerId = value; } }
        public object Id { get { return id; } set { id = value; } }
        public double Price { get { return price; } set { price = value; } }
        public string ContactInfo { get { return contactInfo; } set { contactInfo = value; } }
    }

    [Serializable]
    public class CustomEventList : BindingList<CustomEvent>
    {
        public void AddRange(CustomEventList events)
        {
            foreach (CustomEvent customEvent in events)
                this.Add(customEvent);
        }
        public int GetEventIndex(object eventId)
        {
            for (int i = 0; i < Count; i++)
                if (this[i].Id == eventId)
                    return i;
            return -1;
        }
    }

    public class CustomEventDataSource
    {
        CustomEventList events;
        public CustomEventDataSource(CustomEventList events)
        {
            if (events == null)
                //DevExpress.XtraScheduler.Native.Exceptions.ThrowArgumentNullException("events");
            this.events = events;
        }
        public CustomEventDataSource() : this(new CustomEventList())
        {
        }

        public CustomEventList Events { get { return events; } set { events = value; } }

        public void InsertMethodHandler(CustomEvent customEvent)
        {
            Events.Add(customEvent);
        }

        public void DeleteMethodHandler(CustomEvent customEvent)
        {
            int eventIndex = Events.GetEventIndex(customEvent.Id);
            if (eventIndex >= 0)
                Events.RemoveAt(eventIndex);
        }

        public void UpdateMethodHandler(CustomEvent customEvent)
        {
            int eventIndex = Events.GetEventIndex(customEvent.Id);
            if (eventIndex >= 0)
            {
                Events.RemoveAt(eventIndex);
                Events.Insert(eventIndex, customEvent);
            }
        }

        public IEnumerable SelectMethodHandler()
        {
            CustomEventList result = new CustomEventList();
            result.AddRange(Events);
            return result;
        }
    }
}