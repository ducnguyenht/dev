using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.DAL.Interface
{
    interface IDALValidate
    {
        //void AddRule();
        bool ValidateParameter();
        bool ValidateUnique();
        bool IsExist();
    }
}
