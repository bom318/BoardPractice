using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardPractice.DB;
using BoardPractice.Models;
using PagedList;

namespace BoardPractice.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index(int? page)
        {
            DBManager manager = new DBManager();
            var boardList = manager.GetBoardList();

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(boardList.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Details()
        {
            UserInfo sessionInfo = null;
            if (Session["UserInfo"] !=null)
            {
                sessionInfo = (UserInfo)Session["UserInfo"];
                string sessionId = sessionInfo.userId;

                int num = int.Parse(Request.QueryString["num"].ToString());
                DBManager manager = new DBManager();
                Board board = manager.BoardDetail(num);
                board.sessionId = sessionId;

                return View(board);
            }
            return RedirectToAction("", "Main");
            
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int num)
        {
            DBManager manager = new DBManager();
            manager.DeleteBoard(num);

            return RedirectToAction("","Main");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            int num = int.Parse(Request.QueryString["num"].ToString());
            DBManager manager = new DBManager();
            Board board = manager.BoardDetail(num);


            return View(board);
        }

        [HttpPost, ActionName("Update")]
        public ActionResult Update(string userId, string title, string content,int num)
        {
            DBManager manager = new DBManager();
            manager.UpdateBoard(userId, title, content, num);

            return RedirectToAction("Details","Main", new { num = num});
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();

            return RedirectToAction("", "Home");
        }

        [HttpPost]
        public JsonResult Search(string keyword)
        {
            DBManager manager = new DBManager();
            var boardList = manager.GetSearchBoard(keyword);

            return Json(boardList);
        }
    }
}