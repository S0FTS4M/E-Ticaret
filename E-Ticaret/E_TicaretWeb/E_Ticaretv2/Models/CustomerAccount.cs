using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Ticaretv2.Models
{
    public class CustomerAccount
    {
   
        public string EMail { get; set; }    
        public string Pwd { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }
}