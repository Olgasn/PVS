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
    public class BBCController : Controller
    {
        private PVSEntities db = new PVSEntities();

        //
        // GET: /BBC/
        [Authorize]
        public ActionResult Index(int? page, string param = "")
        {
            int pageSize = 10;
            var feeders = from f in db.Виды_Вторичного_Сырья
                          where f.Наименование.StartsWith(param)
                          orderby f.IDВВС
                          select f;
            int pageNumber = (page ?? 1);
            return View(feeders.ToPagedList(pageNumber, pageSize));
        }
        //
        // GET: /BBC/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Виды_Вторичного_Сырья виды_вторичного_сырья = db.Виды_Вторичного_Сырья.Find(id);
            if (виды_вторичного_сырья == null)
            {
                return HttpNotFound();
            }
            return View(виды_вторичного_сырья);
        }

        //
        // GET: /BBC/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BBC/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Виды_Вторичного_Сырья виды_вторичного_сырья)
        {
            if (ModelState.IsValid)
            {
                db.Виды_Вторичного_Сырья.Add(виды_вторичного_сырья);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(виды_вторичного_сырья);
        }

        //
        // GET: /BBC/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Виды_Вторичного_Сырья виды_вторичного_сырья = db.Виды_Вторичного_Сырья.Find(id);
            if (виды_вторичного_сырья == null)
            {
                return HttpNotFound();
            }
            return View(виды_вторичного_сырья);
        }

        //
        // POST: /BBC/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Виды_Вторичного_Сырья виды_вторичного_сырья)
        {
            if (ModelState.IsValid)
            {
                db.Entry(виды_вторичного_сырья).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(виды_вторичного_сырья);
        }

        //
        // GET: /BBC/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Виды_Вторичного_Сырья виды_вторичного_сырья = db.Виды_Вторичного_Сырья.Find(id);
            if (виды_вторичного_сырья == null)
            {
                return HttpNotFound();
            }
            return View(виды_вторичного_сырья);
        }

        //
        // POST: /BBC/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Виды_Вторичного_Сырья виды_вторичного_сырья = db.Виды_Вторичного_Сырья.Find(id);
            db.Виды_Вторичного_Сырья.Remove(виды_вторичного_сырья);
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