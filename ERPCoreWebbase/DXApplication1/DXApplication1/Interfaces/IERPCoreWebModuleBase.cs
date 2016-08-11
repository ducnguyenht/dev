using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebModule.Interfaces
{
    public interface IERPCoreWebModuleBase
    {
        string AccessObjectId { get; }
        string AccessObjectGroupId { get; }
    }
}
