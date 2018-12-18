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
        public string Role { get; set; }

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
    public class AddProductViewModel
    {
        [Required(ErrorMessage ="A Product has to be an ID")]
        [Display(Name="Product ID")]
        public string Id { get; set; }

        [Required(ErrorMessage ="You have to enter the name of product")]
        [Display(Name ="Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="You have to enter number of product in stock")]
        [Display(Name="Product Amount")]
        public int Amount { get; set; }

        [Required]
        [Url(ErrorMessage ="You should enter correct URL address")]
        [Display(Name="Product Image URL")]
        public string Image { get; set; }

        [Required(ErrorMessage ="You have to enter the price of product")]
        [Display(Name ="Product Price")]
        public double Price { get; set; }

        [Display(Name ="Product Description")]
        public string Desc { get; set; }

        [Required(ErrorMessage ="A Product has to be at least one category")]
        [Display(Name="Product Category")]
        public string Category { get; set; }

        [Display(Name="Product Subcategory")]
        public string SubCategory { get; set; }

        [Display(Name="Product Main Page URL")]
        public string mainPageUrl { get; set; }

        [Display(Name="Product Discount Percentage")]
        public int Discount { get; set; }

    }
    public class EditProductViewModel
    {
        [Required]
        [Display(Name="ID")]
        public string Id { get; set; }

        [Required(ErrorMessage = "You have to enter the name of product")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You have to enter number of product in stock")]
        [Display(Name = "Product Amount")]
        public int Amount { get; set; }

        [Required]
        [Url(ErrorMessage = "You should enter correct URL address")]
        [Display(Name = "Product Image URL")]
        public string Image { get; set; }

        [Required(ErrorMessage = "You have to enter the price of product")]
        [Display(Name = "Product Price")]
        public double Price { get; set; }

        [Display(Name = "Product Description")]
        public string Desc { get; set; }

        [Required(ErrorMessage = "A Product has to be at least one category")]
        [Display(Name = "Product Category")]
        public string Category { get; set; }

        [Display(Name = "Product Subcategory")]
        public string SubCategory { get; set; }

        [Display(Name = "Product Main Page URL")]
        public string mainPageUrl { get; set; }

        [Display(Name = "Product Discount Percentage")]
        public int Discount { get; set; }
    }
    


}