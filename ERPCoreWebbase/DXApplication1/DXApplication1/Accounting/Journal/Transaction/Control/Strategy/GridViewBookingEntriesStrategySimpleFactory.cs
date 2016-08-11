using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Accounting.Journal.Transaction.Control.Strategy
{
    public enum GridViewBookingEntriesStrategyEnum
    {

    }

    public static class GridViewBookingEntriesStrategySimpleFactory
    {
        public static GridViewBookingEntriesStrategy Create(GridViewBookingEntriesStrategyEnum builtInStrategy)
        {
            GridViewBookingEntriesStrategy ret = null;
            return ret;
        }
    }
}