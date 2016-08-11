using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Vouches;

namespace NAS.BO.Accounting.NSCurrency
{
    public class ExchangeRateBO
    {
        public ExchangeRate GetLatest(DevExpress.Xpo.Session session, Guid currencyId)
        {
            ExchangeRate ret = null;
            try
            {
                //Get currency
                NAS.DAL.Accounting.Currency.Currency currency = 
                    session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(currencyId);
                
                var activeExchangeRates =
                    currency.exchangeRates.Where(r => r.Status == Utility.Constant.STATUS_EXCHANGE_RATE_ACTIVE);
                
                if(activeExchangeRates == null || activeExchangeRates.Count() == 0)
                    return null;

                DateTime latestDate = activeExchangeRates.Max(r => r.AffectedDate);

                ret = activeExchangeRates.FirstOrDefault(r => r.AffectedDate.Equals(latestDate));
                
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
