 using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FMIS.Controllers;
using FMIS.Models;
using System.Web.Mvc;

namespace FMISUnitTesting.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
       
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;
            //testing
            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);

        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void Test_ViewPage_Names()
        {
            var controller = new HomeController();
            string index = "Index";
            string u_reg_viewpage = "User_Register";
            string diet_reg_viewpage = "DieticianReg";
            string login_viewpage = "Login";
            string DieticianDataEntry_viewpage = "DieticianDataEntry";
            string DieticianProfile_viewpage = "DieticianProfile";
            string DisplayToUser_viewpage = "DisplayToUser";


            var result = controller.User_Register() as ViewResult;
            var result1 = controller.DieticianReg() as ViewResult;
            var result2 = controller.Login() as ViewResult;

            Assert.AreEqual(login_viewpage, result2.ViewName);
            Assert.AreEqual(u_reg_viewpage, result.ViewName);
            Assert.IsNotNull(result1);

        }
        [TestMethod]
        public void Test_Model_Names()
        {
            var controller = new HomeController();
            string email = "Email";
            var dieticianclass = new Dietician();


            var result =  dieticianclass.Email;


            Assert.AreEqual(email, result);

        }
    }
}
