using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Web.ASPxGridView;
using System.Xml.Linq;
using System.Threading;
using System.IO;
using System.Web;
using DevExpress.Web.ASPxClasses.Internal;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections;
using System.Reflection;
using System.Web.Security;

namespace Utility
{

    /// <summary>
    /// A Logging class implementing the Singleton pattern and an internal Queue to be flushed perdiodically
    /// </summary>
    public class LogWriter
    {
        private static LogWriter instance;
        private static Queue<Log> logQueue;
        private static string logDir = HttpContext.Current.Server.MapPath("~/");
        private static string logFile = "log.txt";
        private static int maxLogAge = int.Parse("10");
        private static int queueSize = int.Parse("100");
        private static DateTime LastFlushed = DateTime.Now;

        /// <summary>
        /// Private constructor to prevent instance creation
        /// </summary>
        private LogWriter() { }

        /// <summary>
        /// An LogWriter instance that exposes a single instance
        /// </summary>
        public static LogWriter Instance
        {
            get
            {
                // If the instance is null then create one and init the Queue
                if (instance == null)
                {
                    instance = new LogWriter();
                    logQueue = new Queue<Log>();
                }
                return instance;
            }
        }

        /// <summary>
        /// The single instance method that writes to the log file
        /// </summary>
        /// <param name="message">The message to write to the log</param>
        public void WriteToLog(string message)
        {
            // Lock the queue while writing to prevent contention for the log file
            lock (logQueue)
            {
                // Create the entry and push to the Queue
                Log logEntry = new Log(message);
                logQueue.Enqueue(logEntry);

                // If we have reached the Queue Size then flush the Queue
                if (logQueue.Count >= queueSize || DoPeriodicFlush())
                {
                    FlushLog();
                }
            }            
        }

        private bool DoPeriodicFlush()
        {
            TimeSpan logAge = DateTime.Now - LastFlushed;
            if (logAge.TotalSeconds >= maxLogAge)
            {
                LastFlushed = DateTime.Now;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Flushes the Queue to the physical log file
        /// </summary>
        private void FlushLog()
        {
            while (logQueue.Count > 0)
            {
                Log entry = logQueue.Dequeue();
                string logPath = logDir + entry.LogDate + "_" + logFile;

        // This could be optimised to prevent opening and closing the file for each write
                using (FileStream fs = File.Open(logPath, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter log = new StreamWriter(fs))
                    {
                        log.WriteLine(string.Format("{0}\t{1}",entry.LogTime,entry.Message));
                    }
                }
            }            
        }
    }

    /// <summary>
    /// A Log class to store the message and the Date and Time the log entry was created
    /// </summary>
    public class Log
    {
        public string Message { get; set; }
        public string LogTime { get; set; }
        public string LogDate { get; set; }

        public Log(string message)
        {
            Message = message;
            LogDate = DateTime.Now.ToString("yyyy-MM-dd");
            LogTime = DateTime.Now.ToString("hh:mm:ss.fff tt");
        }
    }

    public static class Helpers
    {
        public static void AddErrorToGridViewColumn(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        public static void AddErrorToTreeListNode(Dictionary<string,string> errors, string column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

        public static string HashStringMD5(string inputString)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(inputString, Constant.HASH_ALORITHM_MD5);
        }

    }

    public static class Utils
    {
        public const string ThomasEmail = "thomas.hardy@example.com";
        static bool? _isSiteMode;
        static List<NavigationItem> _navigationItems;
        static object lockObject = new object();
        static Thread backgroundThread;

        static HttpContext Context { get { return HttpContext.Current; } }
        static string UploadImagesFolder { get { return Context.Server.MapPath("~/Content/Photo/UploadImages/"); } }

        public static bool IsIE7 { get { return RenderUtils.Browser.IsIE && RenderUtils.Browser.Version < 8; } }
        public static bool IsSiteMode
        {
            get
            {
                if (!_isSiteMode.HasValue)
                    _isSiteMode = ConfigurationManager.AppSettings["SiteMode"].Equals("true", StringComparison.InvariantCultureIgnoreCase);
                return _isSiteMode.Value;
            }
        }

        public static void ApplyTheme(Page page)
        {
            var themeName = CurrentTheme;
            if (string.IsNullOrEmpty(themeName))
                themeName = "Default";
            page.Theme = themeName;
        }

        public static string CurrentTheme
        {
            get
            {
                var themeCookie = Context.Request.Cookies["ERPCoreCurrentTheme"];
                return themeCookie == null ? "MetropolisBlue" : HttpUtility.UrlDecode(themeCookie.Value);
            }
        }

        public static bool IsDarkTheme
        {
            get
            {
                var theme = CurrentTheme;
                return theme == "Office2010Black" || theme == "PlasticBlue" || theme == "RedWine" || theme == "BlackGlass";
            }
        }

        //public static string CurrentPageName
        //{
        //    get
        //    {
        //        var key = "CE1167E3-A068-4E7C-8BFD-4A7D308BEF43";
        //        if (Context.Items[key] == null)
        //            Context.Items[key] = GetCurrentPageName(null);
        //        return Context.Items[key].ToString();
        //    }
        //}

        public static List<NavigationItem> NavigationItems
        {
            get
            {
                if (_navigationItems == null)
                {
                    _navigationItems = new List<NavigationItem>();
                    PopuplateNavigationItems(_navigationItems);
                }
                return _navigationItems;
            }
        }

        public static void StartClearExpiredFilesBackgroundThread()
        {
            lock (lockObject)
            {
                if (backgroundThread == null)
                    backgroundThread = new Thread(RemoveTempFilesWorker);
                if (!backgroundThread.IsAlive)
                    backgroundThread.Start(UploadImagesFolder);
            }
        }

        static void RemoveTempFilesWorker(object startParam)
        {
            if (startParam == null)
                return;
            var directory = startParam.ToString();
            while (true)
            {
                Thread.Sleep(60000);
                RemoveExpiredTempFiles(directory);
            }
        }

        static void RemoveExpiredTempFiles(string directory)
        {
            var expirationTime = DateTime.UtcNow - new TimeSpan(0, 15, 0);
            try
            {
                foreach (var file in new DirectoryInfo(directory).GetFiles("*"))
                {
                    if (file.CreationTimeUtc < expirationTime)
                        try
                        {
                            file.Delete();
                        }
                        catch { }
                }
            }
            catch { }
        }

        //static string GetCurrentPageName(Page page)
        //{
        //    var fileName = Path.GetFileName(Context.Request.Path);
        //    var result = fileName.Substring(0, fileName.Length - 5);
        //    if (result.ToLower() == "default")
        //        result = "mail";
        //    if (result.ToLower().Contains("print"))
        //        result = "print";
        //    return result.ToLower();
        //}

        static void PopuplateNavigationItems(List<NavigationItem> list)
        {
            var path = Utils.Context.Server.MapPath("~/App_Data/Navigation.xml");
            list.AddRange(XDocument.Load(path).Descendants("Item").Select(n => new NavigationItem()
            {
                AccessObjectGroupId = n.Attribute("AccessObjectGroupId").Value,
                Text = n.Attribute("Text").Value,
                NavigationUrl = n.Attribute("NavigateUrl").Value,
                SpriteClassName = n.Attribute("SpriteClassName").Value
            }));
        }
    }

    public class NavigationItem
    {
        public string AccessObjectGroupId { get; set; }
        public string Text { get; set; }
        public string NavigationUrl { get; set; }
        public string SpriteClassName { get; set; }
    }

    public static class MasterHelper
    {
        static readonly Type _masterType = typeof(MasterPage);
        static readonly PropertyInfo _contentTemplatesProp = _masterType.GetProperty("ContentTemplates", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool HasContentOrControls(ContentPlaceHolder cph)
        {
            return HasNonEmptyControls(cph) || HasContentPageContent(cph);
        }

        public static bool HasContentPageContent(ContentPlaceHolder cph)
        {
            IDictionary templates = null;
            MasterPage master = cph.Page.Master;

            while (templates == null && master != null)
            {
                templates = (IDictionary)_contentTemplatesProp.GetValue(master, null);
                master = master.Master;
            }

            if (templates == null)
                return false;

            bool isSpecified = false;

            foreach (string key in templates.Keys)
            {
                if (key == cph.ID)
                {
                    isSpecified = true;

                    break;
                }
            }

            return isSpecified;
        }

        public static bool HasNonEmptyControls(ContentPlaceHolder cph)
        {
            if (cph.Controls.Count == 0)
            {
                return false;
            }
            else if (cph.Controls.Count == 1)
            {
                LiteralControl c = cph.Controls[0] as LiteralControl;

                if (string.IsNullOrEmpty(c.Text) || IsWhiteSpace(c.Text))
                    return false;
            }

            return true;
        }

        static bool IsWhiteSpace(string s)
        {
            for (int i = 0; i < s.Length; i++)
                if (!char.IsWhiteSpace(s[i]))
                    return false;

            return true;
        }
    }

}
