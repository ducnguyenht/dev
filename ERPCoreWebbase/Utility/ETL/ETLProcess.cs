using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DevExpress.Xpo;

namespace Utility.ETL
{
    public interface IETLProcess
    {
        bool Stop{
            get;set;
        }
        void Extract();
        void Transform();
        void Load();
        void WorkFlow();
        void Run();
    }
    public interface IETLJob
    {
        string JobRegisterCode{
            get;
        }
        List<int> BusinessObjectTypeId
        {
            get;
        }
        bool Stop
        {
            get;
            set;
        }
        void GetJobId(Session session);
        void Extract();
        void Transform();
        void Load();
        void Run();        
    }
}
