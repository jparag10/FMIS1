using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FMIS.Models;
namespace FMIS.Controllers
{
    public class HomeController : Controller
    {
        Medicaldbcontext db = new Medicaldbcontext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult User_Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult User_Register(User user)
        {
            if (ModelState.IsValid)
            {
                var userLoggedIn = db.Users.SingleOrDefault(x => x.Email == user.Email);
                if (userLoggedIn == null)
                {
                    ViewBag.message = "Register";
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.triedOnce = "yes";
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        //[HttpGet]
        public ActionResult DieticianReg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DieticianReg(Dietician diet, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                if (postedFile != null)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    ////Set the Image File Path.
                    string filePath = "~/Uploads/" + fileName;
                    ////Save the Image File in Folder.
                    postedFile.SaveAs(Server.MapPath(filePath));
                    diet.filepath = filePath;
                }

                ////Insert the Image File details in Table.
                //// FilesEntities entities = new FilesEntities();
                //db.images.Add(new Dietician
                //{
                //    Name = fileName,
                //    FilePath = filePath 
                //});
                var userLoggedIn = db.Dieticians.SingleOrDefault(x => x.Email == diet.Email);
                if (userLoggedIn == null)
                {
                    ViewBag.message = "Register";
                    db.Dieticians.Add(diet);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.triedOnce = "yes";
                    return View();
                } 
            }
            else
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            if (Session["email"] != null)
            {
                return RedirectToAction("Search", "Home", new { email = Session["email"].ToString() });
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var selectedtype = login.type;
                if (selectedtype == "User")
                {
                    var userLoggedIn = db.Users.SingleOrDefault(x => x.Email == login.Email && x.Password == login.Password);
                    if (userLoggedIn != null)
                    {
                        ViewBag.message = "loggedin";
                        Session["email"] = login.Email;
                        return RedirectToAction("Search", "Home", new { email = login.Email });
                    }
                    else
                    {
                        ViewBag.triedOnce = "yes";
                        return View();
                    }
                }
                else if (selectedtype == "Dietician")
                {
                    var dieticianLoggedIn = db.Dieticians.SingleOrDefault(x => x.Email == login.Email && x.Password == login.Password);
                    if (dieticianLoggedIn != null)
                    {
                        ViewBag.message = "loggedin";
                        Session["email"] = login.Email;
                        return RedirectToAction("DieticianDataEntry", "Home", new { email = login.Email });
                    }
                    else
                    {
                        ViewBag.triedOnce = "yes";
                        return View();
                    }
                }
                else
                {
                    ViewBag.triedOnce = "yes";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Search(string email)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.email = Session["email"];
                return View();
            }
        }

        public ActionResult DieticianDataEntry(DieticianDataEntry dde)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.email = Session["email"];
                string email = Session["email"].ToString();
                string name = db.Database.SqlQuery<string>("Select name from Dieticians where email=@email", new SqlParameter("@email", email)).FirstOrDefault();
                ViewBag.Name = name;
                int experience = db.Database.SqlQuery<int>("Select experience from Dieticians where email=@email", new SqlParameter("@email", email)).FirstOrDefault();
                ViewBag.Experience = experience;
                //var dieticiandata = dde.Dietician.(x => x.Email == email);
                // if (dieticiandata != null)
                // {
                //     ViewBag.Name = dde.Dietician.Name;
                //     ViewBag.Experience = dde.Dietician.Experience;
                // }
                return View();
            }
        }
        [HttpPost]
        public ActionResult DieticianDataEntry(DieticianDataEntry dde,string Add,string Search, string Update, string Delete)
        {
            string email = Session["email"].ToString();
            int dieticianid = db.Database.SqlQuery<int>("Select did from Dieticians where email=@email", new SqlParameter("@email", email)).FirstOrDefault();
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //var dieticianLoggedIn = db.Dieticians.SingleOrDefault(x => x.Email == email);

                if (Add == "Add")
                {
                    if (ModelState.IsValid)
                    {
                        var alreadyExist = db.DieticianDataEntries.SingleOrDefault(x => x.Disease == dde.Disease);
                        if (alreadyExist == null)
                        {
                            dde.dieticianid = dieticianid;
                            db.DieticianDataEntries.Add(dde);
                            db.SaveChanges();
                            return View();
                        }
                        else
                        {
                            ViewBag.AlreadyExist = "Record with same Disease Already Exist. Try Search & Update.";
                            return View();
                                
                        }
                    }
                    else
                    {
                        return View();
                    }

                }
                else if (Search == "Search")
                {
                    //var alreadyExist = db.DieticianDataEntries.SingleOrDefault(x => x.ddeID == dde.ddeID && x.dieticianid==dde.dieticianid);

                    //if (ModelState.IsValid)
                    //{
                    //    if (alreadyExist != null)
                    //    {
                            
                    //        ViewBag.wte = alreadyExist.WhatToEat;
                    //        ViewBag.nte = alreadyExist.NotToEat;
                        
                            
                    //    }
                    //    return View();

                    //}
                }
                else if (Update == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        var alreadyExist = db.DieticianDataEntries.SingleOrDefault(x => x.Disease == dde.Disease);
                        if (alreadyExist == null)
                        {
                            db.DieticianDataEntries.Add(dde);
                            db.SaveChanges();
                            return View();
                        }
                        else
                        {
                            //db.Database.SqlQuery("Update DieticianDataEntries set WhatToEat=@whatte ", new SqlParameter("@whatte",dde.WhatToEat)).FirstOrDefault();
                            //db.Database.SqlQuery("Update DieticianDataEntries set  NotToEat=@notte", new SqlParameter("@notte", dde.NotToEat)).FirstOrDefault();
                            DieticianDataEntry ddentry = (from c in db.DieticianDataEntries
                                                          where c.Disease == dde.Disease
                                                          && c.dieticianid==dde.dieticianid
                                                          select c).FirstOrDefault();
                            ddentry.Disease = dde.Disease;
                            ddentry.WhatToEat = dde.WhatToEat;
                            ddentry.NotToEat = dde.NotToEat;
                            ddentry.dieticianid = dieticianid;
                            db.SaveChanges();
                            return View();
                        }
                    }
                    else
                    {
                        return View();
                    }

                }
                else if (Delete == "Delete")
                {
                    int result = db.Database.ExecuteSqlCommand("delete from DieticianDataEntries where Disease = ");

                }
                else
                {
                    return View();
                }

                return View();
            }
        }

            // return View();
        

        



    }
}