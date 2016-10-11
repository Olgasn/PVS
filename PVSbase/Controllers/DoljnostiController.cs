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
    public class DoljnostiController : Controller
    {
        private PVSEntities db = new PVSEntities();

        //
        // GET: /Doljnosti/
        [Authorize]
        public ActionResult Index(int? page, string param = "")
        {
            int pageSize = 10;
            var feeders = from f in db.Должности
                          where f.НаименованиеДолжности.StartsWith(param)
                          orderby f.IDДолжности
                          select f;
            int pageNumber = (page ?? 1);
            return View(feeders.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Doljnosti/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Должности должности = db.Должности.Find(id);
            if (должности == null)
            {
                return HttpNotFound();
            }
            return View(должности);
        }

        //
        // GET: /Doljnosti/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Doljnosti/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Должности должности)
        {
            if (ModelState.IsValid)
            {
                db.Должности.Add(должности);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(должности);
        }

        //
        // GET: /Doljnosti/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Должности должности = db.Должности.Find(id);
            if (должности == null)
            {
                return HttpNotFound();
            }
            return View(должности);
        }

        //
        // POST: /Doljnosti/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Должности должности)
        {
            if (ModelState.IsValid)
            {
                db.Entry(должности).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(должности);
        }

        //
        // GET: /Doljnosti/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Должности должности = db.Должности.Find(id);
            if (должности == null)
            {
                return HttpNotFound();
            }
            return View(должности);
        }

        //
        // POST: /Doljnosti/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Должности должности = db.Должности.Find(id);
            db.Должности.Remove(должности);
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