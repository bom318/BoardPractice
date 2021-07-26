using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardPractice.Models
{
    public class UserInfo
    {
        [Required(ErrorMessage = "사용자 ID를 입력하세요.")]
        public string userId { get; set; }

        [Required(ErrorMessage = "사용자 비밀번호를 입력하세요.")]
        public string userPwd { get; set; }
        public string userName { get; set; }

        public string userIP { get; set; }
    }
}