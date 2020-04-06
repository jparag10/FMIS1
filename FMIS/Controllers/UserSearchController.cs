using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FMIS.Models;
using System.Configuration; //z
using System.Data;          //z
using System.Data.SqlClient;

namespace FMIS.Controllers
{
    public class UserSearchController : Controller
    {
        // GET: UserSearch
        //////////// User Part /////////////////////////////////
        
        static string disease;    //z
        DiseaseCheck dc = new DiseaseCheck();   //z
        Medicaldbcontext db = new Medicaldbcontext();



        [HttpPost]
        public JsonResult Test(string value)
        {
            disease = value;
            dc.disease = value;


            TempData["Test"] = value;
            return Json(new
            {
                result = "OK"
            });
        }

        // GET: Home
        [HttpGet]
        public PartialViewResult Show()
        {
            


                string dis = disease;
                DataSet ds = new DataSet();
                string constr = ConfigurationManager.ConnectionStrings["Medicaldbcontext"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "SELECT Disease, WhatToEat, NotToEat,Name From DieticianDataEntries inner join Dieticians on Dieticians.did = DieticianDataEntries.dieticianid where Disease ='" + disease + "'";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(ds);
                        }
                    }
                }


                return PartialView(ds);
            
        }
        public ActionResult DisplayToUser()
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
                ViewBag.Name = "Welcome, "+name;

            }
                return View();
        }


        public PartialViewResult DiseaseDiv(string diseases)
        {

            DataSet ds = new DataSet();
            string constr = ConfigurationManager.ConnectionStrings["Medicaldbcontext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT distinct disease FROM DieticianDataEntries order by Disease ";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            return PartialView(ds);
        }

        public PartialViewResult DiseaseSub()
        {
            return PartialView();
        }

        public ActionResult DieticianProfile()
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string email = Session["email"].ToString();
                ViewBag.Email = email;
                string name = db.Database.SqlQuery<string>("Select name from Dieticians where email=@email", new SqlParameter("@email", email)).FirstOrDefault();
                ViewBag.Name = name;
                int experience = db.Database.SqlQuery<int>("Select experience from Dieticians where email=@email", new SqlParameter("@email", email)).FirstOrDefault();
                ViewBag.Experience = experience;
                long contact = db.Database.SqlQuery<long>("Select contact from Dieticians where email=@email", new SqlParameter("@email", email)).FirstOrDefault();
                ViewBag.Contact = contact;
                string location = db.Database.SqlQuery<string>("Select location from Dieticians where email=@email", new SqlParameter("@email", email)).FirstOrDefault();
                ViewBag.Location = location;

                return View();
            }
        }

    }
}