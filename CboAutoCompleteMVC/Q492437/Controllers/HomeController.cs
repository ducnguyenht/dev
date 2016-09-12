using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Q492437.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewData["List"] = DataHelper.Persons;
            return View();
        }
        public ActionResult ComboBoxPartial() {
            ViewData["List"] = DataHelper.Persons;
            return PartialView();
        }
    }

    public static class DataHelper {
        public static IQueryable<Person> Persons {
            get {
                if(HttpContext.Current.Session["Persons"] == null) {
                    List<Person> list = new List<Person>();

                    list.Add(new Person(1, "David", "Jordan", "Adler"));
                    list.Add(new Person(2, "Michael", "Christopher", "Alcamo"));
                    list.Add(new Person(3, "Amy", "Gabrielle", "Altmann"));
                    list.Add(new Person(4, "Meredith", "", "Berman"));
                    list.Add(new Person(5, "Margot", "Sydney", "Atlas"));
                    list.Add(new Person(6, "Eric", "Zachary", "Berkowitz"));
                    list.Add(new Person(7, "Kyle", "", "Bernardo"));
                    list.Add(new Person(8, "Liz", "", "Bice"));

                    HttpContext.Current.Session["Persons"] = list.AsQueryable<Person>();
                }
                return (IQueryable<Person>)HttpContext.Current.Session["Persons"];
            }
        }
    }

    public class Person {
        public Person() {
            PersonID = 0;
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
        }

        public Person(int id, String firstName, string middleName, String lastName) {
            this.PersonID = id;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
        }

        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}