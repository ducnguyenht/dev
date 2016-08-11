using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.BO.ETL
{
    public class ETLDBHelper
    {
        //public static void Populate(Session dbsession)
        //{
        //    try
        //    {
        //        Session session = dbsession;
        //        ETLJobDetail.Populate();
        //        BusinessObject.Populate();                
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Populate failed");
        //    }
        //    finally
        //    {

        //    }
        //}

        //public static bool IsExistXpoObject<T>(Session dbSession, string fieldName, object value, params short[] rowStatuses)
        //{
        //    return IsExistXpoObject<T>(dbSession, fieldName, value, BinaryOperatorType.Equal, rowStatuses);
        //}
        //public static bool IsExistXpoObject<T>(Session dbSession, string fieldName, object value, BinaryOperatorType type, params short[] rowStatuses)
        //{
        //    Session session = null;
        //    try
        //    {
        //        CriteriaOperator rowStatusCriteria = null;
        //        if (rowStatuses.Length > 0)
        //        {
        //            rowStatusCriteria = new InOperator("RowStatus", rowStatuses.ToList());
        //        }
        //        CriteriaOperator fieldCriteria = new BinaryOperator(fieldName, value, type);
        //        CriteriaOperator criteria = CriteriaOperator.And(fieldCriteria, rowStatusCriteria);
        //        session = dbSession;
        //        var result = session.GetObjects(session.GetClassInfo(typeof(T)), criteria, null, 0, false, true);

        //        if (result != null && result.Count > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (session != null) session.Dispose();
        //    }
        //    return false;
        //}
    }
}
