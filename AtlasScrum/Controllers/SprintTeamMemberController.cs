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
    public class SprintTeamMemberController : Controller
    {
        private ScrumContext db = new ScrumContext();

        // GET: /SprintTeamMember/
        public ActionResult Index(
            string sortOrder,
            int? currentFilterRoleId,
            int? searchRoleId,
            int? currentFilterSprintId,
            int? searchSprintId,
            string currentFilterFullName,
            string searchFullName,
            string currentFilterMoS,
            string searchMoS,
            int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            // Sort Order
            ViewBag.SortParmSprintName = string.IsNullOrEmpty(sortOrder) ? "SprintName_desc" : string.Empty;

            ViewBag.SortParmRoleName = sortOrder == "RoleName" ? "RoleName_desc" : "RoleName";
            ViewBag.SortParmFullName = sortOrder == "FullName" ? "FullName_desc" : "FullName";
            ViewBag.SortParmMoS = sortOrder == "MoS" ? "MoS_desc" : "MoS";

            // Role Search
            if (searchRoleId != null)
            {
                page = 1;
            }
            else
            {
                searchRoleId = currentFilterRoleId;
            }

            // Sprint search
            if (searchSprintId != null)
            {
                page = 1;
            }
            else
            {
                searchSprintId = currentFilterSprintId;
            }

            // Full Name search
            if (searchFullName != null)
            {
                page = 1;
            }
            else
            {
                searchFullName = currentFilterFullName;
            }

            // MoS search
            if (searchMoS != null)
            {
                page = 1;
            }
            else
            {
                searchMoS = currentFilterMoS;
            }

            ViewBag.CurrentFilterRoleId = searchRoleId;
            ViewBag.CurrentFilterSprintId = searchSprintId;
            ViewBag.CurrentFilterFullName = searchFullName;
            ViewBag.CurrentFilterMoS = searchMoS;

            var sprintMembers = from s in db.SprintTeamMembers select s;

            if (!string.IsNullOrEmpty(searchFullName))
            {
                sprintMembers = sprintMembers.Where(s => s.TeamMember.FullName.ToLower().Contains(searchFullName.ToLower()));
            }

            if (searchRoleId > 0)
            {
                sprintMembers = sprintMembers.Where(s => s.RoleId == searchRoleId);
            }

            if (searchSprintId > 0)
            {
                sprintMembers = sprintMembers.Where(s => s.SprintId == searchSprintId);
            }

            if (!string.IsNullOrEmpty(searchMoS))
            {
                sprintMembers = sprintMembers.Where(s => s.IsManOfSprint == (searchMoS == "Yes"));
            }

            switch (sortOrder)
            {
                case "SprintName_desc":
                    sprintMembers = sprintMembers.OrderByDescending(s => s.Sprint.SprintName);
                    break;
                case "RoleName":
                    sprintMembers = sprintMembers.OrderBy(s => s.Role.Description);
                    break;
                case "RoleName_desc":
                    sprintMembers = sprintMembers.OrderByDescending(s => s.Role.Description);
                    break;
                case "FullName":
                    sprintMembers = sprintMembers.OrderBy(s => s.TeamMember.FullName);
                    break;
                case "FullName_desc":
                    sprintMembers = sprintMembers.OrderByDescending(s => s.TeamMember.FullName);
                    break;
                case "MoS":
                    sprintMembers = sprintMembers.OrderBy(s => s.IsManOfSprint);
                    break;
                case "MoS_desc":
                    sprintMembers = sprintMembers.OrderByDescending(s => s.IsManOfSprint);
                    break;
                default:
                    sprintMembers = sprintMembers.OrderBy(s => s.Sprint.SprintName);
                    break;
            }

            ViewBag.SearchRoleId = new SelectList(db.Roles.OrderBy(r => r.Description), "RoleId", "Description", searchRoleId.ToString());
            ViewBag.SearchSprintId = new SelectList(db.Sprints.OrderBy(s => s.SprintName), "SprintId", "SprintName", searchSprintId.ToString());

            ViewBag.SearchMoS = new SelectList(new List<string> { "Yes", "No" }, searchMoS);

            var pageSize = 5;
            var pageNumber = page ?? 1;

            return View(sprintMembers.ToPagedList(pageNumber, pageSize));

            //var sprintteammembers = db.SprintTeamMembers.Include(s => s.Role).Include(s => s.Sprint).Include(s => s.TeamMember);
            //return View(sprintteammembers.ToList());
        }

        public ActionResult ManOfSprint(
            string sortOrder,
            int? currentFilterRoleId,
            int? searchRoleId,
            int? currentFilterSprintId,
            int? searchSprintId,
            string currentFilterFullName,
            string searchFullName,
            int? currentFilterProjectId,
            int? searchProjectId,
            string currentFilterMoS,
            string searchMoS,
            int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            // Sort Order
            ViewBag.SortParmProject = sortOrder == "Project" ? "Project_desc" : "Project";
            ViewBag.SortParmSprintName = string.IsNullOrEmpty(sortOrder) ? "SprintName_desc" : string.Empty;
            ViewBag.SortParmRoleName = sortOrder == "RoleName" ? "RoleName_desc" : "RoleName";
            ViewBag.SortParmFullName = sortOrder == "FullName" ? "FullName_desc" : "FullName";
            ViewBag.SortParmMoS = sortOrder == "MoS" ? "MoS_desc" : "MoS";

            // Role Search
            if (searchRoleId != null)
            {
                page = 1;
            }
            else
            {
                searchRoleId = currentFilterRoleId;
            }

            // Sprint search
            if (searchSprintId != null)
            {
                page = 1;
            }
            else
            {
                searchSprintId = currentFilterSprintId;
            }

            // Full Name search
            if (searchFullName != null)
            {
                page = 1;
            }
            else
            {
                searchFullName = currentFilterFullName;
            }

            // Project search
            if (searchProjectId != null)
            {
                page = 1;
            }
            else
            {
                searchProjectId = currentFilterProjectId;
            }
            
            // MoS search
            if (searchMoS != null)
            {
                page = 1;
            }
            else
            {
                searchMoS = currentFilterMoS;
            }

            ViewBag.CurrentFilterRoleId = searchRoleId;
            ViewBag.CurrentFilterSprintId = searchSprintId;
            ViewBag.CurrentFilterFullName = searchFullName;
            ViewBag.CurrentFilterProjectId = searchProjectId;
            ViewBag.CurrentFilterMoS = searchMoS;

            var sprintMembers = from s in db.SprintTeamMembers select s;

            if (!string.IsNullOrEmpty(searchFullName))
            {
                sprintMembers =
                    sprintMembers.Where(s => s.TeamMember.FullName.ToLower().Contains(searchFullName.ToLower()));
            }

            if (searchRoleId > 0)
            {
                sprintMembers = sprintMembers.Where(s => s.RoleId == searchRoleId);
            }

            if (searchSprintId > 0)
            {
                sprintMembers = sprintMembers.Where(s => s.SprintId == searchSprintId);
            }

            if (searchProjectId > 0)
            {
                sprintMembers = sprintMembers.Where(s => s.Sprint.ProjectId == searchProjectId);
            }

            if (!string.IsNullOrEmpty(searchMoS))
            {
                sprintMembers = sprintMembers.Where(s => s.IsManOfSprint == (searchMoS == "Yes"));
            }

            switch (sortOrder)
            {
                case "SprintName_desc":
                    sprintMembers = sprintMembers.OrderByDescending(s => s.Sprint.SprintName);
                    break;
                case "RoleName":
                    sprintMembers = sprintMembers.OrderBy(s => s.Role.Description);
                    break;
                case "RoleName_desc":
                    sprintMembers = sprintMembers.OrderByDescending(s => s.Role.Description);
                    break;
                case "FullName":
                    sprintMembers = sprintMembers.OrderBy(s => s.TeamMember.FullName);
                    break;
                case "FullName_desc":
                    sprintMembers = sprintMembers.OrderByDescending(s => s.TeamMember.FullName);
                    break;
                case "Project":
                    sprintMembers = sprintMembers.OrderBy(s => s.Sprint.Project.ProjectName);
                    break;
                case "Project_desc":
                    sprintMembers = sprintMembers.OrderByDescending(s => s.Sprint.Project.ProjectName);
                    break;
                case "MoS":
                    sprintMembers = sprintMembers.OrderBy(s => s.IsManOfSprint);
                    break;
                case "MoS_desc":
                    sprintMembers = sprintMembers.OrderByDescending(s => s.IsManOfSprint);
                    break;
                default:
                    sprintMembers = sprintMembers.OrderBy(s => s.Sprint.SprintName);
                    break;
            }

            ViewBag.SearchProjectId = new SelectList(
                db.Projects.OrderBy(r => r.ProjectName),
                "ProjectId",
                "ProjectName",
                searchProjectId.ToString());

            ViewBag.SearchRoleId = new SelectList(
                db.Roles.OrderBy(r => r.Description),
                "RoleId",
                "Description",
                searchRoleId.ToString());

            ViewBag.SearchSprintId = new SelectList(
                db.Sprints.OrderBy(s => s.SprintName),
                "SprintId",
                "SprintName",
                searchSprintId.ToString());

            ViewBag.SearchMoS = new SelectList(new List<string> { "Yes", "No" }, searchProjectId);

            var pageSize = 5;
            var pageNumber = page ?? 1;

            return View(sprintMembers.ToPagedList(pageNumber, pageSize));
        }

        // GET: /SprintTeamMember/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SprintTeamMember sprintteammember = db.SprintTeamMembers.Find(id);

            if (sprintteammember == null)
            {
                return HttpNotFound();
            }

            return View(sprintteammember);
        }

        // GET: /SprintTeamMember/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles.OrderBy(r => r.Description), "RoleId", "Description");
            ViewBag.SprintId = new SelectList(db.Sprints.OrderBy(s => s.SprintName), "SprintId", "SprintName");
            ViewBag.TeamMemberId = new SelectList(db.TeamMembers.OrderBy(t => t.FullName), "TeamMemberId", "FullName");

            return View();
        }

        // POST: /SprintTeamMember/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SprintTeamMemberId,SprintId,TeamMemberId,RoleId,IsManOfSprint")] SprintTeamMember sprintteammember)
        {
            if (ModelState.IsValid)
            {
                db.SprintTeamMembers.Add(sprintteammember);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles.OrderBy(r => r.Description), "RoleId", "Description", sprintteammember.RoleId);
            ViewBag.SprintId = new SelectList(db.Sprints.OrderBy(s => s.SprintName), "SprintId", "SprintName", sprintteammember.SprintId);
            ViewBag.TeamMemberId = new SelectList(db.TeamMembers.OrderBy(t => t.FullName), "TeamMemberId", "FullName", sprintteammember.TeamMemberId);

            return View(sprintteammember);
        }

        // GET: /SprintTeamMember/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SprintTeamMember sprintteammember = db.SprintTeamMembers.Find(id);

            if (sprintteammember == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleId = new SelectList(db.Roles.OrderBy(r => r.Description), "RoleId", "Description", sprintteammember.RoleId);
            ViewBag.SprintId = new SelectList(db.Sprints.OrderBy(s => s.SprintName), "SprintId", "SprintName", sprintteammember.SprintId);
            ViewBag.TeamMemberId = new SelectList(db.TeamMembers.OrderBy(t => t.FullName), "TeamMemberId", "FullName", sprintteammember.TeamMemberId);

            return View(sprintteammember);
        }

        // POST: /SprintTeamMember/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SprintTeamMemberId,SprintId,TeamMemberId,RoleId,IsManOfSprint")] SprintTeamMember sprintteammember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprintteammember).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles.OrderBy(r => r.Description), "RoleId", "Description", sprintteammember.RoleId);
            ViewBag.SprintId = new SelectList(db.Sprints.OrderBy(s => s.SprintName), "SprintId", "SprintName", sprintteammember.SprintId);
            ViewBag.TeamMemberId = new SelectList(db.TeamMembers.OrderBy(t => t.FullName), "TeamMemberId", "FullName", sprintteammember.TeamMemberId);

            return View(sprintteammember);
        }

        // GET: /SprintTeamMember/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SprintTeamMember sprintteammember = db.SprintTeamMembers.Find(id);

            if (sprintteammember == null)
            {
                return HttpNotFound();
            }

            return View(sprintteammember);
        }

        // POST: /SprintTeamMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SprintTeamMember sprintteammember = db.SprintTeamMembers.Find(id);

            db.SprintTeamMembers.Remove(sprintteammember);
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
