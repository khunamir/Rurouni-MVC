using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rurouni_v2.Data;
using Rurouni_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Rurouni_v2.Controllers
{
    [Authorize]
    public class JournalController : Controller
    {
        private readonly ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;

        // Dependancy injection
        public JournalController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // Get - Index
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            //var journals = from s in _db.Journals
            //               select s;

            var journals = from x in _db.Journals
                           where x.UserId.Contains(HttpContext.Session.GetString("Id"))
                           select x;
            // todo - journal based on user
            //      - log out

            if (!String.IsNullOrEmpty(searchString))
            {
                journals = journals.Where(s => s.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Date":
                    journals = journals.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    journals = journals.OrderByDescending(s => s.Date);
                    break;
                default:
                    journals = journals.OrderBy(s => s.Date);
                    break;
            }

            int pageSize = 3;

            return View(await PaginatedList<JournalModel>.CreateAsync(journals.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // Get - Create
        public IActionResult Create()
        {
            return View();
        }

        // Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]  // study validate anti forgery token
        public IActionResult Create(JournalModel journal)
        {
            // Server side validation
            if (ModelState.IsValid)
            {
                _db.Journals.Add(journal);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(journal);
        }

        // Get - Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var journal = _db.Journals.Find(id);

            if (journal == null)
                return NotFound();

            return View(journal);
        }

        // Post - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(JournalModel journal)
        {
            // Server side validation
            if (ModelState.IsValid)
            {
                _db.Journals.Update(journal);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Add id for update

            return View(journal);
        }

        // Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var journal = _db.Journals.Find(id);

            if (journal == null)
                return NotFound();

            return View(journal);
        }

        // Post - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var journal = _db.Journals.Find(id);

            if (journal == null)
            {
                return NotFound();
            }

            _db.Journals.Remove(journal);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
