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
    public class TeamMemberController : Controller
    {
        private ScrumContext db = new ScrumContext();

        // GET: /TeamMember/
        public ActionResult Index(string sortOrder, string currentFilterFullName, string searchStringFullName, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            // Sort Order
            ViewBag.FullNameSortParm = sortOrder == "fullName" ? "fullName_desc" : "fullName";

            // Name search
            if (searchStringFullName != null)
            {
                page = 1;
            }
            else
            {
                searchStringFullName = currentFilterFullName;
            }

            ViewBag.CurrentFilterFullName = searchStringFullName;

            var members = from s in db.TeamMembers select s;

            if (!string.IsNullOrEmpty(searchStringFullName))
            {
                members = members.Where(s => s.FullName.ToLower().Contains(searchStringFullName.ToLower()));
            }

            switch (sortOrder)
            {
                case "fullName_desc":
                    members = members.OrderByDescending(s => s.FullName);
                    break;
                default:
                    members = members.OrderBy(s => s.FullName);
                    break;
            }

            var pageSize = 5;
            var pageNumber = page ?? 1;

            return View(members.ToPagedList(pageNumber, pageSize));
            ////return View(db.TeamMembers.ToList());
        }

        // GET: /TeamMember/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            TeamMember teammember = db.TeamMembers.Find(id);
            
            if (teammember == null)
            {
                return HttpNotFound();
            }
            
            return View(teammember);
        }

        // GET: /TeamMember/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TeamMember/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TeamMemberId,FullName")] TeamMember teammember)
        {
            if (ModelState.IsValid)
            {
                db.TeamMembers.Add(teammember);
                
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(teammember);
        }

        // GET: /TeamMember/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TeamMember teammember = db.TeamMembers.Find(id);
            
            if (teammember == null)
            {
                return HttpNotFound();
            }
            
            return View(teammember);
        }

        // POST: /TeamMember/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TeamMemberId,FullName")] TeamMember teammember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teammember).State = EntityState.Modified;
                
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(teammember);
        }

        // GET: /TeamMember/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            TeamMember teammember = db.TeamMembers.Find(id);
            
            if (teammember == null)
            {
                return HttpNotFound();
            }
            
            return View(teammember);
        }

        // POST: /TeamMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamMember teammember = db.TeamMembers.Find(id);
            
            db.TeamMembers.Remove(teammember);
            
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
