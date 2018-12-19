using E_Ticaretv2.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Ticaretv2.ViewModels
{
    public class CommentViewModel
    {
        [Display(Name ="Comment ID")]
        public string CommentId { get; set; }

        [Display(Name ="User Name")]
        public string CustomerUserName { get; set; }

        [Display(Name ="Product ID")]
        public string ProductId { get; set; }

        [Display(Name ="Comment")]
        public string CommentText { get; set; }

        [Display(Name ="Like Count")]
        public int LikeCount { get; set; }

        [Display(Name ="Dislike Count")]
        public int DislikeCount { get; set; }

        public FirebaseObject<Comment> commentInfo { get; set; }

    }
}