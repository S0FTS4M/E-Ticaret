using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public static class Database
    {
       public static ApplicationDbContext dbContext=new ApplicationDbContext();
        public static Product GetProduct(int index)
        {
            return dbContext.Products.ElementAt(index);
        }
    }
}