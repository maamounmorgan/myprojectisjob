using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Job.Models;
using System.IO;

namespace Job.Controllers
{
    public class MyJobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MyJobs
        public ActionResult Index()
        {
            var myJobs = db.MyJobs.Include(m => m.Category);
            return View(myJobs.ToList());
        }

        // GET: MyJobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyJob myJob = db.MyJobs.Find(id);
            if (myJob == null)
            {
                return HttpNotFound();
            }
            return View(myJob);
        }

        // GET: MyJobs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: MyJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( MyJob myJob,HttpPostedFile upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                myJob.JobImage = upload.FileName;
                db.MyJobs.Add(myJob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", myJob.CategoryId);
            return View(myJob);
        }

        // GET: MyJobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyJob myJob = db.MyJobs.Find(id);
            if (myJob == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", myJob.CategoryId);
            return View(myJob);
        }

        // POST: MyJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JobTitle,JobContent,JobImage,CategoryId")] MyJob myJob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", myJob.CategoryId);
            return View(myJob);
        }

        // GET: MyJobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyJob myJob = db.MyJobs.Find(id);
            if (myJob == null)
            {
                return HttpNotFound();
            }
            return View(myJob);
        }

        // POST: MyJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyJob myJob = db.MyJobs.Find(id);
            db.MyJobs.Remove(myJob);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
