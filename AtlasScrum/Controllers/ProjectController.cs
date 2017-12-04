using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using AtlasScrum.DAL;
using AtlasScrum.Models;

using PagedList;

namespace AtlasScrum.Controllers
{
    public class ProjectController : Controller
    {
        private ScrumContext db = new ScrumContext();

        // GET: /Project/
        public ActionResult Index(string sortOrder, string currentFilterProject, string searchStringProject, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            // Sort Order
            ViewBag.ProjectSortParm = sortOrder == "project" ? "project_desc" : "project";
            
            // Name search
            if (searchStringProject != null)
            {
                page = 1;
            }
            else
            {
                searchStringProject = currentFilterProject;
            }

            ViewBag.CurrentFilterProject = searchStringProject;
            
            var projects = from s in db.Projects select s;

            if (!string.IsNullOrEmpty(searchStringProject))
            {
                projects = projects.Where(s => s.ProjectName.ToLower().Contains(searchStringProject.ToLower()));
            }

            switch (sortOrder)
            {
                case "project_desc":
                    projects = projects.OrderByDescending(s => s.ProjectName);
                    break;
                default:
                    projects = projects.OrderBy(s => s.ProjectName);
                    break;
            }

            var pageSize = 5;
            var pageNumber = page ?? 1;

            return View(projects.ToPagedList(pageNumber, pageSize));
            
            ////return View(db.Projects.ToList());
        }

        // GET: /Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: /Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ProjectId,ProjectName")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: /Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProjectId,ProjectName")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: /Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
