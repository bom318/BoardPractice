using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BoardPractice.DB;
using BoardPractice.Models;

namespace BoardPractice.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userId, string userPw)
        {

            SHA256Managed sha256Managed = new SHA256Managed();
            byte[] bytes = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(userPw));
            String password = Convert.ToBase64String(bytes);


            DBManager manager = new DBManager();
            string ip = HttpContext.Request.ServerVariables["REMOTE_ADDR"];

            UserInfo sessionInfo = manager.GetUserInfo(userId, password);
            

            if (sessionInfo != null)
            {
                sessionInfo.userIP = ip;
                Session["UserInfo"] = sessionInfo;
                //다음 페이지로
                return RedirectToAction("", "Main");
            }
            else
            {
                TempData["message"] = "로그인 실패";
            }

            return View();
        }

    }
}

//UserInfo userId = null;
//if (Session["UserInfo"] != null)
//{
//    userId = (UserInfo)Session["UserId"];
//    if (userId.userIP != "ip")
//    {
//        return RedirectToAction("", "home");
//    }
//}
//else
//{
//    return RedirectToAction("", "home");
//}