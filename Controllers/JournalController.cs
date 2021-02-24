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

        public JournalController(JournalDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<JournalModel> objList = _db.Journals;
            return View(objList);
        }
    }
}
