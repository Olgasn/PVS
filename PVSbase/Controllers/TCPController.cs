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
    public class TCPController : Controller
    {
        private PVSEntities db = new PVSEntities();

        //
        // GET: /TCP/
        [Authorize]
        public ActionResult Index(int? page, string param = "")
        {
            int pageSize = 10;
            var feeders = from f in db.Типы_Складских_Помещений
                          where f.Наименование_Типа_Помещения.StartsWith(param)
                          orderby f.IDТСП
                          select f;
            int pageNumber = (page ?? 1);
            return View(feeders.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /TCP/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Типы_Складских_Помещений типы_складских_помещений = db.Типы_Складских_Помещений.Find(id);
            if (типы_складских_помещений == null)
            {
                return HttpNotFound();
            }
            return View(типы_складских_помещений);
        }

        //
        // GET: /TCP/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IDВВС = new SelectList(db.Виды_Вторичного_Сырья, "IDВВС", "Условия_Хранения");
            return View();
        }

        //
        // POST: /TCP/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Типы_Складских_Помещений типы_складских_помещений)
        {
            if (ModelState.IsValid)
            {
                db.Типы_Складских_Помещений.Add(типы_складских_помещений);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDВВС = new SelectList(db.Виды_Вторичного_Сырья, "IDВВС", "Условия_Хранения", типы_складских_помещений.IDВВС);
            return View(типы_складских_помещений);
        }

        //
        // GET: /TCP/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Типы_Складских_Помещений типы_складских_помещений = db.Типы_Складских_Помещений.Find(id);
            if (типы_складских_помещений == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDВВС = new SelectList(db.Виды_Вторичного_Сырья, "IDВВС", "Условия_Хранения", типы_складских_помещений.IDВВС);
            return View(типы_складских_помещений);
        }

        //
        // POST: /TCP/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Типы_Складских_Помещений типы_складских_помещений)
        {
            if (ModelState.IsValid)
            {
                db.Entry(типы_складских_помещений).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDВВС = new SelectList(db.Виды_Вторичного_Сырья, "IDВВС", "Условия_Хранения", типы_складских_помещений.IDВВС);
            return View(типы_складских_помещений);
        }

        //
        // GET: /TCP/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Типы_Складских_Помещений типы_складских_помещений = db.Типы_Складских_Помещений.Find(id);
            if (типы_складских_помещений == null)
            {
                return HttpNotFound();
            }
            return View(типы_складских_помещений);
        }

        //
        // POST: /TCP/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Типы_Складских_Помещений типы_складских_помещений = db.Типы_Складских_Помещений.Find(id);
            db.Типы_Складских_Помещений.Remove(типы_складских_помещений);
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