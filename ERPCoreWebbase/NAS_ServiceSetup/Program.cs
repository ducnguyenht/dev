using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NAS_ServiceSetup
{
    class Program
    {
        static void Main(string[] args)
        {            
            try
            {
                if (args.Length == 1)
                {
                    switch (args[0].ToLower())
                    {
                        case "-h":
                            {
                                Console.WriteLine("Description:");
                                Console.WriteLine("      NAS_ServiceSetup is command line to install ETL service\n");
                                Console.WriteLine("Usage:");
                                Console.WriteLine("      -i: Install");
                                Console.WriteLine("      -u: UnInstall");
                                Console.WriteLine("      -h: Help\n");
                                break;
                            }
                        case "-i":
                            {
                                string servicepath = System.IO.Directory.GetCurrentDirectory().Replace("\\", "/") + "/";
                                servicepath = Application.StartupPath + "\\";
                                System.Diagnostics.Process process = new System.Diagnostics.Process();
                                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                startInfo.FileName = "cmd.exe";
                                startInfo.Arguments = "/C sc create \"NAS_Service\" binPath= \"" + servicepath + "NAS_ETLService.exe\" start= auto&sc start \"NAS_Service\"";
                                process.StartInfo = startInfo;
                                process.Start();
                                //Console.WriteLine(servicepath);
                                Console.WriteLine("Install Service Completed!!!");
                                break;
                            }
                        case "-u":
                            {
                                System.Diagnostics.Process process = new System.Diagnostics.Process();
                                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                startInfo.FileName = "cmd.exe";
                                startInfo.Arguments = "/C sc stop \"NAS_Service\"&sc delete \"NAS_Service\"";
                                process.StartInfo = startInfo;
                                process.Start();
                                Console.WriteLine("Uninstall Service Completed!!!");
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Error:");
                                Console.WriteLine("      Error argument\n");
                                Console.WriteLine("Description:");
                                Console.WriteLine("      NAS_ServiceSetup is command line to install ETL service\n");
                                Console.WriteLine("Usage:");
                                Console.WriteLine("      -i: Install");
                                Console.WriteLine("      -u: UnInstall");
                                Console.WriteLine("      -h: Help\n");
                                break;
                            }
                    }
                }
                else
                {
                    Console.WriteLine("Error:");
                    Console.WriteLine("      Error argument\n");
                    Console.WriteLine("Description:");
                    Console.WriteLine("      NAS_ServiceSetup is command line to install ETL service\n");
                    Console.WriteLine("Usage:");
                    Console.WriteLine("      -i: Install");
                    Console.WriteLine("      -u: UnInstall");
                    Console.WriteLine("      -h: Help\n");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error:");
                Console.WriteLine("      Error argument\n");
                Console.WriteLine("Description:");
                Console.WriteLine("      NAS_ServiceSetup is command line to install ETL service\n");
                Console.WriteLine("Usage:");
                Console.WriteLine("      -i: Install");
                Console.WriteLine("      -u: UnInstall");
                Console.WriteLine("      -h: Help\n");
            }
            finally
            {                
            }
        }
    }
}
