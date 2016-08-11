using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPPopulate.Nomenclature.Item
{

    public class UnitEntityComparer : IEqualityComparer<UnitEntity>
    {
        public bool Equals(UnitEntity x, UnitEntity y)
        {

            //Check whether the compared objects reference the same data. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null. 
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            if (x.Code != null && y.Code != null)
            {
                return x.Code.ToLower().Equals(y.Code.ToLower());
            }
            else if (x.Code == null && y.Code == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public int GetHashCode(UnitEntity obj)
        {
            //Check whether the object is null 
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null. 
            int hashProductName = obj.Name == null ? 0 : obj.Name.GetHashCode();

            //Get hash code for the Code field. 
            int hashProductCode = obj.Code == null ? 0 : obj.Code.GetHashCode();

            //Calculate the hash code for the product. 
            return hashProductName ^ hashProductCode;
        }
    }

    public class UnitEntity
    {
        public string Code
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
    }
}
