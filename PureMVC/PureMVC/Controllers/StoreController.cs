using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PureMVC.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: http://localhost:5913/Store
        public string Index()
        {
            return "Hello from Store.Index()";
        }
        //
        // GET: http://localhost:5913/Store/Browse
        public string Browse()
        {
            return "Hello from Store.Browse()";
        }
        //
        // GET: http://localhost:5913/Store/BrowseParam?genre=Disco
        public string BrowseParam(string genre)
        {
            string message = HttpUtility.HtmlEncode("Store.Browse, Genre = " + genre);
            return message;
        }
        //
        // GET: http://localhost:5913/Store/Details
        public string Details()
        {
            return "Hello from Store.Details()";
        }
        //
        // GET: http://localhost:5913/Store/DetailsParam/5
        public string DetailsParam(int id)
        {
            string message = "Store.Details, ID = " + id;
            return message;
        }
    }
}