using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaretv2.Models
{
    public class Comment
    {

        public string CommentId { get; set; }

        public string CustomerUserName { get; set; }

        public string ProductId { get; set; }

        public string CommentText { get; set; }

        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }


    }
}