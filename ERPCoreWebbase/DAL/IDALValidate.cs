using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.DAL
{
    interface IDALValidate
    {
        bool ValidateParameter();
        bool ValidateUnique();
        bool IsExist();
    }
}
