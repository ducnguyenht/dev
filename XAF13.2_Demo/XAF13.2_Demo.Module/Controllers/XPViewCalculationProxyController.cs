using System;
using System.Collections.Generic;
using System.Diagnostics;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using XAF13_2_Demo.Module.BusinessObjects;

namespace XAF13_2_Demo.Module.Controllers
{
    public class XPViewCalculationProxyController : ViewController<DetailView>
    {
        public XPViewCalculationProxyController()
        {
            TargetObjectType = typeof(XPViewCalculationProxy);

            Activated += XPViewCalculationProxyController_Activated;
        }

        void XPViewCalculationProxyController_Activated(object sender, EventArgs e)
        {
            if (View.CurrentObject is XPViewCalculationProxy)
            {
                var obj = View.CurrentObject as XPViewCalculationProxy;
                var sw = new Stopwatch();

                sw.Start();

                obj.SumOfIntProperty = CalculateSum(obj);

                sw.Stop();

                obj.MillisecondsForCalculation = sw.ElapsedMilliseconds;
            }
        }

        private int CalculateSum(XPViewCalculationProxy obj)
        {
            switch (obj.SumMode)
            {
                case SumMode.PureClientSide:
                    return CalculateSumPureClientMode();

                case SumMode.ClientSide:
                    return CalculateSumClientMode();

                case SumMode.ServerSide:
                    return CalculateSumServerMode();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int CalculateSumPureClientMode()
        {
            using (var os = Application.CreateObjectSpace())
            {
                var objects = os.GetObjects<LargeBusinessObject>(null);

                var result = 0;

                foreach (var item in objects)
                    result += item.IntPropertyToCalculate;

                return result;    
            }
        }

        private int CalculateSumClientMode()
        {
            using (var os = Application.CreateObjectSpace())
            {
                var objects = os.CreateDataView(typeof(LargeBusinessObject), new List<DataViewExpression>
                {
                    new DataViewExpression("IntPropertyToCalculate", new OperandProperty("IntPropertyToCalculate"))
                }, null, new List<SortProperty>());

                var result = 0;

                foreach (ViewRecord item in objects)
                    result += (int)item["IntPropertyToCalculate"];

                return result;
            }
        }

        private int CalculateSumServerMode()
        {
            using (var os = Application.CreateObjectSpace())
            {
                var objects = os.CreateDataView(typeof(LargeBusinessObject), new List<DataViewExpression>
                {
                    new DataViewExpression("CalculatedServerSide", CriteriaOperator.Parse("Sum(IntPropertyToCalculate)"))
                }, null, new List<SortProperty>());

                var result = 0;

                foreach (ViewRecord item in objects)
                    result += (int)item["CalculatedServerSide"];

                return result;
            }
        }
    }
}
