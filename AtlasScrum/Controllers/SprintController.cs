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
    public class SprintController : Controller
    {
        private ScrumContext db = new ScrumContext();

        // GET: /Sprint/
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? currentFilterProject, int? projectId, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            // Sort Order
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;
            ViewBag.ProjectSortParm = sortOrder == "Project" ? "project_desc" : "Project";
            ViewBag.GoalSortParm = sortOrder == "Goal" ? "Goal_desc" : "Goal";
            ViewBag.FromSortParm = sortOrder == "From" ? "From_desc" : "From";
            ViewBag.ToSortParm = sortOrder == "To" ? "To_desc" : "To";
            ViewBag.IsRunningSortParm = sortOrder == "IsRunning" ? "IsRunning_desc" : "IsRunning";

            // Project Search
            if (projectId != null)
            {
                page = 1;
            }
            else
            {
                projectId = currentFilterProject;
            }

            // Name search
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentFilterProject = projectId;

            var sprints = from s in db.Sprints select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                sprints = sprints.Where(s => s.SprintName.ToLower().Contains(searchString.ToLower()));
            }

            if (projectId > 0)
            {
                sprints = sprints.Where(s => s.ProjectId == projectId);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sprints = sprints.OrderByDescending(s => s.SprintName);
                    break;
                case "Project":
                    sprints = sprints.OrderBy(s => s.Project.ProjectName);
                    break;
                case "project_desc":
                    sprints = sprints.OrderByDescending(s => s.Project.ProjectName);
                    break;
                case "Goal":
                    sprints = sprints.OrderBy(s => s.SprintGoal);
                    break;
                case "Goal_desc":
                    sprints = sprints.OrderByDescending(s => s.SprintGoal);
                    break;
                case "From":
                    sprints = sprints.OrderBy(s => s.FromDate);
                    break;
                case "From_desc":
                    sprints = sprints.OrderByDescending(s => s.FromDate);
                    break;
                case "To":
                    sprints = sprints.OrderBy(s => s.ToDate);
                    break;
                case "To_desc":
                    sprints = sprints.OrderByDescending(s => s.ToDate);
                    break;
                case "IsRunning":
                    sprints = sprints.OrderBy(s => s.IsCurrent);
                    break;
                case "IsRunning_desc":
                    sprints = sprints.OrderByDescending(s => s.IsCurrent);
                    break;
                default:
                    sprints = sprints.OrderBy(s => s.SprintName);
                    break;
            }

            ViewBag.ProjectId = new SelectList(db.Projects.OrderBy(p => p.ProjectName), "ProjectId", "ProjectName", projectId.ToString());

            var pageSize = 5;
            var pageNumber = page ?? 1;

            return View(sprints.ToPagedList(pageNumber, pageSize));

            ////var sprints = db.Sprints.Include(s => s.Project);

            ////return View(sprints.ToList());
        }

        // GET: /Sprint/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        // GET: /Sprint/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");
            return View();
        }

        // POST: /Sprint/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SprintId,SprintName,SprintGoal,ProjectId,FromDate,ToDate,DailyScrum,SprintDemo,IsCurrent")] Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Sprints.Add(sprint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects.OrderBy(p => p.ProjectName), "ProjectId", "ProjectName", sprint.ProjectId);
            return View(sprint);
        }

        // GET: /Sprint/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Sprint sprint = db.Sprints.Find(id);
            
            if (sprint == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProjectId = new SelectList(db.Projects.OrderBy(p => p.ProjectName), "ProjectId", "ProjectName", sprint.ProjectId);
            
            return View(sprint);
        }

        // POST: /Sprint/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SprintId,SprintName,SprintGoal,ProjectId,FromDate,ToDate,DailyScrum,SprintDemo,IsCurrent")] Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprint).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects.OrderBy(p => p.ProjectName), "ProjectId", "ProjectName", sprint.ProjectId);
            
            return View(sprint);
        }

        // GET: /Sprint/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sprint sprint = db.Sprints.Find(id);
            
            if (sprint == null)
            {
                return HttpNotFound();
            }
            
            return View(sprint);
        }

        // POST: /Sprint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sprint sprint = db.Sprints.Find(id);
            
            db.Sprints.Remove(sprint);
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
