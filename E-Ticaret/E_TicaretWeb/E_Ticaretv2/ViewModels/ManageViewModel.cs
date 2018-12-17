using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Ticaretv2.ViewModels
{
    public class IndexViewModel
    {
        public string PhoneNumber { get; set; }

    }
    public class ChangeInfoViewModel
    {
        [Phone(ErrorMessage = "A phone number can not contain letter or special character")]
        [StringLength(11, ErrorMessage = "A Phone number should be 11 digit", MinimumLength = 11)]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }


        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }
    }

}