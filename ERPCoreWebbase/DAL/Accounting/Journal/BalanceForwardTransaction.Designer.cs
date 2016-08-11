using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.Journal
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class BalanceForwardTransaction : Transaction
    {
        double fBalance;
        public double Balance
        {
            get
            {
                return fBalance;
            }
            set
            {
                SetPropertyValue<double>("Balance", ref fBalance, value);
            }
        }
    }
}
