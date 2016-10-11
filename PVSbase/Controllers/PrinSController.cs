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
    public class PrinSController : Controller
    {
        private PVSEntities db = new PVSEntities();

        //
        // GET: /PrinS/
        [Authorize]
        public ActionResult Index(int? page, string param = "")
        {
            int pageSize = 10;
            var feeders = from f in db.Принятое_Сырье
                          where f.Помещение_Хранения.StartsWith(param)
                          orderby f.IDПС
                          select f;
            int pageNumber = (page ?? 1);
            return View(feeders.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /PrinS/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Принятое_Сырье принятое_сырье = db.Принятое_Сырье.Find(id);
            if (принятое_сырье == null)
            {
                return HttpNotFound();
            }
            return View(принятое_сырье);
        }

        //
        // GET: /PrinS/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IDВВС = new SelectList(db.Виды_Вторичного_Сырья, "IDВВС", "Условия_Хранения");
            ViewBag.СотрудникПриема = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО");
            return View();
        }

        //
        // POST: /PrinS/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Принятое_Сырье принятое_сырье)
        {
            if (ModelState.IsValid)
            {
                db.Принятое_Сырье.Add(принятое_сырье);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDВВС = new SelectList(db.Виды_Вторичного_Сырья, "IDВВС", "Условия_Хранения", принятое_сырье.IDВВС);
            ViewBag.СотрудникПриема = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО", принятое_сырье.СотрудникПриема);
            return View(принятое_сырье);
        }

        //
        // GET: /PrinS/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Принятое_Сырье принятое_сырье = db.Принятое_Сырье.Find(id);
            if (принятое_сырье == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDВВС = new SelectList(db.Виды_Вторичного_Сырья, "IDВВС", "Условия_Хранения", принятое_сырье.IDВВС);
            ViewBag.СотрудникПриема = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО", принятое_сырье.СотрудникПриема);
            return View(принятое_сырье);
        }

        //
        // POST: /PrinS/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Принятое_Сырье принятое_сырье)
        {
            if (ModelState.IsValid)
            {
                db.Entry(принятое_сырье).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDВВС = new SelectList(db.Виды_Вторичного_Сырья, "IDВВС", "Условия_Хранения", принятое_сырье.IDВВС);
            ViewBag.СотрудникПриема = new SelectList(db.Сотрудники, "IDСотрудника", "ФИО", принятое_сырье.СотрудникПриема);
            return View(принятое_сырье);
        }

        //
        // GET: /PrinS/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Принятое_Сырье принятое_сырье = db.Принятое_Сырье.Find(id);
            if (принятое_сырье == null)
            {
                return HttpNotFound();
            }
            return View(принятое_сырье);
        }

        //
        // POST: /PrinS/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Принятое_Сырье принятое_сырье = db.Принятое_Сырье.Find(id);
            db.Принятое_Сырье.Remove(принятое_сырье);
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