using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    [Table("ShoppingCarts")]
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CustomerAccount")]
        public string UserName { get; set; }

        public int boughtProductAmount { get; set; }


    }
}