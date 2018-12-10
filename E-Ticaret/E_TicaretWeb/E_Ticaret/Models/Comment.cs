using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int commentId { get; set; }

        public string customerUserName { get; set; }

        public int productId { get; set; }

        [StringLength(255)]
        public string commentText { get; set; }

        public int commentLikecount { get; set; }

        public int commentDislikeCount { get; set; }


    }

}