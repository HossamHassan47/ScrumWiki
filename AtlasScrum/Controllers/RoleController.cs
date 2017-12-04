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
    public class RoleController : Controller
    {
        private ScrumContext db = new ScrumContext();

        // GET: /Role/
        public ActionResult Index(string sortOrder, string currentFilterName, string searchStringName, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            // Sort Order
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";

            // Name search
            if (searchStringName != null)
            {
                page = 1;
            }
            else
            {
                searchStringName = currentFilterName;
            }

            ViewBag.CurrentFilterName = searchStringName;

            var roles  = from s in db.Roles select s;

            if (!string.IsNullOrEmpty(searchStringName))
            {
                roles = roles.Where(s => s.Description.ToLower().Contains(searchStringName.ToLower()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    roles = roles.OrderByDescending(s => s.Description);
                    break;
                default:
                    roles = roles.OrderBy(s => s.Description);
                    break;
            }

            var pageSize = 5;
            var pageNumber = page ?? 1;

            return View(roles.ToPagedList(pageNumber, pageSize));
            ////return View(db.Roles.ToList());
        }

        // GET: /Role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: /Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RoleId,Description")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: /Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: /Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RoleId,Description")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: /Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: /Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
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
