using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxEditors;

namespace MVCLargeDB01.DAL
{
    public static class LargeDataBaseDataProvider {
        const string LargeDatabaseDataContextKey = "DXLargeDatabaseDataContext";
        public static MyDbContext DB {
            get {
                if(HttpContext.Current.Items[LargeDatabaseDataContextKey] == null)
                    HttpContext.Current.Items[LargeDatabaseDataContextKey] = new MyDbContext();
                return (MyDbContext)HttpContext.Current.Items[LargeDatabaseDataContextKey];
            }
        }

        public static object GetAllCountries() {
            var countries = from country in DB.Countries
                            orderby country.Description
                            select country;
            return countries.ToList();
        }


        public static object GetCountriesRange(ListEditItemsRequestedByFilterConditionEventArgs args) {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            //return (from country in DB.Countries
            //        where country.Code.StartsWith(args.Filter)
            //        select country).Skip(skip).Take(take) ;

            return DB.Countries.Where(c => c.Code.StartsWith(args.Filter)).OrderBy(c => c.Code).Skip(skip).Take(take).ToList();

        }

        public static object GetCountriesByID(ListEditItemRequestedByValueEventArgs args) {
            if(args.Value != null) {
                int id = (int)args.Value;
                //return (from country in DB.Countries
                //        where country.CountryID == id
                //        select country).SingleOrDefault();
                return DB.Countries.Where(c => c.CountryID == id).FirstOrDefault();
            }
            return null;
        }

    }
}