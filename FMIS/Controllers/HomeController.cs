using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return View("User_Register");
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
            return View("DieticianReg");
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
                        return RedirectToAction("DisplayToUser", "UserSearch", new { email = login.Email });
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
                        //return RedirectToAction("DieticianProfile", "Home", new { email = login.Email });
                        return RedirectToAction("DieticianDataEntry", "DieticianDataEntry", new { email = login.Email });
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

    }
}