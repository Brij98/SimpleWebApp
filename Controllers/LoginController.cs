using Q1WebApp.Models;
using Q1WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Q1WebApp.Controllers
{
    public class LoginController : Controller
    {
        private AppDAO appDAO = new AppDAO();
        // GET: Login
        public ActionResult Index()
        {

            return View("PersonLogin");
        }

        public ActionResult PersonLogin(PersonLoginCredModel loginCredModel)
        {
            if (appDAO.AuthenticatePerson(loginCredModel).Equals(true))
            {
                /*PersonModel person = appDAO.GetPerson(loginCredModel);
                return View("PersonInfoEdit", person);*/
                PersonInfoViewModel personInfo = appDAO.GetPersonInfo(loginCredModel);
                return View("PersonInfoEdit1", personInfo);
            }
            else
            {
                TempData["LoginFailMessage"] = "Login Failed";
            }
            /*return loginCredModel.Username + " : " + loginCredModel.Password;*/
            return View();
        }

        public ActionResult PersonInfoEdit1(PersonInfoViewModel infoViewModel)
        {
            PersonInfoViewModel personInfo = appDAO.UpDatePersonInfo(infoViewModel);
            if (personInfo != null)
            {
                return View("PersonInfoEdit1", personInfo);
            }
            return View("PersonLogin");
        }
    }
}