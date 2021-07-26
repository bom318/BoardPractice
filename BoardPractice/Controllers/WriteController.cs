using BoardPractice.DB;
using BoardPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardPractice.Controllers
{
    public class WriteController : Controller
    {
        // GET: Write
        public ActionResult Index()
        {
            UserInfo sessionInfo = null;
            if(Session["UserInfo"] != null)
            {
                sessionInfo = (UserInfo)Session["UserInfo"];
                string sessionId = sessionInfo.userId;
                Board board = new Board();
                board.sessionId = sessionId;

                return View(board);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userId, string title, string content)
        {
            DBManager manager = new DBManager();
            UserInfo sessionInfo= (UserInfo)Session["UserInfo"];
            string sessionId = sessionInfo.userId;
            userId = sessionId;
            manager.CreateBoard(userId, title, content);

            return RedirectToAction("", "Main");
        }
    }
}