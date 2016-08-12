using PureMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PureMVC.Controllers
{
    public class StoreController : Controller
    {
        // GET: http://localhost:5913/Store/Details/1
        public ActionResult Details(int id)
        {
            var album = new Album { Title = "Album " + id };
            return View(album);
        }
        // GET: http://localhost:5913/Store/Browse/?Genre=Disco
        //http://localhost:5913/store/browse/?genre=Disco1
        public ActionResult Browse(string genre)
        {
            var genreModel = new Genre { Name = genre };
            return View(genreModel);
        }
        //http://localhost:5913/store
        public ActionResult Index()
        {
            var genres = new List<Genre>
            {
            new Genre { Name = "Disco"},
            new Genre { Name = "Jazz"},
            new Genre { Name = "Rock"}
            };
            return View(genres);
        }
        //

        //// GET: http://localhost:5913/Store
        //public string Index()
        //{
        //    return "Hello from Store.Index()";
        //}

        //// GET: http://localhost:5913/Store/Browse
        //public string Browse()
        //{
        //    return "Hello from Store.Browse()";
        //}
        ////
        //// GET: http://localhost:5913/Store/BrowseParam?genre=Disco
        //public string BrowseParam(string genre)
        //{
        //    string message = HttpUtility.HtmlEncode("Store.Browse, Genre = " + genre);
        //    return message;
        //}
        ////
        //// GET: http://localhost:5913/Store/Details
        //public string Details()
        //{
        //    return "Hello from Store.Details()";
        //}
        ////
        //// GET: http://localhost:5913/Store/DetailsParam/5
        //public string DetailsParam(int id)
        //{
        //    string message = "Store.Details, ID = " + id;
        //    return message;
        //}
    }
}