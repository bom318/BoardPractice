using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardPractice.Models
{
    public class BoardListResult
    {
        public List<Board> BoardList { get; set; }
        public string UserId { get; set; }
    }

    public class Board
    {
        public int num { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string userId { get; set; }
        public DateTime? iDate { get; set; }
        public string sessionId { get; set; }

    }
}