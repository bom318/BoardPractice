using BoardPractice.DB;
using BoardPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace BoardPractice.Controllers
{
    public class RegisterController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        // GET: Register
        public ActionResult Index(String userId,String userPw,String name)
        {
            //비밀번호 암호화
            SHA256Managed sha256Managed = new SHA256Managed();
            byte[] bytes = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(userPw));
            String password = Convert.ToBase64String(bytes);


            DBManager manager = new DBManager();
            bool result = manager.CreateUser(userId,password,name);

            if (result)
            {
                return RedirectToAction("", "Home");
            }
            else
            {
                TempData["message"] = "회원가입 실패";
                return View();
            }

            
        }
    }
}