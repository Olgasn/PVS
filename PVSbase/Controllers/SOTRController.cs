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
    public class SOTRController : Controller
    {
        private PVSEntities db = new PVSEntities();

        //
        // GET: /SOTR/
        [Authorize]
        public ActionResult Index(int? page, string param = "")
        {
            int pageSize = 10;
            var feeders = from f in db.Сотрудники
                          where f.ФИО.StartsWith(param)
                          orderby f.IDСотрудника
                          select f;
            int pageNumber = (page ?? 1);
            return View(feeders.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /SOTR/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Сотрудники сотрудники = db.Сотрудники.Find(id);
            if (сотрудники == null)
            {
                return HttpNotFound();
            }
            return View(сотрудники);
        }

        //
        // GET: /SOTR/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SOTR/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Сотрудники сотрудники)
        {
            if (ModelState.IsValid)
            {
                db.Сотрудники.Add(сотрудники);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(сотрудники);
        }

        //
        // GET: /SOTR/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Сотрудники сотрудники = db.Сотрудники.Find(id);
            if (сотрудники == null)
            {
                return HttpNotFound();
            }
            return View(сотрудники);
        }

        //
        // POST: /SOTR/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Сотрудники сотрудники)
        {
            if (ModelState.IsValid)
            {
                db.Entry(сотрудники).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(сотрудники);
        }

        //
        // GET: /SOTR/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Сотрудники сотрудники = db.Сотрудники.Find(id);
            if (сотрудники == null)
            {
                return HttpNotFound();
            }
            return View(сотрудники);
        }

        //
        // POST: /SOTR/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Сотрудники сотрудники = db.Сотрудники.Find(id);
            db.Сотрудники.Remove(сотрудники);
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