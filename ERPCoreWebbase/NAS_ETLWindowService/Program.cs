using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;

namespace NAS_ETLService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            try
            {

            #if DEBUG
                        {
                            ETLService service = new ETLService();
                            service.OnDebug();                
                        }
            #else
                        {
                            ServiceBase[] ServicesToRun;
                            ServicesToRun = new ServiceBase[] { new ETLService() };
                            ServiceBase.Run(ServicesToRun);
                        }
            #endif
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
            }
        }
    }
}
