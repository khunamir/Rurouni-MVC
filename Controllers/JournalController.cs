using Microsoft.AspNetCore.Mvc;
using Rurouni_v2.Data;
using Rurouni_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rurouni_v2.Controllers
{
    public class JournalController : Controller
    {
        private readonly JournalDbContext _db;

        // Dependancy injection
        public JournalController(JournalDbContext db)
        {
            _db = db;
        }

        // Get - Index
        public IActionResult Index(string sortOrder, string searchString)
        {
            IEnumerable<JournalModel> objList = _db.Journals;

            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            if(!String.IsNullOrEmpty(searchString))
                objList = objList.Where(s => s.Description.Contains(searchString));

            switch (sortOrder)
            {
                case "Date":
                    objList = objList.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    objList = objList.OrderByDescending(s => s.Date);
                    break;
                default:
                    objList = objList.OrderBy(s => s.Date);
                    break;
            }

            return View(objList);
        }

        // Get - Create
        public IActionResult Create()
        {
            return View();
        }

        // Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]  // study validate anti forgery token
        public IActionResult Create(JournalModel obj)
        {
            // Server side validation
            if (ModelState.IsValid)
            {
                _db.Journals.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // Get - Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Journals.Find(id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        // Post - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(JournalModel obj)
        {
            // Server side validation
            if (ModelState.IsValid)
            {
                _db.Journals.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Journals.Find(id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        // Post - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            var obj = _db.Journals.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Journals.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
