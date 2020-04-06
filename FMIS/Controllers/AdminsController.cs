using FMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using System.Data.SqlClient;
namespace FMIS.Controllers
{
    public class AdminsController : Controller
    {
        Medicaldbcontext db = new Medicaldbcontext();
        // GET: Admins
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DieticianView()
        {
            return View(getdietician());
        }

        IEnumerable<Dietician> getdietician()
        {
            using (Medicaldbcontext db = new Medicaldbcontext())
            {
                return db.Dieticians.ToList<Dietician>();
            }
        }


        public ActionResult UserView()
        {
            return View(getuser());
        }

        IEnumerable<User> getuser()
        {
            using (Medicaldbcontext db = new Medicaldbcontext())
            {
                return db.Users.ToList<User>();
            }
        }

        public ActionResult DataentryView()
        {


            return View(getdata());
        }

        IEnumerable<DieticianDataEntry> getdata()
        {
            using (Medicaldbcontext db = new Medicaldbcontext())
            {
                return db.DieticianDataEntries.ToList<DieticianDataEntry>();
            }
        }



        public ActionResult Deletedietician(int? id)
        {
            Dietician dietician = db.Dieticians.Find(id);
            if (dietician == null)
            {
                return HttpNotFound();
            }
            return View(dietician);
        }

        // POST: Dieticians/Delete/5
        [HttpPost, ActionName("Deletedietician")]
        [ValidateAntiForgeryToken]
        public ActionResult DieticianDeleteConfirmed(int id)
        {
            Dietician dietician = db.Dieticians.Find(id);
            db.Dieticians.Remove(dietician);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Deleteuser(int? id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Dieticians/Delete/5
        [HttpPost, ActionName("Deleteuser")]
        [ValidateAntiForgeryToken]
        public ActionResult UserDeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Deletedataentry(int? id)
        {
            DieticianDataEntry dataEntry = db.DieticianDataEntries.Find(id);
            if (dataEntry == null)
            {
                return HttpNotFound();
            }
            return View(dataEntry);
        }

        // POST: Dieticians/Delete/5
        [HttpPost, ActionName("Deletedataentry")]
        [ValidateAntiForgeryToken]
        public ActionResult DataentryDeleteConfirmed(int id)
        {
            DieticianDataEntry dataEntry = db.DieticianDataEntries.Find(id);
            db.DieticianDataEntries.Remove(dataEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edituser(int? id)
        {

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edituser([Bind(Include = "userid,Name,Password,Email,Gender,ContactNo,Height,Weight,Age")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        // GET: Dieticians/Edit/5
        public ActionResult Editdietician(int? id)
        {

            Dietician dietician = db.Dieticians.Find(id);
            if (dietician == null)
            {
                return HttpNotFound();
            }
            return View(dietician);
        }

        // POST: Dieticians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editdietician([Bind(Include = "did,Name,Email,Password,Contact,Location,Experience,filepath")] Dietician dietician, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid)
            {
                if (ImagePath != null)
                {
                    string fileName = Path.GetFileName(ImagePath.FileName);
                    ////Set the Image File Path.
                    string filePath = "~/Uploads/" + fileName;
                    ////Save the Image File in Folder.
                    ImagePath.SaveAs(Server.MapPath(filePath));
                    dietician.filepath = filePath;
                }
                else
                {
                    string fileName = Path.GetFileName("/default.jpg");
                    string filePath = "~/Uploads/" + fileName;
                    dietician.filepath = filePath;

                }

                db.Entry(dietician).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dietician);
        }


    }
}