using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaretv2.Models
{
    public class Product
    {

        public string Id { get; set; }


        public string Name { get; set; }

        public int Amount { get; set; }


        public string Image { get; set; }

        public double Price { get; set; }


        public string Desc { get; set; }


        public string Category { get; set; }

        public string SubCategory { get; set; }


        public string mainPageUrl { get; set; }

        public int Discount { get; set; }

        public List<Comment> comment { get; set; }
    }
}