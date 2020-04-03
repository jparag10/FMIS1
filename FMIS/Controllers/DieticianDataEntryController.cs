using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FMIS.Models;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data;
using System.Configuration;

namespace FMIS.Controllers
{
    public class DieticianDataEntryController : Controller
    {
        Medicaldbcontext db = new Medicaldbcontext();
        public ActionResult DieticianDataEntry(DieticianDataEntry dde)
        {
            Medicaldbcontext db = new Medicaldbcontext();

            DataTable ds = new DataTable();
            List<String> dr = new List<String>();
            string constr = ConfigurationManager.ConnectionStrings["Medicaldbcontext"].ConnectionString;
            
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

                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "SELECT distinct Disease FROM DieticianDataEntries order by Disease ";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(ds);
                            foreach (DataRow row in ds.Rows)
                            {
                                dr.Add(row["Disease"].ToString());
                            }
                            ViewBag.dis = new SelectList(dr);
                        }
                    }
                }             
                return View();
            }
        }

        [HttpPost]
        public ActionResult DieticianDataEntry(DieticianDataEntry dde, string Add, string Search, string Update, string Delete, string adddisease)
        {
            
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                DataTable ds = new DataTable();
                List<String> dr = new List<String>();
                string constr = ConfigurationManager.ConnectionStrings["Medicaldbcontext"].ConnectionString;
                ViewBag.email = Session["email"];
                string email = Session["email"].ToString();
                string name = db.Database.SqlQuery<string>("Select name from Dieticians where email=@email", new SqlParameter("@email", email)).FirstOrDefault();
                ViewBag.Name = name;
                int experience = db.Database.SqlQuery<int>("Select experience from Dieticians where email=@email", new SqlParameter("@email", email)).FirstOrDefault();
                ViewBag.Experience = experience;
                int dieticianid = db.Database.SqlQuery<int>("Select did from Dieticians where email=@email", new SqlParameter("@email", email)).FirstOrDefault();
                //var dieticianLoggedIn = db.Dieticians.SingleOrDefault(x => x.Email == email);
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "SELECT distinct Disease FROM DieticianDataEntries order by Disease ";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            if (adddisease != null)
                            {
                                dr.Add(adddisease);
                            }
                            sda.Fill(ds);
                            foreach (DataRow row in ds.Rows)
                            {
                                dr.Add(row["Disease"].ToString());
                            }
                            ViewBag.dis = new SelectList(dr);
                        }
                    }
                }

                if (Add == "Add")
                {
                    if (ModelState.IsValid)
                    {
                        var alreadyExist = db.DieticianDataEntries.SingleOrDefault(x => x.Disease == dde.Disease && x.dieticianid == dieticianid);
                        if (alreadyExist == null)
                        {
                            //dde.Dieticians.Add(diet);
                            dde.dieticianid = dieticianid;
                            db.DieticianDataEntries.Add(dde);
                            db.SaveChanges();
                            
                            ViewBag.Success = "Inserted";                          
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
                    if (ModelState.IsValid)
                    {
                        var alreadyExist = db.DieticianDataEntries.SingleOrDefault(x => x.Disease == dde.Disease && x.dieticianid == dieticianid);
                        if (alreadyExist != null)
                        {
                            ViewBag.whatte = "What TO Eat: " + alreadyExist.WhatToEat;
                            //ViewData["whatte"] = dde.WhatToEat;
                            ViewBag.notte = "What TO Avoid" +
                                ": " + alreadyExist.NotToEat;
                            ViewBag.Success = "Search Completed";
                            return View();
                        }
                        else
                        {
                            ViewBag.AlreadyExist = "No Record with Disease Exist . Try ADD.";
                            return View();
                        }
                    }
                    else
                    {
                        return View();
                    }

                }
                else if (Update == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        var alreadyExist = db.DieticianDataEntries.SingleOrDefault(x => x.Disease == dde.Disease && x.dieticianid == dieticianid);
                        if (alreadyExist == null)
                        {
                            db.DieticianDataEntries.Add(dde);
                            db.SaveChanges();
                            ViewBag.Success = "Inserted";
                            return View();
                        }
                        else
                        {                        
                            alreadyExist.Disease = dde.Disease;
                            alreadyExist.WhatToEat = dde.WhatToEat;
                            alreadyExist.NotToEat = dde.NotToEat;
                            alreadyExist.dieticianid = dieticianid;
                            db.SaveChanges();
                            ViewBag.Success = "Updated";
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
                    if (ModelState.IsValid)
                    {
                        var dise = dde.Disease;
                        //int result = db.Database.ExecuteSqlCommand("delete from DieticianDataEntries where Disease = ");
                        int ddeid = db.Database.SqlQuery<int>("Select ddeID from DieticianDataEntries where Disease=@dise", new SqlParameter("@dise", dise)).FirstOrDefault();
                        DieticianDataEntry dieticianDataEntry = db.DieticianDataEntries.Find(ddeid);
                        db.DieticianDataEntries.Remove(dieticianDataEntry);
                        db.SaveChanges();
                        ViewBag.Success = "Deleted";
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
        }

    }
}
