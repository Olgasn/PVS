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
    public class SKLADController : Controller
    {
        private PVSEntities db = new PVSEntities();

        //
        // GET: /SKLAD/
        [Authorize]
        public ActionResult Index(int? page, string param = "")
        {
            int pageSize = 10;
            var feeders = from f in db.Складские_Помещения
                          where f.НаименованиеПомещения.StartsWith(param)
                          orderby f.IDСП
                          select f;
            int pageNumber = (page ?? 1);
            return View(feeders.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /SKLAD/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Складские_Помещения складские_помещения = db.Складские_Помещения.Find(id);
            if (складские_помещения == null)
            {
                return HttpNotFound();
            }
            return View(складские_помещения);
        }

        //
        // GET: /SKLAD/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Ответственный = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО");
            return View();
        }

        //
        // POST: /SKLAD/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Складские_Помещения складские_помещения)
        {
            if (ModelState.IsValid)
            {
                db.Складские_Помещения.Add(складские_помещения);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ответственный = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО", складские_помещения.Ответственный);
            return View(складские_помещения);
        }

        //
        // GET: /SKLAD/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Складские_Помещения складские_помещения = db.Складские_Помещения.Find(id);
            if (складские_помещения == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ответственный = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО", складские_помещения.Ответственный);
            return View(складские_помещения);
        }

        //
        // POST: /SKLAD/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Складские_Помещения складские_помещения)
        {
            if (ModelState.IsValid)
            {
                db.Entry(складские_помещения).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ответственный = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО", складские_помещения.Ответственный);
            return View(складские_помещения);
        }

        //
        // GET: /SKLAD/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Складские_Помещения складские_помещения = db.Складские_Помещения.Find(id);
            if (складские_помещения == null)
            {
                return HttpNotFound();
            }
            return View(складские_помещения);
        }

        //
        // POST: /SKLAD/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Складские_Помещения складские_помещения = db.Складские_Помещения.Find(id);
            db.Складские_Помещения.Remove(складские_помещения);
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