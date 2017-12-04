using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AtlasScrum.DAL;
using AtlasScrum.Models;
using PagedList;

namespace AtlasScrum.Controllers
{
    public class HomeController : Controller
    {
        private ScrumContext db = new ScrumContext();

        public ViewResult Index()
        {
            var sprints = from s in db.Sprints where s.IsCurrent select s;

            sprints = sprints.OrderBy(s => s.SprintName);

            return View(sprints.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}