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
    public class BacklogItemController : Controller
    {
        private ScrumContext db = new ScrumContext();

        // GET: /BacklogItem/
        public ActionResult Index(string sortOrder, string currentFilterDescription, string searchStringDescription, int? currentFilterSprint, int? sprintId, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            // Sort Order
            ViewBag.DescriptionSortParm = sortOrder == "description" ? "description_desc" : "description";
            ViewBag.SprintSortParm = sortOrder == "sprint" ? "sprint_desc" : "sprint";
            ViewBag.EstimateSortParm = sortOrder == "estimate" ? "estimate_desc" : "estimate";
           
            // Sprint Search
            if (sprintId != null)
            {
                page = 1;
            }
            else
            {
                sprintId = currentFilterSprint;
            }

            // Name search
            if (searchStringDescription != null)
            {
                page = 1;
            }
            else
            {
                searchStringDescription = currentFilterDescription;
            }

            ViewBag.CurrentFilterDescription = searchStringDescription;
            ViewBag.CurrentFilterSprint = sprintId;

            var backlogItems = from s in db.BacklogItems select s;

            if (!string.IsNullOrEmpty(searchStringDescription))
            {
                backlogItems = backlogItems.Where(s => s.Description.ToLower().Contains(searchStringDescription.ToLower()));
            }

            if (sprintId > 0)
            {
                backlogItems = backlogItems.Where(s => s.SprintId == sprintId);
            }

            switch (sortOrder)
            {
                case "description_desc":
                    backlogItems = backlogItems.OrderByDescending(s => s.Description);
                    break;
                case "sprint":
                    backlogItems = backlogItems.OrderBy(s => s.Sprint.SprintName);
                    break;
                case "sprint_desc":
                    backlogItems = backlogItems.OrderByDescending(s => s.Sprint.SprintName);
                    break;
                case "estimate":
                    backlogItems = backlogItems.OrderBy(s => s.Estimate);
                    break;
                case "estimate_desc":
                    backlogItems = backlogItems.OrderByDescending(s => s.Estimate);
                    break;
                default:
                    backlogItems = backlogItems.OrderBy(s => s.Description);
                    break;
            }

            ViewBag.SprintId = new SelectList(db.Sprints.OrderBy(s => s.SprintName), "SprintId", "SprintName", sprintId.ToString());

            var pageSize = 5;
            var pageNumber = page ?? 1;

            return View(backlogItems.ToPagedList(pageNumber, pageSize));
            //var backlogitems = db.BacklogItems.Include(b => b.Sprint);
            //return View(backlogitems.ToList());
        }

        // GET: /BacklogItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BacklogItem backlogitem = db.BacklogItems.Find(id);
            if (backlogitem == null)
            {
                return HttpNotFound();
            }
            return View(backlogitem);
        }

        // GET: /BacklogItem/Create
        public ActionResult Create()
        {
            ViewBag.SprintId = new SelectList(db.Sprints.OrderBy(s => s.SprintName), "SprintId", "SprintName");
            return View();
        }

        // POST: /BacklogItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BacklogItemId,Description,Estimate,SprintId")] BacklogItem backlogitem)
        {
            if (ModelState.IsValid)
            {
                db.BacklogItems.Add(backlogitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SprintId = new SelectList(db.Sprints.OrderBy(s => s.SprintName), "SprintId", "SprintName", backlogitem.SprintId);
            return View(backlogitem);
        }

        // GET: /BacklogItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BacklogItem backlogitem = db.BacklogItems.Find(id);
            if (backlogitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.SprintId = new SelectList(db.Sprints.OrderBy(s => s.SprintName), "SprintId", "SprintName", backlogitem.SprintId);
            return View(backlogitem);
        }

        // POST: /BacklogItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BacklogItemId,Description,Estimate,SprintId")] BacklogItem backlogitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(backlogitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SprintId = new SelectList(db.Sprints.OrderBy(s => s.SprintName), "SprintId", "SprintName", backlogitem.SprintId);
            return View(backlogitem);
        }

        // GET: /BacklogItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BacklogItem backlogitem = db.BacklogItems.Find(id);
            if (backlogitem == null)
            {
                return HttpNotFound();
            }
            return View(backlogitem);
        }

        // POST: /BacklogItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BacklogItem backlogitem = db.BacklogItems.Find(id);
            db.BacklogItems.Remove(backlogitem);
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
