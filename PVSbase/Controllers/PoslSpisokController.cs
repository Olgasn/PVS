using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace PVSbase.Controllers
{
    [Authorize]
    public class PoslSpisokController : Controller
    {
        private PVSEntities db = new PVSEntities();

        //
        // GET: /PoslSpisok/
        [Authorize]
        public ActionResult Index(int? page, string param = "")
        {
            int pageSize = 10;
            var feeders = from f in db.Послужной_Список
                          where f.НаименованиеСписка.StartsWith(param)
                          orderby f.IDПослужной_Список
                          select f;
            int pageNumber = (page ?? 1);
            return View(feeders.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /PoslSpisok/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Послужной_Список послужной_список = db.Послужной_Список.Find(id);
            if (послужной_список == null)
            {
                return HttpNotFound();
            }
            return View(послужной_список);
        }

        //
        // GET: /PoslSpisok/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IDДолжности = new SelectList(db.Должности, "IDДолжности", "НаименованиеДолжности");
            ViewBag.IDСотрудника = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО");
            return View();
        }

        //
        // POST: /PoslSpisok/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Послужной_Список послужной_список)
        {
            if (ModelState.IsValid)
            {
                db.Послужной_Список.Add(послужной_список);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDДолжности = new SelectList(db.Должности, "IDДолжности", "НаименованиеДолжности", послужной_список.IDДолжности);
            ViewBag.IDСотрудника = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО", послужной_список.IDСотрудника);
            return View(послужной_список);
        }

        //
        // GET: /PoslSpisok/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Послужной_Список послужной_список = db.Послужной_Список.Find(id);
            if (послужной_список == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDДолжности = new SelectList(db.Должности, "IDДолжности", "НаименованиеДолжности", послужной_список.IDДолжности);
            ViewBag.IDСотрудника = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО", послужной_список.IDСотрудника);
            return View(послужной_список);
        }

        //
        // POST: /PoslSpisok/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Послужной_Список послужной_список)
        {
            if (ModelState.IsValid)
            {
                db.Entry(послужной_список).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDДолжности = new SelectList(db.Должности, "IDДолжности", "НаименованиеДолжности", послужной_список.IDДолжности);
            ViewBag.IDСотрудника = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО", послужной_список.IDСотрудника);
            return View(послужной_список);
        }

        //
        // GET: /PoslSpisok/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Послужной_Список послужной_список = db.Послужной_Список.Find(id);
            if (послужной_список == null)
            {
                return HttpNotFound();
            }
            return View(послужной_список);
        }

        //
        // POST: /PoslSpisok/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Послужной_Список послужной_список = db.Послужной_Список.Find(id);
            db.Послужной_Список.Remove(послужной_список);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}